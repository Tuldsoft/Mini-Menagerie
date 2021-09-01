﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MenuName
{
    Menu,
    Browse,
    Traits,
    Tags,
    Encounter_Builder,
    Collection_Stats,
    Settings,
    Help,
    About,
    Quit,
    View_Mini,
    TraitPicker,
    NewTrait,
    NewTraitTXT,
    NewTraitCHK,
    NewTraitNUM,
    NewTraitTAG,
    TraitDetails,
    ShowHide
}
public enum MenuType
{
    Main,
    Trait
}

static public class MenuManager
{

    static bool initialized = false;

    static public Dictionary<MenuType, Dictionary<object, string>> MenuNamesDict { get; private set; }

    static GameObject prefabMenu = null;
    //static GameObject prefabDialog = null;
    static GameObject prefabPopup = null;
    static GameObject prefabTraitPicker = null;
    static GameObject prefabTraitDetails_TXT = null;
    static GameObject prefabTraitDetails_CHK = null;
    static GameObject prefabTraitDetails_NUM = null;
    static GameObject prefabTraitDetails_TAG = null;
    static GameObject prefabShowHideMenu = null;

    static GameObject PrefabTraitDetails
    {
        get
        {
            switch (Trait.ActiveTrait.TType)
            {
                case TraitType.TXT:
                    return prefabTraitDetails_TXT;
                case TraitType.CHK:
                    return prefabTraitDetails_CHK;
                case TraitType.NUM:
                    return prefabTraitDetails_NUM;
                case TraitType.TAG:
                    return prefabTraitDetails_TAG;
                default:
                    return prefabTraitDetails_TXT;
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
        //prefabDialog = Resources.Load<GameObject>(@"Prefabs\Menus\prefabDialog");
        prefabPopup = Resources.Load<GameObject>(@"Prefabs\Menus\prefabPopup");
        prefabTraitPicker = Resources.Load<GameObject>(@"Prefabs\Menus\prefabTraitPicker");
        prefabTraitDetails_TXT = Resources.Load<GameObject>(@"Prefabs\TraitScene\prefabTraitDetails_TXT");
        prefabTraitDetails_CHK = Resources.Load<GameObject>(@"Prefabs\TraitScene\prefabTraitDetails_CHK");
        prefabTraitDetails_NUM = Resources.Load<GameObject>(@"Prefabs\TraitScene\prefabTraitDetails_NUM");
        prefabTraitDetails_TAG = Resources.Load<GameObject>(@"Prefabs\TraitScene\prefabTraitDetails_TAG");
        prefabShowHideMenu = Resources.Load<GameObject>(@"Prefabs\Menus\prefabShowHideMenu");
    }
    
    // Switchboard for most menu transitions, either through moving to a new scene
    // or by instantiating a overlay menu prefab

    // SceneManager.LoadScene("BrowseScene");
    // Object.Instantiate(Resources.Load(@"MenuPrefabs\prefabAboutMenu"));

    // Calls a LoadScene, a submenu prefab or an async submenu
    public static async void GoToMenu(MenuName choice)
    {
        switch (choice)
        {
            case MenuName.Menu:
                if (await LaunchMenuAsync(MenuType.Main) is MenuName nextMenu)
                    GoToMenu(nextMenu);
                break;
            case MenuName.Browse:
                SceneManager.LoadScene("BrowseScene");
                break;

            case MenuName.Traits:
                SceneManager.LoadScene("TraitsScene");
                break;

            case MenuName.Tags:
                //SceneManager.LoadScene("TagsScene");
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
            case MenuName.TraitPicker:
                menuOpen = true;
                GameObject.Instantiate(prefabTraitPicker);
                break;

            /*
            // Called from TraitScene
            case MenuName.NewTrait:
                LaunchMenu(MenuType.Trait);
                break;

            // Probably should not be handled by the Menu Manager
            case MenuName.NewTraitTXT:
                Trait.CreateNew(TraitType.TXT); // MenuMonitor and MenuItem take it from here
                break;
            case MenuName.NewTraitCHK:
                Trait.CreateNew(TraitType.CHK);
                break;
            case MenuName.NewTraitNUM:
                Trait.CreateNew(TraitType.NUM);
                break;
            case MenuName.NewTraitTAG:
                Trait.CreateNew(TraitType.TAG);
                break;*/

            case MenuName.TraitDetails:
                menuOpen = true;
                GameObject.Instantiate(PrefabTraitDetails);
                break;

            case MenuName.ShowHide:
                menuOpen = true;
                GameObject.Instantiate(prefabShowHideMenu);
                break;

            default:
                Debug.Log("Unknown menu command.");
                break;
        }
    }

    // Initializes "packs" of MenuNames for different MenuTypes
    static void InitializeNames()
    {
        Dictionary<object, string> MainMenuNames = new Dictionary<object, string>();
        
        MainMenuNames.Add(MenuName.Browse, "Browse");
        MainMenuNames.Add(MenuName.Traits, "Traits");
        MainMenuNames.Add(MenuName.Tags, "Tags");
        MainMenuNames.Add(MenuName.Encounter_Builder, "Encounter Builder");
        MainMenuNames.Add(MenuName.Collection_Stats, "Collection Stats");
        MainMenuNames.Add(MenuName.Settings, "Settings");
        MainMenuNames.Add(MenuName.Help, "Help");
        MainMenuNames.Add(MenuName.About, "About");

        Dictionary<object, string> NewTraitNames = new Dictionary<object, string>();

        NewTraitNames.Add(TraitType.TXT, "Text");
        NewTraitNames.Add(TraitType.CHK, "Check Box");
        NewTraitNames.Add(TraitType.NUM, "Number");
        NewTraitNames.Add(TraitType.TAG, "Tags");

        MenuNamesDict = new Dictionary<MenuType, Dictionary<object, string>>();
        MenuNamesDict.Add(MenuType.Main, MainMenuNames);
        MenuNamesDict.Add(MenuType.Trait, NewTraitNames);



    }

    // Replaced by asnyc version
    /*static void LaunchMenu(MenuType type = MenuType.Main)
    {
        menuOpen = true;
        GameObject menu = GameObject.Instantiate(prefabMenu);
        menu.GetComponentInChildren<MenuMonitor>().SetMenu(type);
    }*/

    // Replaced by LaunchPopupAsync
    /*static public void LaunchDialogBox(DialogBoxType dbType, string message)
    {
        DialogBoxMonitor.DBType = dbType;
        DialogBoxMonitor.Message = message;
        menuOpen = true;

        GameObject menu = GameObject.Instantiate(prefabDialog);
        // no SetBox() required this time
    }*/

    static public async Task<PopupResult> LaunchPopupAsync(PopupType pType, string message, CancellationToken ct = default)
    {
        GameObject menu = GameObject.Instantiate(prefabPopup);
        PopupMonitor monitor = menu.GetComponent<PopupMonitor>();
        PopupResult result = await monitor.SetPopupAsync(pType, message, ct);

        // once result is obtained, destroy the menu
        GameObject.Destroy(menu);

        return result;
    }

    /*static public async Task<DialogBoxResponse> DialogBoxQueryAsync (DialogBoxType dbType, string message)
    {
        GameObject db = GameObject.Instantiate(prefabDialog);
        return await db.GetComponent<DialogBoxMonitor>().RunDBAsync(dbType, message);

    }*/

    static public async Task<object> LaunchMenuAsync(MenuType mType, CancellationToken ct = default)
    {
        GameObject picker = GameObject.Instantiate(prefabMenu);
        MenuMonitor monitor = picker.GetComponent<MenuMonitor>();
        
        
        object result = await monitor.SetMonitorAsync(mType, ct);
        if (result == null) return null; // Close returns null


        return mType switch
        {
            MenuType.Main => (MenuName)result,
            MenuType.Trait => Trait.CreateNew((TraitType)result),
            _ => null,
        };

    }

    static public async Task<object> Button_ClickAsync(Button button, object result, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested(); // necessary?

        bool isPressed = false;
        button.onClick.AddListener(() => isPressed = true);

        //Debug.Log($"Button {button.name} gives result of {pResult} and is on {button.gameObject.name}");

        while (!isPressed)
        {
            await Task.Yield(); // let someone else take over, unless pressed
        }

        // this only is reached if it is the first task to complete
        return result;
    }
    
    // Called by close buttons. menuObj is a prefab menu object
    static public void CloseMenu(GameObject menuObj)
    {
        UnityEngine.Object.Destroy(menuObj);
        menuOpen = false;
    }

    // TODO: Remove below three methods after coroutine version has been fully replaced
    //   MiniScene ShowHide submenu still uses coroutine version
    //   NewTraitListPanel still uses coroutine version (adding a trait to a Mini)
    static public bool MenuOpen()
    {
        return menuOpen;
    }

    static public IEnumerator WaitForClose(Action action)
    {
        yield return new WaitWhile(MenuManager.MenuOpen);
        action();
        yield return null;
    }

    
}




