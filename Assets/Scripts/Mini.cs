using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Mini : IComparable
public class Mini
{

    
    public MiniID ID { get; private set; }

    public string Name { get; set; } = "Unknown";

    public List<Sprite> Photos { get; private set; }

    public Sprite Thumbnail { get; private set; } = null;


    // long sortID;
    // public long SortID {get => sortID;}


    public Mini (string name)
    {
        Name = name;
        ID = new MiniID();
        Photos = new List<Sprite>();

    }


    /*public int CompareTo(Mini mini)
    {
        if (mini == null) return 1;

        // compare by id, creation id, or by user sort
    }*/

    // Override
    public override string ToString()
    {
        return Name + ID.IDNumber; 
    }
}
