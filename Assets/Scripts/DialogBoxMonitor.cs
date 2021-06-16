using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum DialogBoxType { OK, OKCancel, YesNo, YesNoCancel, String }
public enum DialogBoxResponse { OK, Cancel, Yes, No, String }

public class DialogBoxMonitor : MonoBehaviour
{
    [SerializeField]
    Button templateButton = null, closeButton = null;

    [SerializeField]
    Text messageText = null;


    // Set the static type and message before instantiating the box
    static public DialogBoxType DBType { get; set; } = DialogBoxType.OK;
    static public string Message { get; set; } = "The Message";
    
    // Result of dialog box stored as Static, since there will only ever be one of these at a time
    static public DialogBoxResponse Response { get; private set; } = DialogBoxResponse.OK;



    #region Static Initialization
    // Static references, initialized
    static bool initialized = false;

    static Dictionary<DialogBoxType, Vector3[]> positions 
        = new Dictionary<DialogBoxType, Vector3[]>();
    static Dictionary<DialogBoxType, DialogBoxResponse[]> responses 
        = new Dictionary<DialogBoxType, DialogBoxResponse[]>();
    static Dictionary<DialogBoxResponse, UnityAction> actions 
        = new Dictionary<DialogBoxResponse, UnityAction>();
    static Dictionary<DialogBoxType, UnityAction> closeActions
        = new Dictionary<DialogBoxType, UnityAction>();

    static readonly Vector3[] positions1 = { Vector2.zero };
    
    static readonly Vector3[] positions2 = { 
        new Vector3(-100f, 0f),
        new Vector3(100f, 0f)};

    static readonly Vector3[] positions3 = { 
        new Vector3(-175f, 0f),
        Vector3.zero,
        new Vector3(175f, 0f)};

    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        positions.Add(DialogBoxType.OK, positions1);
        positions.Add(DialogBoxType.OKCancel, positions2);
        positions.Add(DialogBoxType.YesNo, positions2);
        positions.Add(DialogBoxType.YesNoCancel, positions3);
        positions.Add(DialogBoxType.String, positions1);

        responses.Add(DialogBoxType.OK, new DialogBoxResponse[1] {
            DialogBoxResponse.OK });
        responses.Add(DialogBoxType.OKCancel, new DialogBoxResponse[2] {
            DialogBoxResponse.OK,
            DialogBoxResponse.Cancel });
        responses.Add(DialogBoxType.YesNo, new DialogBoxResponse[2] {
            DialogBoxResponse.Yes,
            DialogBoxResponse.No });
        responses.Add(DialogBoxType.YesNoCancel, new DialogBoxResponse[3] {
            DialogBoxResponse.Yes,
            DialogBoxResponse.No,
            DialogBoxResponse.Cancel });
        responses.Add(DialogBoxType.String, new DialogBoxResponse[2] { 
            DialogBoxResponse.String,
            DialogBoxResponse.Cancel });

        actions.Add(DialogBoxResponse.OK, () => Response = DialogBoxResponse.OK);
        actions.Add(DialogBoxResponse.Cancel, () => Response = DialogBoxResponse.Cancel);
        actions.Add(DialogBoxResponse.Yes, () => Response = DialogBoxResponse.Yes);
        actions.Add(DialogBoxResponse.No, () => Response = DialogBoxResponse.No);
        actions.Add(DialogBoxResponse.String, () => Response = DialogBoxResponse.String); 

        // Define action of the close button per type
        closeActions.Add(DialogBoxType.OK, () => Response = DialogBoxResponse.OK);
        closeActions.Add(DialogBoxType.OKCancel, () => Response = DialogBoxResponse.Cancel);
        closeActions.Add(DialogBoxType.YesNo, () => Response = DialogBoxResponse.No);
        closeActions.Add(DialogBoxType.YesNoCancel, () => Response = DialogBoxResponse.Cancel);
        closeActions.Add(DialogBoxType.String, () => Response = DialogBoxResponse.Cancel);

    }
    #endregion

    // Type is set as static property before object is created.
    // Using static property eliminates the need for a SetBox(type) method. 
    // Called by Unity upon creation before Update()
    void Start ()
    {
        Button newButton;
        int numButtons = positions[DBType].Length;
        Button[] buttons = new Button[numButtons];

        // Create 1 to 3 buttons, based on DBType
        for (int i = 0; i < numButtons; i++)
        {
            newButton = Instantiate<Button>(templateButton, templateButton.transform.parent);
            newButton.transform.localPosition += positions[DBType][i];
            buttons[i] = newButton;

            DialogBoxResponse response = responses[DBType][i];
            
            // Clicking a button sets a Response via an annonymous UnityAction
            // ex: UnityAction( () => Response = DialogRespose.OK );
            buttons[i].onClick.AddListener( actions[response] );
            buttons[i].GetComponentInChildren<Text>().text = 
                response == DialogBoxResponse.String ? "OK"
                : response.ToString(); // Display the text "OK" for a string button
        }

        // Set duplicate action for the close button
        // ALL buttons also invoke SubMenu.Close_Click()
        closeButton.onClick.AddListener(closeActions[DBType]);

        messageText.text = Message;

        Destroy(templateButton.gameObject);
    }

    // Button_Click() ... unnecessary. Every button calls Submenu.Close_Click()
    // An annonymous method is also invoked to set the Response.

    // TO DO: Refactor the coroutines using TAP (Task-oriented Asyncronous Programming)
    // in order to return a value. 
    // Read: https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap

    // This should eliminate the need to store things staticly and retrieve them, better OOP.

    // ... OK, I simply could not wrap my brain around a good wait to do this. What I'm looking
    // for is a way to await user input without the bother of events and listeners.

   
}
