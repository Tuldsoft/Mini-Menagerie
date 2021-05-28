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

public class DescrID
{
    Guid id;

    public string ID { get => id.ToString(); }

    public long IDNumber { get; private set; }

    static public long TotalCreated { get; private set; }


    // Constructor
    public DescrID()
    {
        SetID(Guid.NewGuid());
        TotalCreated++;
        IDNumber = TotalCreated;
    }

    // Methods
    void SetID(Guid newGuid)
    {
        id = newGuid;
    }

    /*public string CreateID ()
    {
        Guid newGuid = Guid.NewGuid();
        return newGuid.ToString();
    }*/
}

public class Descriptor : IComparable<Descriptor>, IEquatable<Descriptor>
{
    const string defaultName = "Descriptor Name";
    const string defaultName_TXT = "Text Name";
    const string defaultName_CHK = "Checkbox Name";
    const string defaultName_NUM = "Number Name";
    const string defaultName_TAG = "Tags Name";

    public Guid ID { get; private set; } 

    public string Name { get; private set; }

    public DescrType Type { get; set; }

    public bool AlwaysShow { get; set; } = false;
    public bool IsMaster { get; private set; } = true;


    // Stores all Descriptors created, used in populating ScrollLists.
    // Later, implement a DescriptorCollection class, like the MiniCollection class
    static bool initialized = false;

    static DescrCollection collection = new DescrCollection();
    static public List<Descriptor> List { get => collection.List; }
    static public IEnumerable<string> Names { 
        get
        {
            IEnumerable<string> names =
            from descr in List
            select descr.Name;

            return names;
        } 
    }


    static public Descriptor ActiveDescr { get; private set; }

    // Constructor for unique Descriptors
    public Descriptor (string name = defaultName, DescrType type = DescrType.Text)
    {
        int i = 1;
        string testName = name;
        while (!Rename(testName))
        {
            testName = $"{name} ({i++})";
        }

        Type = type;
        ID = Guid.NewGuid();
        if (collection.AddDescr(this))
            IsMaster = true; // Adds unique descr to the static collection
    }

    // Copy Constructor for putting Descriptors on Minis
    public Descriptor (Descriptor descr)
    {
        this.Name = descr.Name;
        this.Type = descr.Type;
        this.ID = descr.ID;
        this.AlwaysShow = descr.AlwaysShow;
        this.IsMaster = false;
    }

    // IComparable implementation: Compare NameType
    public int CompareTo(Descriptor descr)
    {
        // Later, implement user-defined sorting.
        
        // For now, compare IDs
        return ID.CompareTo(descr.ID);
    }

    // IEquatable implementation, for determining if a Descriptor exists in a List.
    public bool Equals(Descriptor descr)
    {
        return (ID == descr.ID);
    }
    
    // Returns false if Name is already in use in master list
    // Renaming only used by DescrListPanel
    public bool Rename(string name)
    {
        if (Names.Contains(name))
            return false;

        Name = name;
        return true;
    }


    static public void Initialize()
    {
        if (initialized) return;
        
        initialized = true;
        CreateNew(DescrType.Text);
        CreateNew(DescrType.Text);
        CreateNew(DescrType.Text);
        CreateNew(DescrType.Text);
        CreateNew(DescrType.Text);

    }


    static public void RemoveDescriptor(Descriptor descriptor)
    {
        collection.RemoveDescr(descriptor);
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

    // Makes use of copy constructor
    static public Descriptor Copy(Descriptor descr)
    {
        return collection.CopyDescr(descr);
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
        : base(name, DescrType.Text) 
    {
        Text = DefaultText;
    }

    public Descriptor_Text(Descriptor_Text descr) : base(descr)
    {
        this.DefaultText = descr.DefaultText;
        this.Text = descr.DefaultText;
    }
}

public class Descriptor_CheckBox : Descriptor
{
    public bool DefaultIsChecked { get; set; } = false;
    public bool IsChecked { get; set; } = false;

    public Descriptor_CheckBox(string name = "Descriptor Name", DescrType type = DescrType.CheckBox)
        : base(name, DescrType.CheckBox) { }

    public Descriptor_CheckBox(Descriptor_CheckBox descr) : base(descr)
    {
        this.DefaultIsChecked = descr.DefaultIsChecked;
        this.IsChecked = descr.IsChecked;
    }
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
    public Descriptor_Number(string name = "Descriptor Name", DescrType type = DescrType.Number)
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
    // Copy Constructor
    public Descriptor_Number(Descriptor_Number descr) : base(descr)
    {
        Array.Copy(descr.nums, nums, descr.nums.Length);
        Precision = descr.Precision;
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
public class Descriptor_Tags : Descriptor
{

    public Descriptor_Tags(string name = "Descriptor Name", DescrType type = DescrType.Tags)
        : base(name, DescrType.Tags) { }

    public Descriptor_Tags(Descriptor_Tags descr) : base(descr)
    {

    }
}


