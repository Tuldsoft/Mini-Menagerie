using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrollListMonitor : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected GameObject gridContent = null;

    protected GameObject prefabPanel = null; // set by Start() in children

    protected List<IComparable> referenceList;

    protected bool keepFirstPanel = false;

    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        PopulateGrid();
    }
       


    // Fills the ScrollList with ScrollListPanels
    public abstract void PopulateGrid();
    // body must contain a foreach loop that iterates through a collection,
    // instantiating new prefabs for each item in the collection.
    

    // Empty the ScrollList
    protected void EmptyGrid()
    {
        for (int i = (keepFirstPanel ? 1 : 0); i < gridContent.transform.childCount; i++)
        {
            Destroy(gridContent.transform.GetChild(i).gameObject);
        }
    }
}
