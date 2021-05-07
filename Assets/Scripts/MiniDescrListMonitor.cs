using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDescrListMonitor : ScrollListMonitor<Descriptor>
{
    /*[SerializeField]
    GameObject prefabNewDescrPanel = null;*/

    [SerializeField]
    public DescrType defaultNewType;

    GameObject templatePanel_Text = null;
    GameObject templatePanel_CheckBox = null;
    GameObject templatePanel_Number = null;
    GameObject templatePanel_Tags = null;

    protected override void Start()
    {
        keepFirstPanel = true; // first panel is thumbnail carousel
        referenceList = Mini.ActiveMini.Descriptors; // used to populate the list

        // Load panel templates
        templatePanel_Text = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_Text");
        templatePanel_CheckBox = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_CheckBox");
        templatePanel_Number = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_Number");
        //templatePanel_Tags = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabDescrPanel_Tags");
        
        prefabNewPanel = Resources.Load<GameObject>(@"Prefabs\MiniScene\prefabNewDescrPanel");

        if (defaultNewType == DescrType.Tags)
            prefabPanel = templatePanel_Tags;
        else if (defaultNewType == DescrType.CheckBox)
            prefabPanel = templatePanel_CheckBox;
        else if (defaultNewType == DescrType.Number)
            prefabPanel = templatePanel_Number;
        else 
            prefabPanel = templatePanel_Text;

        base.Start();


    }

    /*public override void PopulateGrid()
    {
        EmptyGrid(); // keep first panel (prefabMiniThumbnail)

        GameObject newPanel;

        foreach (Descriptor descriptor in Mini.ActiveMini.Descriptors)
        {
            newPanel = Instantiate(prefabPanel, gridContent.transform);
            ScrollListPanel descrPanel = newPanel.GetComponent<ScrollListPanel>();

            descrPanel.SetPanel(descriptor, this);
        }

        newPanel = Instantiate(prefabNewDescrPanel, gridContent.transform);
        ScrollListPanel listPanel = newPanel.GetComponent<ScrollListPanel>();
        listPanel.SetPanel(null, this);
    }*/
}
