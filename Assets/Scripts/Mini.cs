using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Mini : IComparable
//public class Mini : IComparable<Mini>, IComparable
public class Mini : IComparable<Mini>
{

    // Publicly accessible for all scripts to reference 
    static public Mini ActiveMini { get; private set; }
    
    public MiniID ID { get; private set; }

    public string Name { get; private set; } = "Unknown";

    public List<Sprite> Photos { get; private set; }

    public Sprite Thumbnail { get; private set; } = null;

    public List<Descriptor> Descriptors { get; private set; } = new List<Descriptor>();

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

        //Descriptors.Add(new Descriptor_Text());

    }

    public int CompareTo(Mini mini)
    {
        // for now, just compare name strings
        return Name.CompareTo(mini.Name);
    }

    /*public int CompareTo(object obj)
    {
        if (obj != null && !(obj is Mini))
            throw new ArgumentException("Object is not a Mini.");
        
        // for now, just compare name strings
        return CompareTo(obj as Mini);
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

    public Descriptor AddDescriptor(Descriptor descr = null)
    {
        if (descr == null)
            descr = new Descriptor_Text();

        Descriptors.Add(descr);
                
        return descr;
    }

    // Override
    public override string ToString()
    {
        return Name + ID.IDNumber; 
    }
}
