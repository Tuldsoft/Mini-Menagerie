using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDescrListPanel : ScrollListPanel<Descriptor>
{

    // SetPanel() does not need overwriting

    public void NewButton_Click()
    {
        // Replace later with a Descr picker

        switch ((monitor as MiniDescrListMonitor).defaultNewType)
        {
            case DescrType.Text:
                Mini.ActiveMini.AddDescriptor(new Descriptor_Text());
                break;
            case DescrType.CheckBox:
                Mini.ActiveMini.AddDescriptor(new Descriptor_CheckBox());
                break;
            case DescrType.Number:
                Mini.ActiveMini.AddDescriptor(new Descriptor_Number());
                break;
            case DescrType.Tags:
                //Mini.ActiveMini.AddDescriptor(new Descriptor_Tags());
                break;
            default:
                Mini.ActiveMini.AddDescriptor(new Descriptor_Text());
                break;
        }

        monitor.PopulateGrid();
    }

}
