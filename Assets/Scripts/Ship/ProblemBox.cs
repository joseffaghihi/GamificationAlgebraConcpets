using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProblemBox : MonoBehaviour
{

        /*                    PROBLEM BOX
         * This class will simply generate the quadratic equation by using the RNG within the given range specified within the inspector.
         *  
         * GOALS:
         *  Generate the quadratic equation indexes
         *  Return a random number that requires the RNG boundry
         */



    // Declarations
    // -------------
    // Indexes
        int A;
        int B;
        int C;
    // Reference
        Text problemBox;
    // Random Number Generator (RNG) range [minimum, maximum]
        public int minValue;
        public int maxValue;
    // ----



    // Accessors; used for returning the value of the index positions
    // Index: A
    public int a
    {
        get { return A; }
    }

    // Index: B
    public int b
    {
        get { return B; }
    }

    // Index: C
    public int c
    {
        get { return C; }
    }
    // ----



	// Use this for initialization
    void Awake()
    {
        // Initialization for References
        problemBox = GetComponent<Text>();
    } // End of Awake



    // This script is called once, after the actor has been placed in the scene
	void Start ()
    {
        // Initialize the Quadratic Equation indexes
        Generate();
	} // End of Start



    // Generate the Quadratic Equation
    public void Generate()
    {
        // Update the Indexes
            A = GetRandomNumber();
            B = GetRandomNumber();
            C = GetRandomNumber();
        // ----

        // Output the new equation
        problemBox.text = A.ToString() + "x +" + B.ToString() + "x+" + C.ToString();
    } // End of Generate



    // Return the range of the 'Random Number Generator' or simply the RNG of the Quadratic Equation.
    public int GetRandomNumber()
    {
        return Random.Range(minValue, maxValue);
    } // End of GetRandomNumber

} // End of Class
