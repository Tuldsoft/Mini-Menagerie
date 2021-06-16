using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTraitListMonitor : ScrollListMonitor<Trait>
{
    // this monitor no longer loads individual panels, this is done once by the Loader.

    protected override void Start()
    {
        keepFirstPanel = true; // first panel is thumbnail carousel
        newAsLastPanel = true;
        referenceList = Mini.ActiveMini.Traits.List; // used to populate the list

        base.Start();


    }

    
}
