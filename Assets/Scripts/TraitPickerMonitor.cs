using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TraitPickerMonitor : ScrollListMonitor<Trait>
{

    // Overrides ReferenceList for ScrollListMonitor
    protected override IEnumerable<Trait> ReferenceList
    { get { return from trait in Trait.List
                   where !Mini.ActiveMini.Traits.List.Contains(trait)
                   select trait; } }


    // Start is called before the first frame update
    protected override void Start()
    {
        keepFirstPanel = false;

        base.Start();
    }

}
