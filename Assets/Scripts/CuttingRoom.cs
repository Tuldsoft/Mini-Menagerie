using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is empty. Its source code only contains commented-out, abandoned code.
/// </summary>
class CuttingRoom 
{
    
}


/// <summary>
/// The below experiment used DNum and DNums as a standin for a value that could be either 
/// an int or a decimal. It has been abandoned. 
/// </summary>
/*
// Experimental Descriptor_Number that uses DNum and DNums as a replacement for convertable ints and decimals
public class Descriptor_Number : Descriptor
{
    // DNum is treated as if it were an int or a decimal. Math (+/-) is only permitted
    // with other DNums. They input and output as strings.
    class DNum : IComparable
    {
        const int Precision = 3; // number of decimal places to show when in that mode
        decimal? decValue = null;
        int? intValue = null;

        public bool IsDecimal 
        {
            get => IsDecimal;
            set 
            {
                IsDecimal = value;
                Convert();
            } 
        }

        public DNum()
        {
            IsDecimal = false; // properties cannot be autoinitialized if using custom get; set;
        }

        // Returns false if string cannot be parsed
        public bool Set(string s)
        {
            if (IsDecimal)
            {
                if (Decimal.TryParse(s, out decimal result))
                {
                    decValue = result;
                    return true;
                }
            }
            else
            {
                if (Int32.TryParse(s, out int result))
                {
                    intValue = result;
                    return true;
                }
            }
            
            return false; // TryParse failed, no values set
        }

        public int CompareTo(DNum dNum)
        {
            if (dNum == null) return 1;

            decimal? thisValue = IsDecimal ? decValue : intValue;
            decimal? thatValue = dNum.IsDecimal ? dNum.decValue : dNum.intValue;

            return Nullable.Compare(thisValue, thatValue);
        }

        void Convert()
        {
            if (IsDecimal)
                decValue = intValue;       // implicit conversion ok, "up"scaling int to decimal
            else
                intValue = (int?)decValue; // explicit conversion required to downscale decimal to int
        }

        DNum Clamp(DNum dNum, DNum min, DNum max)
        {
            if (dNum.CompareTo(max) > 0) 
                return max;
            if (dNum.CompareTo(min) < 0) 
                return min;
            return dNum;
        }

        public override string ToString()
        {
            if (IsDecimal)
                return (decValue == null) ? null : Math.Round((decimal)decValue, Precision).ToString();

            return intValue.ToString();
        }

        // Unary operators (+DNum, -DNum)
        public static DNum operator +(DNum dNum) => dNum;
        public static DNum operator -(DNum dNum)
        {
            dNum.decValue *= -1; // null * -1 = null
            dNum.intValue *= -1;
            return dNum;
        }

        // Binary operators (DNum + DNum, DNum - DNum)
        public static DNum operator +(DNum dNumA, DNum dNumB)
        {
            if (dNumA == null || dNumB == null)
                return dNumA;

            
            if (dNumA.IsDecimal)
            {
                if (dNumB.decValue == null)
                    return dNumA;

                dNumA.decValue ??= 0m; // if (dNumA.decValue == null)
                                      //     dNumA.decValue = 0m;

                dNumA.decValue += dNumB.decValue;
                return dNumA;
            }
            else
            {
                if (dNumB.intValue == null)
                    return dNumA;

                dNumA.intValue ??= 0; // if (dNumA.intValue == null)
                                      //     dNumA.intValue = 0;

                dNumA.intValue += dNumB.intValue;
                return dNumA;
            }
        }
        
        public static DNum operator -(DNum dNumA, DNum dNumB)
        {
            if (dNumA == null || dNumB == null)
                return dNumA;


            if (dNumA.IsDecimal)
            {
                if (dNumB.decValue == null)
                    return dNumA;

                dNumA.decValue ??= 0m;
                dNumA.decValue -= dNumB.decValue;
                return dNumA;
            }
            else
            {
                if (dNumB.intValue == null)
                    return dNumA;

                dNumA.intValue ??= 0;
                dNumA.intValue -= dNumB.intValue;
                return dNumA;
            }
        }

        public static implicit operator int(DNum dNum)
            => (int)dNum.intValue;

        public static implicit operator decimal(DNum dNum)
            => (decimal)dNum.decValue;

        public static implicit operator string(DNum dNum)
            => dNum.ToString();

        
    }
    
    // A set of 5 DNums
    class DNums
    {
        DNum[] dNums = new DNum[5];
        
        public bool IsDecimal
        {
            get => dNums[0].IsDecimal;
            set
            {
                foreach (DNum dNum in dNums)
                {
                    dNum.IsDecimal = value;
                }
            }
        }

        // private Indexer overload (enables using NumIndex without the cast to (int) )
        // Example: this[NumIndex.Value].Set("123.456")
        DNum this[NumIndex nIndex] //=> dNums[(int)nIndex];
        {
            get => dNums[(int)nIndex];
            set => dNums[(int)nIndex] = value;
        }
        
        // Accessor properties, applying clamp as necessary
        public string Value
        {
            get => this[NumIndex.Value];
            set
            {
                
                
                
                
                
                
                
                
                
                
                
                
                // FIX THIS!!! Comparing a string to a DNum, not two DNums...










                Value = value.CompareTo(this[NumIndex.Max]) > 0 ? this[NumIndex.Max] : value;
                Value = value.CompareTo(this[NumIndex.Min]) < 0 ? this[NumIndex.Min] : value;
            }
        }
        //=> dNums[NumIndex.Value];    // need to implement clamp
        public string Min => this[NumIndex.Min];
        public string Max => this[NumIndex.Max];
        public string Default
        {
            get => this[NumIndex.Default];
            set
            {
                Default = value.CompareTo(this[NumIndex.Max]) > 0 ? this[NumIndex.Max] : value;
                Default = value.CompareTo(this[NumIndex.Min]) < 0 ? this[NumIndex.Min] : value;
            }
        }
    }

    enum NumIndex { Value = 0, Min = 1, Max = 2, Default = 3, Increment = 4 }
        
    DNums dNums = new DNums(); // wrapper for an array of 5 dNums

    // Accessor Shortcut properties







    // FIX: DNums should have its own Accessor Properties, and Descriptor should reference those.






    public string Value
    {
        get => dNums[NumIndex.Value];
        set
        {
            Value = value.CompareTo(dNums[NumIndex.Max]) > 0 ? dNums[NumIndex.Max] : value;
            Value = value.CompareTo(dNums[NumIndex.Min]) < 0 ? dNums[NumIndex.Min] : value;
        }
    }
        //=> dNums[NumIndex.Value];    // need to implement clamp
    public string Min => dNums[NumIndex.Min];
    public string Max => dNums[NumIndex.Max];
    public string Default
    {
        get => dNums[NumIndex.Default];
        set
        {
            Default = value.CompareTo(dNums[NumIndex.Max]) > 0 ? dNums[NumIndex.Max] : value;
            Default = value.CompareTo(dNums[NumIndex.Min]) < 0 ? dNums[NumIndex.Min] : value;
        }
    }
        //=> dNums[NumIndex.Default]; // need to implement clamp
    public string Increment => dNums[NumIndex.Increment];

    
    public bool AllowDecimals { 
        get => dNums.IsDecimal;
        set => dNums.IsDecimal = value;
    }

    
}
*/