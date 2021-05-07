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
    Help,
    About,
    Quit,
    View_Mini
}

static public class MenuManager
{

    static bool initialized = false;

    static public Dictionary<MenuName, string> MenuNamesDict { get; private set; } 

    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        InitializeNames();

        Mini.SetActiveMini(MiniCollection.Minis[0]);

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
                Object.Instantiate(Resources.Load(@"Prefabs\Menus\prefabMenu"));
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

            case MenuName.View_Mini:
                SceneManager.LoadScene("MiniScene");
                break;

            default:
                Debug.Log("Unknown menu command.");
                break;
        }
    }

    static void InitializeNames()
    {
        MenuNamesDict = new Dictionary<MenuName, string>();
        
        MenuNamesDict.Add(MenuName.Browse, "Browse");
        MenuNamesDict.Add(MenuName.Descriptors, "Descriptors");
        MenuNamesDict.Add(MenuName.Tags, "Tags");
        MenuNamesDict.Add(MenuName.Encounter_Builder, "Encounter Builder");
        MenuNamesDict.Add(MenuName.Collection_Stats, "Collection Stats");
        MenuNamesDict.Add(MenuName.Help, "Help");
        MenuNamesDict.Add(MenuName.About, "About");

    }
}




