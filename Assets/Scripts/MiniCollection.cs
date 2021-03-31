using System.Collections;
using System.Collections.Generic;

static public class MiniCollection
{
    static bool initialized = false;


    static public List<Mini> Minis { get; private set; } = new List<Mini>();

    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        Minis.Add(new Mini("Bugbear Chief"));
        Minis.Add(new Mini("Criminal Mastermind"));
        Minis.Add(new Mini("Arwen"));
    }

    static public void NewMini (string name = "New Mini")
    {
        Minis.Add(new Mini(name));
    }

}
