using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DescrSceneMonitor : MonoBehaviour
{
    [SerializeField]
    DescrListMonitor listMonitor = null;
    
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
        MenuManager.GoToMenu(MenuName.NewDescr);
        StartCoroutine(WaitForClose());
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitWhile(MenuManager.MenuOpen);
        listMonitor.AddToGrid(Descriptor.List.Last());
        //listMonitor.PopulateGrid(); // replace with add to grid
        yield return null;
    }


}
