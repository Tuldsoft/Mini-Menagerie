using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DescrType
{
    Text,
    CheckBox,
    Number,
    Tags
}

public class Descriptor : IComparable<Descriptor>
{
    const string defaultName = "Descriptor Name";
    const string defaultName_TXT = "Text Name";
    const string defaultName_CHK = "Checkbox Name";
    const string defaultName_NUM = "Number Name";
    const string defaultName_TAG = "Tags Name";

    class NameType : IEquatable<NameType>
    {
        public DescrType Type { get; set; }
        
        string _name;
        public string Name { 
            get => suffix <= 0 ? _name : _name + " " + suffix; 
            set => ValidateName(value); }

        int suffix = 0;

        public NameType(string name = defaultName, DescrType type = DescrType.Text)
        {
            this.Name = name; // Triggers ValidateName()
            this.Type = type;
        }

        void ValidateName(string checkName)
        {
            if (Name == checkName) return;
            
            List<string> names = new List<string>();

            foreach (Descriptor descr in Descriptor.List)
            {
                if (Type == descr.Type)
                    names.Add(descr.Name);
            }
            
            _name = checkName;
            suffix = 0;

            while (names.Contains(Name))
            {
                suffix++;
            }
            
        }

        public bool Equals(NameType comparison)
        {
            if (Name == comparison.Name && Type == comparison.Type)
                return true;

            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    // Stores Name and Type
    NameType nameType;

    // Get from NameType
    public string Name { 
        get => nameType.Name;
        set => nameType.Name = value;
    }  

    // Get from NameType
    public DescrType Type { get => nameType.Type; set => nameType.Type = value; }
    // public Sprite TypeSprite { get => GetSprite(Type); } // moved to ImageUtils

    public bool AlwaysShow { get; set; } = false;

    // Stores all Descriptors created, used in populating ScrollLists.
    // Later, implement a DescriptorCollection class, like the MiniCollection class
    static List<Descriptor> masterList = new List<Descriptor>();
    static public List<Descriptor> List { get => masterList; }
    static public Descriptor ActiveDescr { get; private set; }

    // Constructor
    public Descriptor (string name = defaultName, DescrType type = DescrType.Text)
    {
        this.nameType = new NameType(name, type); // stores both Name (edited) and Type
        masterList.Add(this);
    }

    // IComparable implementation: Compare NameType
    public int CompareTo(Descriptor descr)
    {
        // for now, just compare names. Later, implement user sorting.
        return Name.CompareTo(descr.Name);
    }

    static public void RemoveDescriptor(Descriptor descriptor)
    {
        masterList.Remove(descriptor);
    }

    static public void CreateNew(DescrType type)
    {
        switch (type)
        {
            case DescrType.Text:
                new Descriptor_Text(defaultName_TXT, type);
                break;
            case DescrType.CheckBox:
                new Descriptor_CheckBox(defaultName_CHK, type);
                break;
            case DescrType.Number:
                new Descriptor_Number(defaultName_NUM, type);
                break;
            case DescrType.Tags:
                //new Descriptor_Tags(defaultName_TAG, type);
                break;
            default:
                new Descriptor_Text(defaultName_TXT, type);
                break;
        }
    }

    static public void SetActive(Descriptor descr)
    {
        ActiveDescr = descr;
    }
    
}


public class Descriptor_Text : Descriptor
{
    public string DefaultText { get; set; } = "Default text";
    public string Text { get; set; } = "Default text";

    public Descriptor_Text(string name = "Descriptor Name", DescrType type = DescrType.Text)
        : base(name, DescrType.Text) { }
}

public class Descriptor_CheckBox : Descriptor
{
    public bool DefaultIsChecked { get; set; } = false;
    public bool IsChecked { get; set; } = false;

    public Descriptor_CheckBox(string name = "Descriptor Name", DescrType type = DescrType.Text)
        : base(name, DescrType.CheckBox) { }
}

public class Descriptor_Number : Descriptor
{
    
    enum NumIndex { Value = 0, Default = 1, Min = 2, Max = 3, Increment = 4 }

    readonly decimal?[] defaultNums = { null, null, null, null, 1 };
    decimal?[] nums;

    // Public Accessor properties to interface with Text UI objects and strings
    public string Value
    {
        get => GetNum(NumIndex.Value);
        set => SetNum(NumIndex.Value, value);
    }

    public string Default
    {
        get => GetNum(NumIndex.Default);
        set => SetNum(NumIndex.Default, value);
    }

    public string Min
    {
        get => GetNum(NumIndex.Min);
        set => SetNum(NumIndex.Min, value);
    }
    public string Max
    {
        get => GetNum(NumIndex.Max);
        set => SetNum(NumIndex.Max, value);
    }
    public string Increment
    {
        get => GetNum(NumIndex.Increment);
        set => SetNum(NumIndex.Increment, value);
    }

    private int _precision = 0;
    // Number of places after the decimal point
    public int Precision
    {
        get => _precision;
        set => _precision = Math.Max(Math.Min(value, MaxPrecision), 0); // clamp 0 to 5
    }
    const int MaxPrecision = 5;
    private string precisionString { get => String.Format("N{0}", Precision); }



    // Constructor
    public Descriptor_Number(string name = "Descriptor Name", DescrType type = DescrType.Text)
        : base(name, DescrType.Number)
    {
        Precision = 0;
        nums = new decimal?[Enum.GetNames(typeof(NumIndex)).Length];
        // EDIT: add try statement for safety, in case array sizes don't line up
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = defaultNums[i];
        }
    }

    // Get and Set nums using strings
    string GetNum(NumIndex numIndex)
    {
        decimal? result = nums[(int)numIndex];

        if (result == null) 
            return String.Empty;

        return ((decimal)result).ToString(precisionString);
    }

    void SetNum(NumIndex numIndex, string s)
    {
        if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
        {
            nums[(int)numIndex] = null;
        }
        else
        {
            decimal? newValue = StringToDecimal(s); // returns null if parse fails
            if (newValue == null)
                return;                             // do not change the value for a bad string

            if (numIndex == NumIndex.Value ||
                numIndex == NumIndex.Default)
                Clamp(ref newValue);
            
            nums[(int)numIndex] = newValue;
        }
    }
    
    // Returns null if unparsable, does not check string for null
    decimal? StringToDecimal(string s)
    {
        if (Decimal.TryParse(s, out decimal newValue))
        {
            newValue = Math.Round(newValue, Precision);
            return newValue;
        }
        else
        {
            return null;
        }
    }

    // Clamp a decimal? value
    void Clamp(ref decimal? num)
    {
        decimal? min = nums[(int)NumIndex.Min];
        decimal? max = nums[(int)NumIndex.Max];

        num = num < min ? min : num; // < and > work for nullable types
        num = num > max ? max : num; // null < any value
    }

    // Increment Value
    public void Increase()
    {
        if (nums[(int)NumIndex.Increment] != null)
        {
            nums[(int)NumIndex.Value] ??= 0;
            nums[(int)NumIndex.Value] += nums[(int)NumIndex.Increment];
            Clamp(ref nums[(int)NumIndex.Value]);
        }
    }

    // Decrement Value
    public void Decrease()
    {
        if (nums[(int)NumIndex.Increment] != null)
        {
            nums[(int)NumIndex.Value] ??= 0;
            nums[(int)NumIndex.Value] -= nums[(int)NumIndex.Increment];
            Clamp(ref nums[(int)NumIndex.Value]);
        }
    }
}


