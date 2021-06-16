using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add Initializer.Initialize() to the Awake method of any scene monitor
// Each static Initialize() method begins with "if (initialized) return;"
// Including in every scene allows all classes to be ready no matter 
//   which scene is begun in the Unity Editor.

static public class Initializer
{
    static bool initialized = false;

    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        Utils.Initialize();
        ImageUtils.Initialize();
        MiniCollection.Initialize();
        Trait.Initialize();
        MenuManager.Initialize();
        Loader.Initialize();
        DialogBoxMonitor.Initialize();

    }
}
