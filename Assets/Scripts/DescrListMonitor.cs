using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescrListMonitor : ScrollListMonitor<Descriptor>
{

    // debugging only
    [SerializeField]
    public DescrType defaultNewType;

    GameObject templatePanel_Text = null;
    GameObject templatePanel_CheckBox = null;
    GameObject templatePanel_Number = null;
    GameObject templatePanel_Tags = null;

    protected override void Start()
    {
        // Load panel templates
        //templatePanel_Text = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel_Text");
        //templatePanel_CheckBox = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel_CheckBox");
        //templatePanel_Number = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel_Number");
        //templatePanel_Tags = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabDescrPanel_Tags");

        // rather than use a panel, have a dedicated New button at the top
        //prefabNewDescrPanel = Resources.Load<GameObject>(@"Prefabs\DescrScene\prefabNewDescrPanel");

        if (defaultNewType == DescrType.Tags)
            prefabPanel = templatePanel_Tags;
        else if (defaultNewType == DescrType.CheckBox)
            prefabPanel = templatePanel_CheckBox;
        else if (defaultNewType == DescrType.Number)
            prefabPanel = templatePanel_Number;
        else
            prefabPanel = templatePanel_Text;

        keepFirstPanel = false;
        referenceList = Descriptor.List;

        base.Start();

    }

    /*public override void PopulateGrid()
    {
        EmptyGrid(); // do not keep any first panel 

        GameObject newPanel;

        foreach (Descriptor descriptor in Descriptor.List)
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
