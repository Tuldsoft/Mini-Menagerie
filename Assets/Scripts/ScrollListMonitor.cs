using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollListMonitor : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected GameObject gridContent = null;

    protected GameObject prefabPanel = null; // set by Start() in children

    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        PopulateGrid();
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/



    // Fills the ScrollList with ScrollListPanels
    public virtual void PopulateGrid()
    {
        EmptyGrid();

        GameObject newPanel;

        foreach (Mini mini in MiniCollection.Minis)
        {
            newPanel = Instantiate(prefabPanel, gridContent.transform);
            ScrollListPanel miniPanel = newPanel.GetComponent<ScrollListPanel>();

            miniPanel.SetPanel(mini, this);
        }
    }

    // Empty the ScrollList
    void EmptyGrid()
    {
        for (int i = 0; i < gridContent.transform.childCount; i++)
        {
            Destroy(gridContent.transform.GetChild(i).gameObject);
        }
    }
}
