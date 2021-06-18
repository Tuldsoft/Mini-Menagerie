using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A container of Traits, used by each Mini and by Trait
public class TraitCollection 
{

    public List<Trait> List { get; private set; } = new List<Trait>();

    // Succeed or fail at Add()
    public bool AddTrait(Trait trait)
    {
        // Unique only
        if (List.Contains(trait))
            return false;
            
        
        List.Add(trait);
        return true;
    }

    // Succeed or fail at Remove()
    public bool RemoveTrait(Trait trait)
    {
        return List.Remove(trait);
    }

    
    // Redo this so that a Mini's collection is exclusive, not handled by copy constructor alone

    // see Trait.Copy(Trait trait)
    /*public Trait CopyTrait(Trait trait)
    {
        // Only copy Descriptors in the Collection
        if (!List.Contains(trait)) return null;

        // Make sure we are using the one from the List
        trait = List[List.IndexOf(trait)];

        Trait copy;

        switch (trait.Type)
        {
            case TraitType.TXT:
                copy = new Trait_TXT(trait as Trait_TXT);
                break;
            case TraitType.CHK:
                copy = new Trait_CHK(trait as Trait_CHK);
                break;
            case TraitType.NUM:
                copy = new Trait_NUM(trait as Trait_NUM);
                break;
            case TraitType.TAG:
                copy = new Trait_TAG(trait as Trait_TAG);
                break;
            default:
                copy = new Trait_TXT();
                break;
        }

        return copy;
    }*/

    // Empty constructor
    public TraitCollection() { }

    // Copy Constructor overload, used when creating a new Mini?
    public TraitCollection(TraitCollection refColl, bool includeAll = true)
    {

        foreach (Trait trait in refColl.List)
        {
            if (includeAll)
            {
                if (trait.IncludeAll)
                    AddTrait(Trait.Copy(trait));
            }
            else
            {
                AddTrait(Trait.Copy(trait));
            }
        }
    }


}
