using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitListMonitor : ScrollListMonitor<Trait>
{
    protected override IEnumerable<Trait> ReferenceList => Trait.List;

    protected override void Start()
    {

        keepFirstPanel = false;

        base.Start();
    }

    

}
