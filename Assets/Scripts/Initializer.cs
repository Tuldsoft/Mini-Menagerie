using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Initializer
{
    static public void Initialize()
    {
        Utils.Initialize();
        ImageUtils.Initialize();
        MiniCollection.Initialize();
        Descriptor.Initialize();
        MenuManager.Initialize();
        Loader.Initialize();

    }
}
