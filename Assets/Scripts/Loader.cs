using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Loads Panel prefabs from Resources, supplies the correct one via GetPanel()
static public class Loader
{
    static bool initialized = false;

    // Mini panels
    static public GameObject MiniPanel { get; private set; }
    static public GameObject MiniPanel_New { get; private set; }

    // Thumbpanel
    static public GameObject ThumbPanel { get; private set; }

    // MiniTraitListPanels
    static public Dictionary<TraitType, GameObject> MiniTraitPanels { get; private set; }
    static GameObject miniTraitPanel_TXT = null;
    static GameObject miniTraitPanel_CHK = null;
    static GameObject miniTraitPanel_NUM = null;
    static GameObject miniTraitPanel_TAG = null;

    static public GameObject MiniTraitPanel_New { get; private set; }

    // TraitPanel (also used in TraitPicker)
    static public GameObject TraitPanel { get; private set; }

    // Used by ShowHideMonitor
    static public GameObject ShowHidePanel { get; private set; }


    static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        // Mini panels
        MiniPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniPanel");
        MiniPanel_New = Resources.Load<GameObject>(@"Prefabs\prefabNewMiniPanel");

        // Thumb panel
        ThumbPanel = Resources.Load<GameObject>(@"Prefabs\prefabMiniThumbnail"); 
        
        // Mini Descr Panels
        miniTraitPanel_TXT = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabTraitPanel_TXT");
        miniTraitPanel_CHK = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabTraitPanel_CHK");
        miniTraitPanel_NUM = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabTraitPanel_NUM");
        //miniTraitPanel_TAG = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabTraitPanel_TAG");

        MiniTraitPanels = new Dictionary<TraitType, GameObject>();
        MiniTraitPanels.Add(TraitType.TXT, miniTraitPanel_TXT);
        MiniTraitPanels.Add(TraitType.CHK, miniTraitPanel_CHK);
        MiniTraitPanels.Add(TraitType.NUM, miniTraitPanel_NUM);
        MiniTraitPanels.Add(TraitType.TAG, miniTraitPanel_TAG);

        MiniTraitPanel_New = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabNewTraitPanel");

        // Trait Panel (Trait scene and Trait Picker)
        TraitPanel = Resources.Load<GameObject>(@"Prefabs\TraitScene\prefabTraitPanel");

        ShowHidePanel = Resources.Load<GameObject>(@"Prefabs\Menus\prefabShowHidePanel");
    }


    // listItem is null (default) when a "new" panel is created
    static public GameObject GetPanel<T>(ScrollListMonitor<T> monitor, T listItem) where T : IComparable<T>
    {
        if (monitor is MiniTraitListMonitor)
        {
            Trait trait = listItem as Trait;
            return (trait is null ? MiniTraitPanel_New : MiniTraitPanels[trait.Type]);
        }

        if (monitor is TraitListMonitor
            || monitor is TraitPickerMonitor)
            return TraitPanel;

        // unused, see overload below
        if (monitor is ThumbListMonitor)
            return ThumbPanel;

        if (monitor is MiniListMonitor)
            return (listItem is null ? MiniPanel_New : MiniPanel);

        if (monitor is ShowHideListMonitor)
            return ShowHidePanel;

        return null;
    }

    // overload for retrieving the ThumbPanel
    static public GameObject GetPanel(ScrollListMonitor<Mini> monitor)
    {
        if (monitor is ThumbListMonitor)
            return ThumbPanel;

        return null;
    }
}
