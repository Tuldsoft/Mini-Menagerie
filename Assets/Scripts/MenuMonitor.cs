using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMonitor : MonoBehaviour
{

    /*  
     *  create a list of menu options
     *  generate items on the list
     *  name the items
     *  use Utils button colorizer
     *  use delegates?
     *  space the items in the prefab
     *  resize the content panel
     *  
     *  Do this similar to DungeonLevels..
    */

    [SerializeField]
    Text menuItemTemplate = null;


    const int MenuItemsCount = 7;
    List<MenuItem> menuItems = new List<MenuItem>();

    /*
     * 0 - Browse
     * 1 - Descriptors
     * 2 - Tags
     * 3 - Encounter_Builder
     * 4 - Collection_Stats
     * 5 - Help
     * 6 - About
    */

    public void Start()
    {
        SetMenu();
    }

    public void SetMenu()
    {
        // get info from template
        Vector2 position = menuItemTemplate.transform.localPosition;
        float height = menuItemTemplate.rectTransform.rect.height * menuItemTemplate.rectTransform.localScale.y;
        float offsetY = Mathf.Abs(position.y) - (height / 2);
        Transform parentTransform = menuItemTemplate.transform.parent.transform;
        Vector3 scale = menuItemTemplate.transform.localScale;

        //float maxWidth = 0f;

        foreach (KeyValuePair<MenuName, string> pair in MenuManager.MenuNamesDict)
        {

            GameObject newMenuItem = Instantiate(menuItemTemplate.gameObject);
            newMenuItem.transform.SetParent(parentTransform);
            newMenuItem.transform.localScale = scale;
            newMenuItem.transform.localPosition = position;

            //RectTransform rectT = newMenuItem.GetComponent<RectTransform>();
            //maxWidth = Mathf.Max(Mathf.Abs(rectT.sizeDelta.x * rectT.localScale.x), maxWidth);

            MenuItem menuItem = newMenuItem.GetComponent<MenuItem>();
            menuItem.SetMenuItem(pair.Key, this);

            menuItems.Add(menuItem);

            // move to next position
            position = new Vector2(position.x, position.y - (height + offsetY));
        }
        
        Destroy(menuItemTemplate);

        // resize Content (parentTransform) height 
        RectTransform rt = parentTransform.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Abs(position.y) - height + offsetY);

        // using resize of texts to fit width, no need to expand Menuscroll width

        // resize MenuScroll width (Content -> Viewport -> MenuScroll
        /*Transform scrollTransform = parentTransform.parent.transform.parent.transform;
        rt = scrollTransform.GetComponent<RectTransform>();

        float scrollWidth = rt.rect.width;
        if (scrollWidth < maxWidth)
            rt.sizeDelta = new Vector2(maxWidth, rt.sizeDelta.y);*/


    }


    public void CloseMenu()
    {
        Destroy(gameObject);
    }


}
