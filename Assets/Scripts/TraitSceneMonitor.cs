//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
using UnityEngine;


public class TraitSceneMonitor : MonoBehaviour
{
    [SerializeField]
    TraitListMonitor listMonitor = null;
    
    // Awake is called before Start()
    private void Awake()
    {
        Initializer.Initialize();
    }

    public void Menu_Click()
    {
        MenuManager.GoToMenu(MenuName.Menu);
    }


    // Attached to New button. Launches a menu and uses a task return from the async launch
    //   to create a new trait panel. Async version that replaces coroutine-based version.
    public async void New_Click()
    {
        if (await MenuManager.LaunchMenuAsync(MenuType.Trait) is Trait newTrait)
            listMonitor.AddToGrid(newTrait, Loader.GetPanel<Trait>(listMonitor, newTrait));

        // don't need to fully reset the grid anymore because async returns an object
        //listMonitor.PopulateGrid(true);
    }

    
}
