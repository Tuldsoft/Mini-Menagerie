using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniDescrListPanel : ScrollListPanel<Descriptor>
{
    protected Descriptor descriptor = null;

    [SerializeField]
    Text nameText = null;

    public override void SetPanel(object obj, ScrollListMonitor<Descriptor> monitor)
    {
        base.SetPanel(obj, monitor);

        if (obj is Descriptor setDescriptor)
        {
            descriptor = setDescriptor;
            nameText.text = descriptor.Name;

            //Utils.SetButtonColors(gameObject.GetComponentInChildren<Button>());
        }
    }

    protected bool IsNull(Descriptor descr)
    {
        bool test = (descr is null);
        if (test)
            Debug.Log("Descriptor is null.");
        return test;
    }
}
