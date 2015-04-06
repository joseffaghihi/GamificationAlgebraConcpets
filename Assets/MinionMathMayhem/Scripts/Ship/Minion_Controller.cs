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
                    // [MINION_CONTROLLER] This var takes the local ranges of (min, max) and uses a RNG to get a unique speed value.
                        private float speedClimbLadder;
                    // Local Minimum Boundry Range
                        public float speedClimbLadder_Minimum;
                    // Local Maximum Boundry Range
                        public float speedClimbLadder_Maximum;
                // Running
                    // [MINION_CONTROLLER] This var takes the local ranges of (min, max) and uses a RNG to get a unique speed value.
                        private float speedRunning;
                    // Local Minimum Boundry Range
                        public float speedRunning_Minimum;
                    // Local Maximum Boundry Range
                        public float speedRunning_Maximum;
            // Minion Selected\Flick
                // Thrust Force
                        public float thrustForce;
                // Thrust Direction
                        public Vector3 thrustDirection;
        // ----




        private float GenerateClimbSpeed()
        {
            return Random.Range(speedClimbLadder_Minimum, speedClimbLadder_Maximum);
        } // GenerateClimbSpeed()


        private float GenerateRunningSpeed()
        {
            return Random.Range(speedRunning_Minimum, speedRunning_Maximum);
        } // GenerateRunningSpeed()



        // Returns the Climb Speed value to the calling script
        public float ClimbSpeed
        {
            get { return GenerateClimbSpeed(); }
        } // ClimbSpeed



        // Returns the Running Speed value to the calling script
        public float RunningSpeed
        {
            get { return GenerateRunningSpeed(); }
        } // RunningSpeed



        // Returns the Thrust Force value to the calling script
        public float ThurstForce
        {
            get { return thrustForce; }
        } // ThrustForce



        // Returns the Thrust Direction to the calling script
        public Vector3 ThrustDirection
        {
            get { return thrustDirection; }
        } // ThrustDirection
    } // End of Class
} // namespace