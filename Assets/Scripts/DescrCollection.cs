using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A container of Descriptors, used by each Mini and by Descriptor
public class DescrCollection 
{

    public List<Descriptor> List { get; private set; } = new List<Descriptor>();

    // Succeed or fail at Add()
    public bool AddDescr(Descriptor descr)
    {
        // Unique only
        if (List.Contains(descr)) 
            return false;
        
        List.Add(descr);
        return true;
    }

    // Succeed or fail at Remove()
    public bool RemoveDescr(Descriptor descr)
    {
        return List.Remove(descr);
    }

    public Descriptor CopyDescr(Descriptor descr)
    {
        // Only copy Descriptors in the Collection
        if (!List.Contains(descr)) return null;

        // Make sure we are using the one from the List
        descr = List[List.IndexOf(descr)];

        Descriptor copy;

        switch (descr.Type)
        {
            case DescrType.Text:
                copy = new Descriptor_Text(descr as Descriptor_Text);
                break;
            case DescrType.CheckBox:
                copy = new Descriptor_CheckBox(descr as Descriptor_CheckBox);
                break;
            case DescrType.Number:
                copy = new Descriptor_Number(descr as Descriptor_Number);
                break;
            case DescrType.Tags:
                copy = new Descriptor_Tags(descr as Descriptor_Tags);
                break;
            default:
                copy = new Descriptor_Text();
                break;
        }

        return copy;
    }

    // Empty constructor
    public DescrCollection() { }

    // Copy Constructor overload
    public DescrCollection(DescrCollection refColl, bool alwaysShow = true)
    {
        DescrCollection newColl = new DescrCollection();

        foreach (Descriptor descr in refColl.List)
        {
            if (alwaysShow)
            {
                if (descr.AlwaysShow)
                    newColl.AddDescr(CopyDescr(descr));
            }
            else
            {
                newColl.AddDescr(CopyDescr(descr));
            }
        }
    }


}
