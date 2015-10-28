using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Integer 
{
    private static System.Random r = new System.Random();
    private int value;

    public class FactorPair
    {
        private static System.Random r = new System.Random();
        public Integer f1;
        public Integer f2;

        // If these factors came from a positive number, then both f1 and
        // f2 will be positive. After this function is called, there is an
        // equal chance thate f1 and f2 remain positive or both will be negetive.
        // If these factors came from a negetive number, then f2 will
        // be negetive. After this function is called, there is an equal
        // chance that either f1 or f2 will be negetive.
        public void RandomizeSigns()
        {
            int choice = r.Next(0, 2);
            // the signs will be changed
            if(choice == 1)
            {
                f1 = -f1;
                f2 = -f2;
            }
        }

        // Randomly assigns the values of f1 and f2 to i1 and i2.
        public void RandomlyAssignFactorsToIntegers(ref Integer i1, ref Integer i2)
        {
            int choice = r.Next(0, 2);
            if(choice == 0)
            {
                i1 = f1;
                i2 = f2;
            }
            else
            {
                i1 = f2;
                i2 = f1;
            }
        }
    }

    // Returns an array of all 
    public FactorPair[] GetFactors()
    {
        Stack<FactorPair> factors = new Stack<FactorPair>();
        Integer v = value;
        v = v.Abs();
        v = v.Sqrt();
        for (int i = 1; i <= v; i++)
        {
            if (v % i == 0)
            {
                FactorPair factor = new FactorPair();
                factor.f1 = i;
                factor.f2 = value / i;
                factors.Push(factor);
            }
        }

        return factors.ToArray();
    }

    // Given the set of all possible factors for this integer, this function returns a 
    // pair of factors, such that the first factor, f1, time the second
    // factor, f2, is equal to the value of this integer.
    public FactorPair GetRandomFactorPair()
    {
        FactorPair[] factors = GetFactors();
        int i = r.Next(0, factors.Length);
        return factors[i];
    }

    // Define implicit conversion to int
    public static implicit operator int(Integer i)
    {
        return i.value;
    }

    // Define implicit conversion from int to Integer
    public static implicit operator Integer(int i)
    {
        Integer ret = new Integer();
        ret.value = i;
        return ret;
    }

    // Convert the integer to a string
    public override string ToString()
    {
        return value.ToString();
    }

    // Return the square root of an integer with the decimal truncated
    public Integer Sqrt()
    {
        return (int)Math.Sqrt((float)value);
    }

    // Returns the quare root of a number, but it the number is negetive
    // it will find the square root of the absolute value of the number
    // and then return the negated root.
    public Integer SqrtIgnoringSign()
    {
        if(value < 0)
            return -(int)Math.Sqrt((float)-value);
        return Sqrt();
    }

    public Integer Abs()
    {
        return Math.Abs(value);
    }
}
