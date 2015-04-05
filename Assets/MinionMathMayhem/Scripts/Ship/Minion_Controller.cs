using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Minion_Controller : MonoBehaviour
    {


        /*                      MINION CONTROLLER
         * This class is designed to centralize the minion's attributes and behaviour to meet the condition's within the environment.
         * 
         * GOALS:
         *  Centralization of behavior and attributes
         */


        // Declarations and Initializations
        // ---------------------------------
        // These can be adjusted within Unity's inspector
            // Speed
                // Climbing the Ladder
                    public float speedClimbLadder;
                // Running
                    public float speedRunning;
            // Minion Selected\Flick
                // Thrust Force
                        public float thrustForce;
                // Thrust Direction
                        public Vector3 thrustDirection;
        // ----



        public void Detected()
        {
            print("Successfully detected this script and function!");
        }




        // Returns the Climb Speed value to the calling script
        public float ReturnClimbSpeed
        {
            get { return speedClimbLadder; }
        } // ReturnClimbSpeed



        // Returns the Running Speed value to the calling script
        public float ReturnRunningSpeed
        {
            get { return speedRunning; }
        } // ReturnRunningSpeed


        // Returns the Thrust Force value to the calling script
        public float ReturnThurstForce
        {
            get { return thrustForce; }
        } // ReturnThrustForce


        // Returns the Thrust Direction to the calling script
        public Vector3 ReturnThrustDirection
        {
            get { return thrustDirection; }
        } // ReturnThrustDirection
    } // End of Class
} // namespace