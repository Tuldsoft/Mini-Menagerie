using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuMonitor : MonoBehaviour
{
    [SerializeField]
    Text menuItemTemplate = null;

    [SerializeField]
    GameObject menuScroll = null;

    [SerializeField]
    Button closeButton = null;

    // These are now passed to the Set method, and do not need to be stored locally
    /*MenuType menuType = MenuType.Main;
    bool anchorLeft = true;

    const int MenuItemsCount = 7;
    List<MenuItem> menuItems = new List<MenuItem>();*/

    /*
     * 0 - Browse
     * 1 - Descriptors
     * 2 - Tags
     * 3 - Encounter_Builder
     * 4 - Collection_Stats
     * 5 - Help
     * 6 - About
    */

    // Populated by PositionMenuItems()
    List<Button> menuButtons = new List<Button>();
    List<object> menuResults = new List<object>();

    // Set up the monitor and await the result of the click
    public async Task<object> SetMonitorAsync (MenuType menuType, CancellationToken ct)
    {
        // Dictionary<object, string>
        var names = MenuManager.MenuNamesDict[menuType];

        // PositionMenuItems(Dict names, bool anchorLeft)
        PositionMenuItems(names, menuType!=MenuType.Trait);

        // result may be a newly-constructed trait, for example
        object result = await SetMenuButtonsAsync(names, ct);
        
        // after result is captured, menu is no longer needed, so destroy
        Destroy(gameObject);

        return result;

    }
    
    // size and position buttons for the menu
    void PositionMenuItems(Dictionary<object, string> names, bool anchorLeft=true)
    {
        // get info from template
        Vector2 position = menuItemTemplate.transform.localPosition;
        float height = menuItemTemplate.rectTransform.rect.height * menuItemTemplate.rectTransform.localScale.y;
        float offsetY = Mathf.Abs(position.y) - (height / 2);
        Transform parentTransform = menuItemTemplate.transform.parent.transform;
        Vector3 scale = menuItemTemplate.transform.localScale;

        //float maxWidth = 0f;

        foreach (KeyValuePair<object, string> pair in names)
        {

            GameObject newMenuItem = Instantiate(menuItemTemplate.gameObject);
            newMenuItem.transform.SetParent(parentTransform);
            newMenuItem.transform.localScale = scale;
            newMenuItem.transform.localPosition = position;

            //RectTransform rectT = newMenuItem.GetComponent<RectTransform>();
            //maxWidth = Mathf.Max(Mathf.Abs(rectT.sizeDelta.x * rectT.localScale.x), maxWidth);

            newMenuItem.GetComponent<Text>().text = pair.Value;

            menuButtons.Add(newMenuItem.GetComponentInChildren<Button>());
            menuResults.Add(pair.Key);

            // move to next position
            position = new Vector2(position.x, position.y - (height + offsetY));
        }

        Destroy(menuItemTemplate);

        // resize Content (parentTransform) height 
        RectTransform rt = parentTransform.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Abs(position.y) - height + offsetY);

        /*
        // using resize of texts to fit width, no need to expand Menuscroll width
        // later, keep the size of the text the same, and resize the menu to fit the text.

        // resize MenuScroll width (Content -> Viewport -> MenuScroll
        Transform scrollTransform = parentTransform.parent.transform.parent.transform;
        rt = scrollTransform.GetComponent<RectTransform>();

        float scrollWidth = rt.rect.width;
        if (scrollWidth < maxWidth)
            rt.sizeDelta = new Vector2(maxWidth, rt.sizeDelta.y);
        */

        // position MenuScroll left or right
        if (!anchorLeft)
        {
            RectTransform msRT = menuScroll.GetComponent<RectTransform>();
            msRT.anchorMin = new Vector2(1f, 1f);
            msRT.anchorMax = new Vector2(1f, 1f);
            Vector2 aPos = msRT.anchoredPosition;
            aPos.x = -aPos.x - msRT.sizeDelta.x;
            msRT.anchoredPosition = aPos;
        }

    }

    // Upon completion of a Task, an object is returned. This assigns Button_ClickAsync
    //   to be triggered on each button. The first button clicked returns its value.
    async Task<object> SetMenuButtonsAsync(Dictionary<object, string> names, CancellationToken ct)
    {
        // Set up button tasks

        var tasks = new List<Task<object>>();
        var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);

        // try block to contain and dispose of leaky linkedcts
        try
        {
            var linkedCt = linkedCts.Token;

            for (int i = 0; i < menuButtons.Count; i++)
            {
                tasks.Add(MenuManager.Button_ClickAsync(menuButtons[i], menuResults[i], linkedCt));
            }

            // implement close button and android back button function
            tasks.Add(MenuManager.Button_ClickAsync(closeButton, null, linkedCt));
            //tasks.Add(MenuManager.Button_ClickAsync(backButton, null, linkedCt));  

            // Wait for any button (or other) task to complete
            Task<object> finishedTask = await Task.WhenAny(tasks);

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
}
