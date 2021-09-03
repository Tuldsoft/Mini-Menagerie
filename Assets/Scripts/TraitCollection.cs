using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REFACTOR: HashSet is a better container for unique-only traits, should work
//   because Trait is IComparable

// A container of Traits, used by each Mini and by Trait
public class TraitCollection 
{

    public List<Trait> List { get; private set; } = new List<Trait>();

    // Succeed or fail at Add()
    public bool AddTrait(Trait trait)
    {
        // Unique only
        if (List.Contains(trait) || trait == null)
            return false;
            
        List.Add(trait);
        return true;
    }

    // Succeed or fail at Remove()
    public bool RemoveTrait(Trait trait)
    {
        return List.Remove(trait);
    }

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
