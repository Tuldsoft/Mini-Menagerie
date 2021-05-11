using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescrListMonitor : ScrollListMonitor<Descriptor>
{
    
    protected override void Start()
    {
        // Load panel template
        prefabPanel = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel");

        keepFirstPanel = false;
        referenceList = Descriptor.List;

        base.Start();

    }
}
