using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrollListMonitor<T> : MonoBehaviour where T : IComparable<T>
{
    #region Fields

    [SerializeField]
    protected GameObject gridContent = null;     // set in Inspector

    protected GameObject prefabPanel = null;     // set by Start() in children
    protected GameObject prefabNewPanel = null;  // set by Start() in children, if used

    protected List<T> referenceList;             // set by Start() in children

    protected bool keepFirstPanel = false;       // set by Start() in children

    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        PopulateGrid();
    }
       


    // Fills the ScrollList with ScrollListPanels
    // Overridde in case T is not IComparable (ex. a Sprite)
    // T is generally a Mini, a Descriptor, or a Tag (IComparables)
    public virtual void PopulateGrid()
    {
        EmptyGrid();

        GameObject newPanel;

        foreach (T listItem in referenceList)
        {
            newPanel = Instantiate(prefabPanel, gridContent.transform);
            ScrollListPanel<T> miniPanel = newPanel.GetComponent<ScrollListPanel<T>>();

            // set up panel based on listItem info, and register this as its monitor
            miniPanel.SetPanel(listItem, this); 
        }

        if (prefabNewPanel != null)
        {
            newPanel = Instantiate(prefabNewPanel, gridContent.transform);
            ScrollListPanel<T> listPanel = newPanel.GetComponent<ScrollListPanel<T>>();

            listPanel.SetPanel(null, this);
        }
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
