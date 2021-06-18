using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowHideListMonitor : ScrollListMonitor<Trait>
{
    protected override IEnumerable<Trait> ReferenceList { get => Mini.ActiveMini.Traits.List; }
    
    // Start is called before the first frame update
    protected override void Start()
    {
        keepFirstPanel = false;

        base.Start();
    }
}
