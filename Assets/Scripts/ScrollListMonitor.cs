using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScrollListMonitor<T> : MonoBehaviour where T : IComparable<T>
{
    #region Fields

    [SerializeField]
    protected GameObject gridContent = null;     // set in Inspector

    //protected GameObject prefabPanel = null;     // set by Start() in children
    //protected GameObject prefabNewPanel = null;  // set by Start() in children, if used

    protected IEnumerable<T> referenceList;             // set by Start() in children

    protected bool keepFirstPanel = false;       // set by Start() in children
    protected bool newAsLastPanel = false;       // set by Start() in children

    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        PopulateGrid(false); // scroll to top at start
    }

        
    // Fills the ScrollList with ScrollListPanels
    // Overridde in case T is not IComparable (ex. a Sprite)
    // T is generally a Mini, a Descriptor, or a Tag (IComparables)
    public virtual void PopulateGrid()
    {
        EmptyGrid();

        //GameObject newPanel;

        foreach (T listItem in referenceList)
        {
            AddToGrid(listItem, Loader.GetPanel<T>(this, listItem));
        }
        if (newAsLastPanel)
        {
            AddToGrid(default, Loader.GetPanel<T>(this, default));
        }

        /*foreach (T listItem in referenceList)
        {
            AddToGrid(listItem, prefabPanel);
        }

        if (prefabNewPanel != null)
        {
            AddToGrid(default, prefabNewPanel);
        }*/

    }

    // overload to scroll to end
    public virtual void PopulateGrid(bool scrollToEnd)
    {
        PopulateGrid();

        StartCoroutine(ScrollToEnd(scrollToEnd));
    }

    // listItem is null (default) if creating a "new" panel
    public virtual void AddToGrid(T listItem, GameObject prefab)
    {
        if (prefab is null)
        {
            Debug.Log("Attempting to create a panel from null");
            return;
        }
        
        GameObject newPanel;

        newPanel = Instantiate(prefab, gridContent.transform);
        ScrollListPanel<T> panel = newPanel.GetComponent<ScrollListPanel<T>>();

        // set up panel based on listItem info, and register this as its monitor
        panel.SetPanel(listItem, this);

        // waits for next frame to load before scrolling to end.
        StartCoroutine(ScrollToEnd());
    }

    // need to run this in the next frame
    IEnumerator ScrollToEnd(bool toEnd = true)
    {
        yield return new WaitForEndOfFrame();
        ScrollRect sr = 
            gridContent.transform.parent.transform.parent.GetComponent<ScrollRect>();
        sr.verticalNormalizedPosition = toEnd ? 0f : 1f;

        yield return null;
    }


    // Empty the ScrollList
    protected void EmptyGrid()
    {
        for (int i = (keepFirstPanel ? 1 : 0); i < gridContent.transform.childCount; i++)
        {
            Destroy(gridContent.transform.GetChild(i).gameObject);
        }
    }
}
