using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniTraitListMonitor : ScrollListMonitor<Trait>
{

    protected override IEnumerable<Trait> ReferenceList { get
        {return from trait in Mini.ActiveMini.Traits.List
                where trait.Show
                select trait;
        } }

    protected override void Start()
    {
        keepFirstPanel = true; // first panel is thumbnail carousel
        newAsLastPanel = true;

        base.Start();


    }

    
}
