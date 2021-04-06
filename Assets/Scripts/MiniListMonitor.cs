using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used in the BrowseScene
public class MiniListMonitor : ScrollListMonitor
{

    [SerializeField]
    GameObject prefabNewMiniPanel = null;

    protected override void Start()
    {
        // Load a panel template
        prefabPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniPanel");
        prefabNewMiniPanel = Resources.Load<GameObject>(@"Prefabs\prefabNewMiniPanel");

        base.Start();


    }

    public override void PopulateGrid()
    {
        EmptyGrid();

        GameObject newPanel;

        foreach (Mini mini in MiniCollection.Minis)
        {
            newPanel = Instantiate(prefabPanel, gridContent.transform);
            ScrollListPanel miniPanel = newPanel.GetComponent<ScrollListPanel>();

            miniPanel.SetPanel(mini, this);
        }

        newPanel = Instantiate(prefabNewMiniPanel, gridContent.transform);
        ScrollListPanel listPanel = newPanel.GetComponent<ScrollListPanel>();

        listPanel.SetPanel(null, this);

    }

}
