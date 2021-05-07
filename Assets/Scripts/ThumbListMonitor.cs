using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Populates a ScrollList with Sprites, but a Sprite is no IComparable.
// Override of PopulateList() is required. "Mini" is a placeholder for T, but is unused.

public class ThumbListMonitor : ScrollListMonitor<Mini>
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

    // Sprite is not IComparable, so override
    public override void PopulateGrid()
    {
        EmptyGrid();

        GameObject newPanel;

        foreach (Sprite sprite in Mini.ActiveMini.Photos)
        {
            newPanel = Instantiate(prefabPanel, gridContent.transform);
            ScrollListPanel<Mini> thumbPanel = newPanel.GetComponent<ScrollListPanel<Mini>>();

            thumbPanel.SetPanel(sprite, this);
        }

    }
}
