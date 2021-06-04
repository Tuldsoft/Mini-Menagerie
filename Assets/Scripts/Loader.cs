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

    // MiniDescrListPanels
    static public Dictionary<DescrType, GameObject> MiniDescrPanels { get; private set; }
    static GameObject miniDescrPanel_TXT = null;
    static GameObject miniDescrPanel_CHK = null;
    static GameObject miniDescrPanel_NUM = null;
    static GameObject miniDescrPanel_TAG = null;

    static public GameObject MiniDescrPanel_New { get; private set; }

    // DescrPanel (also used in DescrPicker)
    static public GameObject DescrPanel { get; private set; }

    


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
        miniDescrPanel_TXT = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_Text");
        miniDescrPanel_CHK = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_CheckBox");
        miniDescrPanel_NUM = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_Number");
        //miniDescrPanel_TAG = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_Tags");

        MiniDescrPanels = new Dictionary<DescrType, GameObject>();
        MiniDescrPanels.Add(DescrType.Text, miniDescrPanel_TXT);
        MiniDescrPanels.Add(DescrType.CheckBox, miniDescrPanel_CHK);
        MiniDescrPanels.Add(DescrType.Number, miniDescrPanel_NUM);
        MiniDescrPanels.Add(DescrType.Tags, miniDescrPanel_TAG);

        MiniDescrPanel_New = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabNewDescrPanel");

        // Descr Panel (Descr scene and Descr Picker)
        DescrPanel = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel");

    }


    // listItem is null (default) when a "new" panel is created
    static public GameObject GetPanel<T>(ScrollListMonitor<T> monitor, T listItem) where T : IComparable<T>
    {
        if (monitor is MiniDescrListMonitor)
        {
            Descriptor descr = listItem as Descriptor;
            return (descr is null ? MiniDescrPanel_New : MiniDescrPanels[descr.Type]);
        }

        if (monitor is DescrListMonitor
            || monitor is DescrPickerMonitor)
            return DescrPanel;

        // unused, see overload below
        if (monitor is ThumbListMonitor)
            return ThumbPanel;

        if (monitor is MiniListMonitor)
            return (listItem is null ? MiniPanel_New : MiniPanel);


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
