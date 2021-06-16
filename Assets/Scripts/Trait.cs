using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public enum TraitType
{
    TXT, // Text trait
    CHK, // CheckBox trait
    NUM, // Number trait
    TAG  // Tags trait
}


public class Trait : IComparable<Trait>, IEquatable<Trait>
{
    protected const string defaultName = "Trait Name";
    protected const string defaultName_TXT = "Text Name";
    protected const string defaultName_CHK = "Checkbox Name";
    protected const string defaultName_NUM = "Number Name";
    protected const string defaultName_TAG = "Tags Name";

    // master is set by the constructor. If a "new" Trait, then master = this.
    // If the copy constructor is used, then master = the copied trait.

    protected Trait master; // If this, then this is the master.
    public bool IsMaster { get => master == this; }

    // Properties derived from Master
    Guid id;
    public Guid ID { get => master.id; }

    string name;
    public string Name { get => master.name; }

    TraitType type;
    public TraitType Type { get => master.type; }

    bool includeAll = false;
    public bool IncludeAll { get => master.includeAll; set => master.includeAll = value; }

    // Properties unique to this Trait
    bool Show { get; set; } = true;

    // Static properties
    static bool initialized = false;

    static TraitCollection collection = new TraitCollection();
    static public List<Trait> List { get => collection.List; }
    static public Dictionary<TraitType, Action> Constructors { get; private set; }
    static public Dictionary<Type, ConstructorInfo> CopyConstructors { get; private set; }

    static public IEnumerable<string> Names { 
        get
        {
            // Pull names from the master list
            IEnumerable<string> names =
            from trait in List
            select trait.Name;

            return names;
        } 
    }

    static public Trait ActiveTrait { get; private set; }



    // Constructor for unique Traits, only called by derived constructors
    protected Trait (string name = defaultName, TraitType type = TraitType.TXT)
    {
        master = this;  // This sets the trait as a master trait
        
        int i = 1;
        string testName = name;
        while (!Rename(testName)) // Loops until a unique name is created
        {
            testName = $"{name} ({i++})";
        }

        this.type = type;
        this.id = Guid.NewGuid();

        collection.AddTrait(this);
    }

    // Copy Constructor for putting Descriptors on Minis
    public Trait (Trait trait)
    {
        master = trait; // the remaining fields now pull from master

        //this.Name = trait.Name;
        //this.Type = trait.Type;
        //this.ID = trait.ID;
        //this.IncludeAll = trait.IncludeAll;
        //this.IsMaster = false;
    }

    // IComparable implementation: Compare NameType
    public int CompareTo(Trait trait)
    {
        // Later, implement user-defined sorting.
        
        // For now, compare IDs
        return ID.CompareTo(trait.ID);
    }

    // IEquatable implementation, for determining if a Trait exists in a List.
    public bool Equals(Trait trait)
    {
        return (ID == trait.ID);
    }
    
    // Returns false if Name is already in use in master list
    // Renaming used by constructor and TraitListPanel
    public bool Rename(string name)
    {
        // if in the master list, return false
        if (Names.Contains(name))
            return false;

        // if unique, set as the name of the master
        master.name = name;
        return true;
    }

    // Static tools
    static public void Initialize()
    {
        if (initialized) return;
        
        initialized = true;

        Constructors = new Dictionary<TraitType, Action>();
        Constructors.Add(TraitType.TXT, new Action(() => new Trait_TXT()));
        Constructors.Add(TraitType.CHK, new Action(() => new Trait_CHK()));
        Constructors.Add(TraitType.NUM, new Action(() => new Trait_NUM()));
        Constructors.Add(TraitType.TAG, new Action(() => new Trait_TAG()));


        // CopyConstructors best called via Reflection since parameters are required
        //   and copy constructors are meant to be reusable.
        CopyConstructors = new Dictionary<Type, ConstructorInfo>();

        ConstructorInfo ctorInfo = typeof(Trait_TXT).GetConstructor(new[] { typeof(Trait_TXT) });
        CopyConstructors.Add(typeof(Trait_TXT), ctorInfo);
        
        ctorInfo = typeof(Trait_CHK).GetConstructor(new[] { typeof(Trait_CHK) });
        CopyConstructors.Add(typeof(Trait_CHK), ctorInfo);

        ctorInfo = typeof(Trait_NUM).GetConstructor(new[] { typeof(Trait_NUM) });
        CopyConstructors.Add(typeof(Trait_NUM), ctorInfo);
        
        ctorInfo = typeof(Trait_TAG).GetConstructor(new[] { typeof(Trait_TAG) });
        CopyConstructors.Add(typeof(Trait_TAG), ctorInfo);

        CreateNew(TraitType.TXT);
        CreateNew(TraitType.CHK);
        CreateNew(TraitType.NUM);
        CreateNew(TraitType.TXT);
        CreateNew(TraitType.TXT);

    }

    static public void RemoveTrait(Trait trait)
    {
        collection.RemoveTrait(trait);
    }

    static public void CreateNew(TraitType type)
    {
        Constructors[type].DynamicInvoke();
    }

    // Makes use of copy constructor to recreate a trait from the master collection
    static public Trait Copy(Trait trait)
    {
        return CopyConstructors[trait.GetType()].Invoke(new Trait[] { trait }) as Trait;
    }

    static public void SetActive(Trait trait)
    {
        ActiveTrait = trait;
    }
    
}



// A Trait that stores text as its value
public class Trait_TXT : Trait
{
    // Master-derived properties
    Trait_TXT masterTXT { get => master as Trait_TXT; }
    
    string defaultText = "Default Text";
    public string DefaultText { 
        get => masterTXT.defaultText; 
        set => masterTXT.defaultText = value; }
    

    // Unique to each Trait, does not reference the master
    public string Text { get; set; } = "Default text";


    // For unique construction
    public Trait_TXT(string name = defaultName_TXT)
        : base(name, TraitType.TXT) 
    {
        // a new Trait_TXT's Text begins as DefaultText
        this.Text = DefaultText;
    }

    public Trait_TXT(Trait_TXT trait) : base(trait)
    {
        // a new Trait_TXT's Text begins as DefaultText
        this.Text = DefaultText;
    }
}


// A Trait that stores true or false as its value
public class Trait_CHK : Trait
{
    // Master-derived properties
    Trait_CHK masterCHK { get => master as Trait_CHK; }

    bool defaultIsChecked = false;
    public bool DefaultIsChecked { 
        get => masterCHK.defaultIsChecked; 
        set => masterCHK.defaultIsChecked = value; }


    // Unique to this trait
    public bool IsChecked { get; set; } = false;

    // Unique Construction
    public Trait_CHK(string name = defaultName_CHK)
        : base(name, TraitType.CHK) 
    {
        IsChecked = DefaultIsChecked;
    }

    // Copy Construction
    public Trait_CHK(Trait_CHK trait) : base(trait)
    {
        IsChecked = DefaultIsChecked;
    }
}


// A Trait that stores a number as its value
public class Trait_NUM : Trait
{
    Trait_NUM masterNUM { get => master as Trait_NUM; }


    enum NumIndex { Value = 0, Default = 1, Min = 2, Max = 3, Increment = 4 }

    readonly decimal?[] defaultNums = { null, null, null, null, 1 };


    // All but nums[0] is derived from master
    decimal?[] nums;     // only used by master
    decimal?[] Nums { get => masterNUM.nums; }


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

    // Number of places after the decimal point
    private int precision = 0;
    // Number of places after the decimal point
    public int Precision
    {
        get => masterNUM.precision;
        set => masterNUM.precision = Math.Max(Math.Min(value, MaxPrecision), 0); // clamp 0 to 5
    }
    const int MaxPrecision = 5;
    private string precisionFormat { get => String.Format("N{0}", Precision); }



    // Constructor
    public Trait_NUM(string name = defaultName_NUM)
        : base(name, TraitType.NUM)
    {
        Precision = 0;
        nums = new decimal?[Enum.GetNames(typeof(NumIndex)).Length];
        // TODO: add try statement for safety, in case array sizes don't line up
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = defaultNums[i];
        }
    }
    // Copy Constructor
    public Trait_NUM(Trait_NUM trait) : base(trait)
    {
        //Array.Copy(descr.nums, nums, descr.nums.Length); // Array.Copy() does not work with Nullable types
        nums = new decimal?[Enum.GetNames(typeof(NumIndex)).Length];
        for (int i = 1; i < nums.Length; i++)
            nums[i] = trait.nums[i];

        // a new Trait_NUM starts with a Value of Default
        nums[(int)NumIndex.Value] = trait.nums[(int)NumIndex.Default];

        Precision = trait.Precision;
    }



    // Get a string from a decimal? value in the nums[] array
    string GetNum(NumIndex numIndex)
    {
        // use either unique value or master-derived value
        decimal? result = numIndex == NumIndex.Value ? nums[0] : masterNUM.nums[(int)numIndex];

        if (result == null) 
            return String.Empty;

        return ((decimal)result).ToString(precisionFormat);
    }

    // Parse a string, clamp the value, and store it in nums[]
    void SetNum(NumIndex numIndex, string s)
    {
        //store reference to self nums or master nums
        decimal?[] localNums = numIndex == NumIndex.Value ? nums : masterNUM.nums;

        if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
        {
            localNums[(int)numIndex] = null;
        }
        else
        {
            decimal? newValue = StringToDecimal(s); // returns null if parse fails
            if (newValue == null)
                return;                             // do not change the value for a bad string

            // No Min greater than Max, or vice versa
            if (numIndex == NumIndex.Max &&
                newValue < localNums[(int)NumIndex.Min])
                newValue = localNums[(int)NumIndex.Min];
            if (numIndex == NumIndex.Min &&
                newValue > localNums[(int)NumIndex.Max])
                newValue = localNums[(int)NumIndex.Max];

            // Clamp a new Value or Default to existing Min and Max
            if (numIndex == NumIndex.Value ||
                numIndex == NumIndex.Default )
                Clamp(ref newValue);

            localNums[(int)numIndex] = newValue;

            // If Default or Value is outside a new Min or Max, Clamp it.
            if (numIndex == NumIndex.Min ||
                numIndex == NumIndex.Max)
            {
                Clamp(ref localNums[(int)NumIndex.Value]);
                Clamp(ref localNums[(int)NumIndex.Default]);
            }
                
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

    // Clamp a decimal? value to Min and Max
    void Clamp(ref decimal? num)
    {
        decimal? min = Nums[(int)NumIndex.Min];
        decimal? max = Nums[(int)NumIndex.Max];

        num = num < min ? min : num; // < and > work for nullable types
        num = num > max ? max : num; // null < any value
    }

    // Increment Value
    public void Increase()
    {
        if (nums[(int)NumIndex.Increment] != null)
        {
            // Unity does not recognize ??= operator...
            // nums[(int)NumIndex.Value] ??= 0;
            
            if (nums[(int)NumIndex.Value] is null)
                nums[(int)NumIndex.Value] = 0;
            nums[(int)NumIndex.Value] += Nums[(int)NumIndex.Increment];
            Clamp(ref nums[(int)NumIndex.Value]);
        }
    }

    // Decrement Value
    public void Decrease()
    {
        if (nums[(int)NumIndex.Increment] != null)
        {
            if (nums[(int)NumIndex.Value] is null)
                nums[(int)NumIndex.Value] = 0;
            nums[(int)NumIndex.Value] -= Nums[(int)NumIndex.Increment];
            Clamp(ref nums[(int)NumIndex.Value]);
        }
    }
}

public class Trait_TAG : Trait
{

    public Trait_TAG(string name = defaultName_TAG)
        : base(name, TraitType.TAG) { }

    public Trait_TAG(Trait_TAG descr) : base(descr)
    {

    }
}


