using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    // Declarations and Initializations
    // ---------------------------------
    public bool debugMode; // This is meaningless right now, this is a feature that is soon to come.
    private int level;
    private float spawnSpeed = 15;
    private const float speedIncrease = 5.0f;
    private const int winningScore = 100;
    private const int lossFailure = 5;
    // ----



    // Use this for initialization
    void Start()
    {

    } // End of Start



    // Update is called once per frame
    void Update()
    {

    } // End of Update



    // Destroy the Minions from the scene
    private void DestroyMinions()
    {
        //Destroy();
    } // End of DestroyMinions


} // End of Class