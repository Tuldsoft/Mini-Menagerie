using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuName
{
    Menu,
    Browse,
    Descriptors,
    Tags,
    Encounter_Builder,
    Collection_Stats,
    Settings,
    Help,
    About,
    Quit,
    View_Mini,
    DescrPicker,
    NewDescr,
    NewDescrText,
    NewDescrCheckBox,
    NewDescrNumber,
    NewDescrTags,
    DescrDetails
}
public enum MenuType
{
    Main,
    Descr
}

static public class MenuManager
{

    static bool initialized = false;

    static public Dictionary<MenuName, string> MainMenuNames { get; private set; } 
    static public Dictionary<MenuName, string> NewDescrNames { get; private set; }

    static GameObject prefabMenu = null;
    static GameObject prefabDescrPicker = null;
    static GameObject prefabDescrDetails_TXT = null;
    static GameObject prefabDescrDetails_CHK = null;
    static GameObject prefabDescrDetails_NUM = null;
    static GameObject prefabDescrDetails_TAG = null;


    static GameObject PrefabDescrDetails
    {
        get
        {
            switch (Descriptor.ActiveDescr.Type)
            {
                case DescrType.Text:
                    return prefabDescrDetails_TXT;
                case DescrType.CheckBox:
                    return prefabDescrDetails_CHK;
                case DescrType.Number:
                    return prefabDescrDetails_NUM;
                case DescrType.Tags:
                    return prefabDescrDetails_TAG;
                default:
                    return prefabDescrDetails_TXT;
            }
        }
    }


    static bool menuOpen = false;

    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        InitializeNames();

        Mini.SetActiveMini(MiniCollection.Minis[0]);

        LoadPrefabs();
    }

    static void LoadPrefabs()
    {
        prefabMenu = Resources.Load<GameObject>(@"Prefabs\Menus\prefabMenu");
        prefabDescrPicker = Resources.Load<GameObject>(@"Prefabs\Menus\prefabDescrPicker");
        prefabDescrDetails_TXT = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrDetails_TXT");
        prefabDescrDetails_CHK = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrDetails_CHK");
        prefabDescrDetails_NUM = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrDetails_NUM");
        prefabDescrDetails_TAG = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrDetails_TAG");
    }
    
    // Switchboard for most menu transitions, either through moving to a new scene
    // or by instantiating a overlay menu prefab

    // SceneManager.LoadScene("BrowseScene");
    // Object.Instantiate(Resources.Load(@"MenuPrefabs\prefabAboutMenu"));

    public static void GoToMenu(MenuName choice)
    {
        switch (choice)
        {
            case MenuName.Menu:
                LaunchMenu();
                break;
            case MenuName.Browse:
                SceneManager.LoadScene("BrowseScene");
                break;

            case MenuName.Descriptors:
                SceneManager.LoadScene("DescriptorsScene");
                break;

            case MenuName.Tags:
                //SceneManager.LoadScene("DescriptorsScene");
                break;

            case MenuName.Encounter_Builder:
                //SceneManager.LoadScene("EncountersScene");
                break;

            case MenuName.Collection_Stats:
                //SceneManager.LoadScene("StatsScene");
                break;

            case MenuName.Help:
                // Object.Instantiate(Resources.Load(@"MenuPrefabs\prefabHelpMenu"));
                break;

            case MenuName.About:
                //SceneManager.LoadScene("AboutScene");
                break;

            case MenuName.Quit:
                // Closes the entire application, called from Main Menu or pause menu
                Application.Quit();
                Debug.Log("You have quit the game.");
                break;

            //Called from BrowseScene
            case MenuName.View_Mini:
                SceneManager.LoadScene("MiniScene");
                break;

            // Called from MiniScene
            case MenuName.DescrPicker:
                menuOpen = true;
                GameObject.Instantiate(prefabDescrPicker);
                break;


            // Called from DescrScene
            case MenuName.NewDescr:
                LaunchMenu(MenuType.Descr);
                break;

            // Probably should not be handled by the Menu Manager
            case MenuName.NewDescrText:
                Descriptor.CreateNew(DescrType.Text); // MenuMonitor and MenuItem take it from here
                break;
            case MenuName.NewDescrCheckBox:
                Descriptor.CreateNew(DescrType.CheckBox);
                break;
            case MenuName.NewDescrNumber:
                Descriptor.CreateNew(DescrType.Number);
                break;
            case MenuName.NewDescrTags:
                Descriptor.CreateNew(DescrType.Tags);
                break;

            case MenuName.DescrDetails:
                menuOpen = true;
                GameObject.Instantiate(PrefabDescrDetails);
                break;

            default:
                Debug.Log("Unknown menu command.");
                break;
        }
    }

    // Initializes "packs" of MenuNames for different MenuTypes
    static void InitializeNames()
    {
        MainMenuNames = new Dictionary<MenuName, string>();
        
        MainMenuNames.Add(MenuName.Browse, "Browse");
        MainMenuNames.Add(MenuName.Descriptors, "Descriptors");
        MainMenuNames.Add(MenuName.Tags, "Tags");
        MainMenuNames.Add(MenuName.Encounter_Builder, "Encounter Builder");
        MainMenuNames.Add(MenuName.Collection_Stats, "Collection Stats");
        MainMenuNames.Add(MenuName.Settings, "Settings");
        MainMenuNames.Add(MenuName.Help, "Help");
        MainMenuNames.Add(MenuName.About, "About");

        NewDescrNames = new Dictionary<MenuName, string>();

        NewDescrNames.Add(MenuName.NewDescrText, "Text");
        NewDescrNames.Add(MenuName.NewDescrCheckBox, "Check Box");
        NewDescrNames.Add(MenuName.NewDescrNumber, "Number");
        NewDescrNames.Add(MenuName.NewDescrTags, "Tags");
    }

    static void LaunchMenu(MenuType type = MenuType.Main)
    {
        menuOpen = true;
        GameObject menu = GameObject.Instantiate(prefabMenu);
        menu.GetComponentInChildren<MenuMonitor>().SetMenu(type);
    }

    static public void CloseMenu(GameObject menuObj)
    {
        Object.Destroy(menuObj);
        menuOpen = false;
    }

    static public bool MenuOpen()
    {
        return menuOpen;
    }
}




