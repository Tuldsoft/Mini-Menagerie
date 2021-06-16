using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void New_Click()
    {
        //Descriptor.CreateNew(DescrType.Text); // swap for type selector
        MenuManager.GoToMenu(MenuName.NewTrait);
        StartCoroutine(WaitForClose());
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitWhile(MenuManager.MenuOpen);
        Trait trait = Trait.List.Last();
        listMonitor.AddToGrid(trait, Loader.GetPanel<Trait>(listMonitor, trait));
        //listMonitor.PopulateGrid(); // replace with add to grid
        yield return null;
    }


}
