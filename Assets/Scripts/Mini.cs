using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Mini : IComparable
public class Mini
{

    // Publicly accessible for all scripts to reference 
    static public Mini ActiveMini { get; private set; }
    
    public MiniID ID { get; private set; }

    public string Name { get; private set; } = "Unknown";

    public List<Sprite> Photos { get; private set; }

    public Sprite Thumbnail { get; private set; } = null;



    // long sortID;
    // public long SortID {get => sortID;}


    public Mini (string name)
    {
        Name = name;
        ID = new MiniID();
        Photos = new List<Sprite>();

        Sprite sampleImage = Resources.Load<Sprite>(@"Images\moustache bowler hat");
        Photos.Add(sampleImage);
        sampleImage = Resources.Load<Sprite>(@"Images\moustache bowler hat green");
        Photos.Add(sampleImage);
        sampleImage = Resources.Load<Sprite>(@"Images\moustache bowler hat blue");
        Photos.Add(sampleImage);

        Thumbnail = Photos[0];

    }


    /*public int CompareTo(Mini mini)
    {
        if (mini == null) return 1;

        return 0;
        // compare by id, creation id, or by user sort
    }*/

    static public void SetActiveMini(Mini mini)
    {
        ActiveMini = mini;
    }

    public void SetThumbnail(Sprite sprite)
    {
        Thumbnail = sprite;
    }

    public void Rename(string name)
    {
        // put string formatting Utils here.
        // Utils.FormatString(ref name)

        Name = name;
    }

    // Override
    public override string ToString()
    {
        return Name + ID.IDNumber; 
    }
}
