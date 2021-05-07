using System;
using System.Collections;
using System.Collections.Generic;

public enum DescrType
{
    Text,
    CheckBox,
    Number,
    Tags
}

public class Descriptor : IComparable<Descriptor>
{
    /// <summary>
    /// Instead of just using Name, Descriptors have NameTypes, which includes both Name and DescrType. 
    /// Only one combo of Name and Type is allowed, if another pair is generated, Name is appended
    /// with an iterator.
    /// </summary>
    class NameType : IEquatable<NameType>
    {
        // Master list of all NameTypes
        static public List<NameType> masterList = new List<NameType>();

        string origName;

        public DescrType Type { get; set; }

        int nameIter = 0;

        string name
        {
            get => nameIter <= 0 ? origName : origName + nameIter.ToString();
            set
            {
                //ValidateString(ref value);
                origName = value;
                ValidateName(this);
            }
        }

        public NameType(string name = "Nameless", DescrType type = DescrType.Text)
        {
            this.Type = type;
            this.name = name; // triggers name validation
            masterList.Add(this);
        }

        public bool Equals(NameType comparison)
        {
            if (name == comparison.name && Type == comparison.Type)
                return true;

            return false;
        }

        void ValidateString(ref string s)
        {
            // StringUtils.ValidateString(s);
        }

        void ValidateName (NameType checkNameType)
        {
            while (masterList.Contains(checkNameType))
            {
                checkNameType.IncrementName();
            }
        }

        public void IncrementName()
        {
            nameIter++;
        }

        public override string ToString() => name;

        public void Rename(string newName)
        {
            masterList.Remove(this);
            name = newName;
            masterList.Add(this);
        }

    }

    // Stores Name and Type
    NameType nameType;

    // Get from NameType
    public string Name { 
        get => nameType.ToString();
        set => nameType.Rename(value);
    }  

    // Get from NameType
    public DescrType Type { get => nameType.Type; set => nameType.Type = value; }
    
    public bool AlwaysShow { get; set; } = false;

    // Stores all Descriptors created, used in populating ScrollLists.
    static List<Descriptor> masterList = new List<Descriptor>();
    static public List<Descriptor> List { get => masterList; }

    // Constructor
    public Descriptor (string name = "Descriptor Name", DescrType type = DescrType.Text)
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

    public void RemoveDescriptor(Descriptor descriptor)
    {
        masterList.Remove(descriptor);
    }

    
}


public class Descriptor_Text : Descriptor
{
    public string DefaultText { get; set; } = "Default text";
    public string Text { get; set; } = "Default text";
}

public class Descriptor_CheckBox : Descriptor
{
    public bool DefaultIsChecked { get; set; } = false;
    public bool IsChecked { get; set; } = false;
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
    public Descriptor_Number()
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


