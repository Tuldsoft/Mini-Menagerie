using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniListMonitor : ScrollListMonitor
{

    [SerializeField]
    GameObject prefabNewMiniPanel = null;

    protected override void Start()
    {
        // Load a shop panel template
        prefabPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniPanel");
        prefabNewMiniPanel = Resources.Load<GameObject>(@"Prefabs\prefabNewMiniPanel");

        base.Start();


    }

    public override void PopulateGrid()
    {
        base.PopulateGrid();

        GameObject newPanel;

        newPanel = Instantiate(prefabNewMiniPanel, gridContent.transform);
        ScrollListPanel listPanel = newPanel.GetComponent<ScrollListPanel>();

        listPanel.SetPanel(null, this);

    }

}
