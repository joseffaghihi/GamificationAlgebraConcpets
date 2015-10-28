using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuadraticProblemGenerator
{
    public int min = -100;
    public int max = 100;

    // The coefficients of the quadratic formula
    private int A, B, C;
    private static System.Random r = new System.Random();

    public void GenerateQuadraticWithRandomCoefficientsABC()
    {
        A = GetRandomNumber();
        B = GetRandomNumber();
        C = GetRandomNumber();
    }

    private int GetRandomNumber()
    {
        // We must add one because the upper bound is exclusive
        return r.Next(min, max + 1);
    }

    // This function behaves the same as the Random.Next function
    // except that it excludes zero from the return value.
    private int GetRandomNumberNotEqualToZero(int min, int max)
    {
        int ret = r.Next(min, max);
        while (ret == 0)
            ret = r.Next(min, max);
        return ret;
    }

    private int GetRandomNumberNotEqualToZero()
    {
        return GetRandomNumberNotEqualToZero(min, max + 1);
    }

    override public String ToString()
    {
        String ret = "";
        if (A == 1)
            ret += "x^2";
        else if (A == -1)
            ret += "-x^2";
        else
            ret += A.ToString() + "x^2";

        if(B != 0)
        {
            if (B > 0)
            {
                if(B == 1)
                    ret += " + x";
                else
                    ret += " + " + B.ToString() + "x";
            }          
            else
            {
                if(B == -1)
                    ret += " - x";
                else
                    ret += " - " + (-B).ToString() + "x";
            }        
        }

        if (C != 0)
        {
            if (C > 0)
            {
                ret += " + " + C.ToString();
            }
            else
            {
                ret += " - " + (-C).ToString();
            }
        }

        return ret;
    }

    // This generates a completely random but factorable quadratic.
    // The algorithm works as follows.
    //
    // Suppose we have a quadratic in factored form (C1*x + C2)(C3*x + C4),
    // then we can write it in expanded form as 
    // C1*C3*x^2 + (C1*C4 + C3*C2)*x + C2*C4.
    // Thus, A = C1*C3 and B = C1*C4 + C3*C2 and C = C2*C4. We merely need 
    // to pick the values for C1, C2, C3, and C4 at random, but this 
    // algorithm is concerned with creating nicely factorable quadratics. 
    // In the factoring process, we multiply A by C, and we desire that
    // this number stays within a certain range, so as not be too difficult 
    // for a human to compute. That is min <= A*C <= max.
    // To accomplish this, I start with AC which equals C1*C2*C3*C4 and 
    // factor it to get A and C. I factor A and C to get C1, C2, C3, and C4.
    public void GenerateFactorableQuadratic()
    {
        Integer C1C2C3C4 = GetRandomNumber();
        while (C1C2C3C4 == 0)
            C1C2C3C4 = GetRandomNumber();
        Integer.FactorPair factors = C1C2C3C4.GetRandomFactorPair();
        factors.RandomizeSigns();
        Integer C1C3 = new Integer();
        Integer C2C4 = new Integer();
        factors.RandomlyAssignFactorsToIntegers(ref C1C3, ref C2C4);
        Integer C1 = new Integer(), C2 = new Integer(), C3 = new Integer(), C4 = new Integer();
        factors = C1C3.GetRandomFactorPair();
        factors.RandomizeSigns();
        factors.RandomlyAssignFactorsToIntegers(ref C1, ref C3);
        factors = C2C4.GetRandomFactorPair();
        factors.RandomizeSigns();
        factors.RandomlyAssignFactorsToIntegers(ref C2, ref C4);
        A = C1 * C3;
        B = C1 * C4 + C3 * C2;
        C = C2 * C4;
    }

    // This generates a quadatic with A = 1, so it's a little easier to factor.
    // This uses the same principles from the GenerateFactorableQuadratic above.
    // The expanded form becomes x^2 + (C4 + C2)x + C2*C4
    public void GenerateSimpleFactorableQuadratic()
    {
        Integer C2C4 = GetRandomNumber();
        while(C2C4 == 0)
            C2C4 = GetRandomNumber();
        Integer.FactorPair factors = C2C4.GetRandomFactorPair();
        factors.RandomizeSigns();
        Integer C2 = new Integer(), C4 = new Integer();
        factors.RandomlyAssignFactorsToIntegers(ref C2,ref C4);
        A = 1;
        B = C2 + C4;
        C = C2 * C4;
    }

    // Generate a perfect square with the squared terms coeffient
    // always equal to 1. For example, x^2+4x+4
    public void GenerateSimplePerfectSquare()
    {
        Integer max = this.max;
        Integer min = this.min;

        min = min.SqrtIgnoringSign();
        max = max.SqrtIgnoringSign();

        int rootC = r.Next(min, max + 1);
        A = 1;
        B = 2 * rootC;
        C = rootC * rootC;
    }

    // Generates a completely random perfect square
    public void GeneratePerfectSquare()
    {
        Integer max = this.max;
        Integer min = this.min;

        min = min.SqrtIgnoringSign();
        max = max.SqrtIgnoringSign();

        A = GetRandomNumberNotEqualToZero(min, max + 1);
        // Allow C to equal zero. This results in a polynomial of (a*x)^2
        C = r.Next(min, max + 1);
        B = 2 * A * C;
        A = A * A;
        C = C * C;
    }

    // Produces a difference of two squares problem with 
    // -C <= max and leading coefficient equal to 1
    public void GenerateSimpleDifferenceOfTwoSquares()
    {
        Integer min = this.min;
        Integer max = this.max;

        min = min.SqrtIgnoringSign();
        max = max.SqrtIgnoringSign();

        A = 1;
        B = 0;
        C = r.Next(min, max + 1);
        C = -C * C;
    }

    // Produces a difference of two squares problem.
    // -C <= max and A <= max.
    public void GenerateDifferenceOfTwoSquares()
    {
        Integer min = this.min;
        Integer max = this.max;

        min = min.SqrtIgnoringSign();
        max = max.SqrtIgnoringSign();

        A = GetRandomNumberNotEqualToZero(min, max + 1);
        B = 0;
        C = r.Next(min, max + 1);
        A *= A;
        C *= -C;
    }
}