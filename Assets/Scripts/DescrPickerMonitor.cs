using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DescrPickerMonitor : ScrollListMonitor<Descriptor>
{
    
    // Start is called before the first frame update
    protected override void Start()
    {
        // Load panel template
        prefabPanel = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel");

        keepFirstPanel = false;

        // only load picker with Descriptors that are not already in this Mini's List
        IEnumerable<Descriptor> partialList =
            from descr in Descriptor.List
            where !Mini.ActiveMini.Descriptors.List.Contains(descr)
            select descr;

        referenceList = partialList;

        base.Start();
    }

}
