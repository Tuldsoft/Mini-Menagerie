using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used in the BrowseScene
public class MiniListMonitor : ScrollListMonitor<Mini>
{
    protected override IEnumerable<Mini> ReferenceList { get => MiniCollection.Minis; }

    protected override void Start()
    {
        // Load a panel templates
        //prefabPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniPanel");
        //prefabNewPanel = Resources.Load<GameObject>(@"Prefabs\prefabNewMiniPanel");
        newAsLastPanel = true;

        base.Start();
    }

    
}
