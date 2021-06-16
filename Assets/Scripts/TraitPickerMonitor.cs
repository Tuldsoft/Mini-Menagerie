using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TraitPickerMonitor : ScrollListMonitor<Trait>
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        // Load panel template
        //prefabPanel = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel");

        keepFirstPanel = false;

        // only load picker with Descriptors that are not already in this Mini's List
        IEnumerable<Trait> partialList =
            from trait in Trait.List
            where !Mini.ActiveMini.Traits.List.Contains(trait)
            select trait;

        referenceList = partialList;

        base.Start();
    }

}
