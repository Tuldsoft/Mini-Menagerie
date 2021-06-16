using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitListMonitor : ScrollListMonitor<Trait>
{

    protected override void Start()
    {
        // Load panel template
        //prefabPanel = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel");
        

        keepFirstPanel = false;
        referenceList = Trait.List;

        base.Start();
    }

    

}
