using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Loads and holds references to all system images
static public class ImageUtils
{
    static bool initialized = false;

    readonly static public Dictionary<TraitType, Sprite> TraitTypeSprites = new Dictionary<TraitType, Sprite>();

    static public Sprite RadialGreen { get; private set; }

    static public Sprite Trash { get; private set; }



static public void Initialize()
    {
        if (initialized) return;

        initialized = true;

        TraitTypeSprites.Add(TraitType.TXT, Resources.Load<Sprite>(@"Images\TXT"));
        TraitTypeSprites.Add(TraitType.CHK, Resources.Load<Sprite>(@"Images\CHK"));
        TraitTypeSprites.Add(TraitType.NUM, Resources.Load<Sprite>(@"Images\NUM"));
        TraitTypeSprites.Add(TraitType.TAG, Resources.Load<Sprite>(@"Images\TAG"));

        RadialGreen = Resources.Load<Sprite>(@"Images\RadialGreen.png");
        Trash = Resources.Load<Sprite>(@"Images\trash.png");
    }



}
