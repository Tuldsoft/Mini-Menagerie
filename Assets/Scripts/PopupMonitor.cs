using System.Collections.Generic;
using System.Threading;       // for CancellationToken
using System.Threading.Tasks; // for async/await
using UnityEngine;
using UnityEngine.UI;

public enum PopupType { OK, OKCancel, YesNo, YesNoCancel, String }
public enum PopupResult { OK, Cancel, Yes, No, String }


public class PopupMonitor : MonoBehaviour
{
    [SerializeField]
    Button templateButton = null, closeButton = null;

    [SerializeField]
    Text messageText = null;
    //Text messageText = null, titleText = null;

    #region Static Initialization
    static bool initialized = false;

    // Retrieves an array of position offsets determined by PopupType
    // Used by Start() to position the buttons
    // Initialized in Initialize()
    static Dictionary<PopupType, Vector3[]> positionsDict
        = new Dictionary<PopupType, Vector3[]>();

    // Position settings can be one of these three, based on the number of buttons
    // One of these is stored in positions
    static readonly Vector3[] positions1 = { Vector2.zero };

    static readonly Vector3[] positions2 = {
        new Vector3(-100f, 0f),
        new Vector3(100f, 0f)};

    static readonly Vector3[] positions3 = {
        new Vector3(-175f, 0f),
        Vector3.zero,
        new Vector3(175f, 0f)};


    // Retrieves an array of responses, determined by PopupType
    // Initialized in Initialize
    static Dictionary<PopupType, PopupResult[]> resultsDict
        = new Dictionary<PopupType, PopupResult[]>();

    static Dictionary<PopupType, PopupResult> backCloseResultDict
        = new Dictionary<PopupType, PopupResult>();


    // Called by Initializer
    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        // On initialization, set definitions for the potential positions of the buttons
        // positions1, positions2, positions3 correspond to an array of 1, 2, or 3 offsets 
        positionsDict.Add(PopupType.OK, positions1);
        positionsDict.Add(PopupType.OKCancel, positions2);
        positionsDict.Add(PopupType.YesNo, positions2);
        positionsDict.Add(PopupType.YesNoCancel, positions3);
        positionsDict.Add(PopupType.String, positions1);

        // On initialization, set definitions for the potential results, based on pType
        resultsDict.Add(PopupType.OK, new PopupResult[1] {
            PopupResult.OK });
        resultsDict.Add(PopupType.OKCancel, new PopupResult[2] {
            PopupResult.OK,
            PopupResult.Cancel });
        resultsDict.Add(PopupType.YesNo, new PopupResult[2] {
            PopupResult.Yes,
            PopupResult.No });
        resultsDict.Add(PopupType.YesNoCancel, new PopupResult[3] {
            PopupResult.Yes,
            PopupResult.No,
            PopupResult.Cancel });
        resultsDict.Add(PopupType.String, new PopupResult[2] {
            PopupResult.String,
            PopupResult.Cancel });

        // On initialization, set definitions for the default action of the back
        //   or close buttons, based on pType
        backCloseResultDict.Add(PopupType.OK, PopupResult.OK);
        backCloseResultDict.Add(PopupType.OKCancel, PopupResult.Cancel);
        backCloseResultDict.Add(PopupType.YesNo, PopupResult.No);
        backCloseResultDict.Add(PopupType.YesNoCancel, PopupResult.Cancel);
        backCloseResultDict.Add(PopupType.String, PopupResult.Cancel);
    }

    #endregion

    // SetPopup replaces Start(), since parameters are required
    public async Task<PopupResult> SetPopupAsync(PopupType pType, string message, CancellationToken ct)
    {
        messageText.text = message;

        // Retrieve the number of buttons, based on PopupType
        int numButtons = positionsDict[pType].Length;

        // buttons[i] at positions[i] generates results[i]
        PopupResult[] results = resultsDict[pType];
        Button[] buttons = new Button[numButtons];

        // Create 1 to 3 buttons
        Button newButton;
        for (int i = 0; i < numButtons; i++)
        {
            // Create and place the button
            newButton = Instantiate<Button>(templateButton, templateButton.transform.parent);
            newButton.transform.localPosition += positionsDict[pType][i];
            newButton.GetComponentInChildren<Text>().text = results[i].ToString();
            newButton.name = results[i].ToString() + "Button";

            // Store a reference to the button for later, tied to responses
            buttons[i] = newButton;
        }
        
        Destroy(templateButton.gameObject);

        // Set up button tasks

        var tasks = new List<Task<PopupResult>>();
        var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);

        // try block to contain and dispose of leaky linkedcts
        try
        {
            var linkedCt = linkedCts.Token;

            for (int i = 0; i < numButtons; i++)
            {
                tasks.Add(PopupButton_ClickAsync(buttons[i], results[i], linkedCt));
            }

            // implement android back button and close button function
            tasks.Add(PopupButton_ClickAsync(closeButton, backCloseResultDict[pType], linkedCt));
            //tasks.Add(PopupButton_ClickAsync(backButton, backCloseResultDict[pType], linkedCt));

            // Wait for any button (or other) task to complete
            Task<PopupResult> finishedTask = await Task.WhenAny(tasks);

            // extra await to propogate Exceptions
            await finishedTask;

            linkedCts.Cancel();

            return finishedTask.Result;
        }
        finally
        {
            linkedCts.Dispose();
            // add animation coroutine for closing the popup

        }

    }

    // this task is set up for each button
    static async Task<PopupResult> PopupButton_ClickAsync (Button button, PopupResult pResult, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested(); // necessary?

        bool isPressed = false;
        button.onClick.AddListener(() => isPressed = true);

        //Debug.Log($"Button {button.name} gives result of {pResult} and is on {button.gameObject.name}");

        while (!isPressed)
        {
            await Task.Yield(); // let someone else take over, unless pressed
        }
        
        // this only is reached if it is the first task to complete
        return pResult;
    }
}
