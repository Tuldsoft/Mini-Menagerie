using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThumbListMonitor : ScrollListMonitor
{
    [SerializeField]
    public Image BigThumb = null;

    protected override void Start()
    {
        // Load a shop panel template
        prefabPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniThumbnail");

        keepFirstPanel = false;

        base.Start();

    }



    public override void PopulateGrid()
    {
        EmptyGrid();

        GameObject newPanel;

        foreach (Sprite sprite in Mini.ActiveMini.Photos)
        {
            newPanel = Instantiate(prefabPanel, gridContent.transform);
            ScrollListPanel thumbPanel = newPanel.GetComponent<ScrollListPanel>();

            thumbPanel.SetPanel(sprite, this);
        }

    }
}
