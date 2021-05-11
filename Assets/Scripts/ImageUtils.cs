using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Loads and holds references to all system images
static public class ImageUtils
{
    static bool initialized = false;

    readonly static public Dictionary<DescrType, Sprite> DescrTypeSprites = new Dictionary<DescrType, Sprite>();

    static public Sprite RadialGreen { get; private set; }

    static public Sprite Trash { get; private set; }



static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        DescrTypeSprites.Add(DescrType.Text, Resources.Load<Sprite>(@"Images\TXT"));
        DescrTypeSprites.Add(DescrType.CheckBox, Resources.Load<Sprite>(@"Images\CHK"));
        DescrTypeSprites.Add(DescrType.Number, Resources.Load<Sprite>(@"Images\NUM"));
        DescrTypeSprites.Add(DescrType.Tags, Resources.Load<Sprite>(@"Images\TAG"));

        RadialGreen = Resources.Load<Sprite>(@"Images\RadialGreen.png");
        Trash = Resources.Load<Sprite>(@"Images\trash.png");
    }



}
