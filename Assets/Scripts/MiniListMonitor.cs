using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used in the BrowseScene
public class MiniListMonitor : ScrollListMonitor<Mini>
{

    /*[SerializeField]
    GameObject prefabNewMiniPanel = null;*/

    protected override void Start()
    {
        // Load a panel templates
        prefabPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniPanel");
        prefabNewPanel = Resources.Load<GameObject>(@"Prefabs\prefabNewMiniPanel");

        referenceList = MiniCollection.Minis;  // used to populate the list

        base.Start();
    }

    /*public override void PopulateGrid()
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

    }*/


    
}
