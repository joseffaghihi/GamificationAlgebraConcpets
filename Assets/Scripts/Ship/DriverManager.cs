using UnityEngine;
using System.Collections;

public class DriverManager : MonoBehaviour
{
    /*
    // Declarations and Initializations
    // ---------------------------------
    // Debug Mode Variable
    // Used to output verbose information and\or status messages.
    public bool debugMode = true;
    // Spawn creature prefab variable
    // This variable will contain the prefab for the creatures in the game\scene
    public GameObject Creature;
    // Spawn Points positions
    // Within the scene, there contains several 'Map Spots' that are dedicated to spawning.
    // Initialize these with using the Unity Inspector
    public Transform spawnPoint0;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    // Creature: Movement speed
    //public float creatureSpeed;
    // How many creatures will be spawned per wave
    // Initialize this with using the Unity Inspector
    public int countCreatures;
    // How many waves per game
    // This can be initialized using the Unity Inspector
    public int waveCount;
    // How much time should we wait before spawning another creature
    // Initialize these with using the Unity Inspector
    public int waitTimeSeconds;
    // Map Number
    // Need a better way to implement this
    public const int mapNumber = 1;
    // Quadratic Formula: Random Number Generator Range
    // Minimum range - which is assigned within the Unity's Inspector
    public int quadraticMinimumRange;
    // Maximum range - which is assigned within the Unity's Inspector
    public int quadraticMaximumRange;
    // Quadratic Formula: Identify A, B, C
    // A
    private int quadraticIdentify_A;
    // B
    private int quadraticIdentify_B;
    // C
    private int quadraticIdentify_C;
    // These variables will be used as a means to communicate to other scripts
    // Access the creature's unique number that they asign themselves.
    //public CreatureIdentity accessCreatureIdentity;
    //public GameObject CreatureIdentityObject;
    // ----
    


    // When the scene immediately loads, this script will be executed automatically - regardless if not the is exactly no instance of the actor.
    private void Awake()
    {
        // This will initialize the stated variable to the specific object that contains the 'Minion' tag.
        //accessCreatureIdentity = GameObject.FindGameObjectWithTag("Minion").gameObject.GetComponent<CreatureIdentity>();
    } // End of Awake



    // Use this for initialization
    private void Start()
    {
        // Generate a new 'Quadratic' Equation
        //GenerateQuadraticEquation();
        // Check the creature's unique self-assigned number
        //CreatureCheckUniqueNumber();
        // Activate the creatures to spawn in the game
        //StartCoroutine(SpawnCreatureDriver());
    } // End of Start()




    // This function will manage how the creatures are to be spawned and where
    private IEnumerator SpawnCreatureDriver()
    {
        // ----
        // Declarations
        // Random number generator; used for selecting the spawn points
        int randomGen;
        // ----


        // Algorithm for summoning the creatures until the max creature limit has been reached
        for (int i = 0; i <= countCreatures; i++)
        {
            //Fetch the random number
            randomGen = SpawnCreatureRandomGenerator(0, 3); // Fetch a random number that will be used for determining which Spawn Point is to be selected.
            yield return StartCoroutine(SpawnCreatureHandler(randomGen)); // Call another function but this function /MUST/ wait for the called function to terminate.
        } // End of loop
    } // End of SpawnCreatureDriver()



    //This function will spawn the creature within the virtual world.
    //This function requies the Driver to have already 'randomized' the Spawn Points
    private void SpawnCreature(Transform spawnPoint)
    {
        // Debug Status Message
        if (debugMode == true)
            Debug.Log("Spawning creature at: " + spawnPoint.transform.position);
        // ----

        // Spawn the creature at the selected spawn spot
        Instantiate(Creature, spawnPoint.transform.position, Quaternion.identity);
    } // End of SpawnCreature()



    //This function will generate a random number given the specific ranges
    private int SpawnCreatureRandomGenerator(int min, int max)
    {
        // Capture the random number into a cached variable.
        int randNum = Random.Range(min, max);

        // Debug the results to the console
        if (debugMode == true)
            Debug.Log("Random Number Generator: [min = " + min + " , max = " + max + " ]; returned: " + randNum);
        // ----

        return randNum;
    } // End of SpawnCreatureRandomGenerator()



    // This function is dedicated for temporarily 'suspending' for a specific amount of time.
    private IEnumerator SpawnCreatureHandler(int randomNumber)
    {
        // -----------------
        //         * NOTES
        //         * --------
        //         * These values are going to be used for determining which spawn point will be selected
        //         * Minimum = 0
        //         * Maximum = 2
        //         * 
        //         * 0 = SpawnPoint0
        //         * 1 = SpawnPoint1
        //         * 2 = SpawnPoint2
        // --------------


        // Determind what spawn point is going to be selected
        // SpawnPoint0
        if (randomNumber == 0)
        {
            // Debug Status Message
            if (debugMode == true)
                Debug.Log("Determined spawn spot: SpawnPoint0");
            // ----

            SpawnCreature(spawnPoint0);
        } // End of if: SpawnPoint0


    // SpawnPoint1
        else if (randomNumber == 1)
        {
            // Debug Status Message
            if (debugMode == true)
                Debug.Log("Determined spawn spot: SpawnPoint1");
            // ----

            SpawnCreature(spawnPoint1);
        } // End of if: SpawnPoint1


    // SpawnPoint2
        else if (randomNumber == 2)
        {
            // Debug Status Message
            if (debugMode == true)
                Debug.Log("Determined spawn spot: SpawnPoint2");
            // ----

            SpawnCreature(spawnPoint2);
        } // End of if: SpawnPoint2


    // Critical Error
        else
        {
            // An error occured within the Random Number Generator range.
            // Log the error in the console
            Debug.LogError("Random Number has an unsupported value of: " + randomNumber);
        } // End of else: <!>


        // wait $waitTimeSeconds before closing this function.
        yield return new WaitForSeconds(waitTimeSeconds);
    } // End of SpawnCreatureHandler()




    // Report back the map number for the current local scene.  Yes, this is really bad - terribly bad.  We can later fix this
    public int reportMapNumber()
    {
        return mapNumber;
    } // End of reportMapNumber




    // *************************************************************************************************************************
    // -------------------------------------------------------------------------------------------------------------------------
    // *************************************************************************************************************************
    // Quadratic Equation Manager
    // ------------------------------

    
    // This function will return a random number to the calling function
    private int RandomNumberGenerator()
    {
        //Fetch a random number within the given range and return it
        return Random.Range(quadraticMinimumRange, quadraticMaximumRange);
    } // End of RandomNumberGenerator



    // This function will generate the quadratic equation within the game
    private void GenerateQuadraticEquation()
    {
        // Build the Quadratic equation by using the RNG
        quadraticIdentify_A = RandomNumberGenerator();
        quadraticIdentify_B = RandomNumberGenerator();
        quadraticIdentify_C = RandomNumberGenerator();
        // ----

        // Debug Message
        if (debugMode == true)
            Debug.Log("Quadratic Equation: " +
                quadraticIdentify_A + "x^2 + " +
                quadraticIdentify_B + "x + " +
                quadraticIdentify_C + " " +
                "= 0");
    } // End of GenerateQuadraticEquation



    
    // This function will check the creature's given random 'number' and determine if it matches with the actual quadratic equation
    private void CreatureCheckUniqueNumber()
    {

        // Fetch the creatures number by accessing the instance's unique ID
        int creatureNum = accessCreatureIdentity.CreatureNumber_Identify();

        // Debug message
        if (debugMode == true)
            Debug.Log("Creature's Identity Recieved: " + creatureNum);

        // The creatures number matches with 'A' from the Quadratic Equation
        if (creatureNum == quadraticIdentify_A)
        {
            // Debug message
            if (debugMode == true)
                Debug.Log("Creature's Identity: " + creatureNum + " Matches with Quadratic Equation 'A': " + quadraticIdentify_A);

            // -----
            // DO STUFF HERE
            // -----

            // Leave this entire function - nothing further will be executed in this function
            return;
        }

        // The creatures number matches with 'B' from the Quadratic Equation
        else if (creatureNum == quadraticIdentify_B)
        {
            // Debug message
            if (debugMode == true)
                Debug.Log("Creature's Identity: " + creatureNum + " Matches with Quadratic Equation 'B': " + quadraticIdentify_B);

            // -----
            // DO STUFF HERE
            // -----

            // Leave this entire function - nothing further will be executed in this function
            return;
        }

        // The creatures number matches with 'C' from the Quadratic Equation
        else if (creatureNum == quadraticIdentify_C)
        {
            // Debug message
            if (debugMode == true)
                Debug.Log("Creature's Identity: " + creatureNum + " Matches with Quadratic Equation 'C': " + quadraticIdentify_C);

            // -----
            // DO STUFF HERE
            // -----

            // Leave this entire function - nothing further will be executed in this function
            return;
        }

        // The creatures number did not match with the generated quadratic equation
        else
        {
            Debug.Log("Creature's Identity [" + creatureNum + " does not match with the expected values: " +
                "A: " + quadraticIdentify_A + " " +
                "B: " + quadraticIdentify_B + " " +
                "C: " + quadraticIdentify_C);
        }
    } // End of CreatureCheckUniqueNumber
    



    // This function will only return the value of either the maximum or minimum value of the range to the caller or script.
    // This will be useful for the creatures, for example, to determine what is the requested max and min values that they can use for their
    // Random Number Generator.
    public int ReturnRNGRange(bool num)
    {
        if (num == false)
            return quadraticMinimumRange; // Return the minimum value
        else
            return quadraticMaximumRange; // Return the maximum value
    } // End of ReturnRNGRange

*/
} // End of Class