using System;
using System.Collections;
using System.Collections.Generic;

public class MiniID
{
    Guid id;

    public string ID { get => id.ToString(); private set => ID = value; }
        
    public long IDNumber { get; private set; }
            
    
    static public long TotalCreated { get; private set; }


    // Constructor
    public MiniID()
    {
        SetID(Guid.NewGuid());
        TotalCreated++;
        IDNumber = TotalCreated;
    }

    // Methods
    void SetID(Guid newGuid)
    {
        id = newGuid;
    }

    /*public string CreateID ()
    {
        Guid newGuid = Guid.NewGuid();
        return newGuid.ToString();
    }*/
}
