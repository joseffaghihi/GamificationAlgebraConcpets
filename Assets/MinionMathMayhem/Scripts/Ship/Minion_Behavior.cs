using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MinionMathMayhem_Ship
{
    public class Minion_Behavior : MonoBehaviour
    {

        /*                                  MINION BEHAVIOR
         * This script defines the minion's self personality, generalized attirubutes, and attitude.  This script will allow the minion
         *  to walk and climb at it's own unique pace, and fight with eachother as to who gets to enter into the ship when they run into each other.
         *  In addition, when the minions get flung out of the way, they will screech.
         * 
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      |_ UNITY 3D ENGINE 4.5 OR LATER (atleast tested since)
         *      |_ Minion Controller
         *          |_ Minion Behavior
         *
         * 
         * GOALS:
         *      Minion walks forward at a random speed, depending on the given condition
         *      Minion climbs ladder at a random speed, depending on the given condition
         *      Screech or make sounds
         *      Selected attirubutes
         *          Normal selection; user interaction
         *          Minions fighting with each other
         */



        // Declarations and Initializations
        // ---------------------------------
            // Speed that is used when the minions are climbing the ladder
                private float climbSpeed;
            // Speed that is used when the minions are walking (or running) forward
                private float walkSpeed;
            // Force 'thrust' that is used when the minions have been selected.
                private float force;
            // Minion actions:
                private bool isClimbing = false;
                private bool isWalking = true;
            // Direction in which the Minions are thrusted
                private Vector3 forceDirection; 
            // Multimedia
                // Screeches
                    public AudioClip[] clickedSound;
                // Won
                    public AudioClip[] celebrationSound;
            // Animations and physics
                private Animator minionAnim;
                private CapsuleCollider capsuleCollider;
                private ParticleActivation particleActivation;
            // Accessors and Communication
                // Minion Controller; this is a centralization field for the minion behavior
                    private Minion_Controller scriptMinionController;
        // ----




        // Initialization of specialized variables
        private void Awake()
        {
            // References and Initializations
                minionAnim = GetComponent<Animator>();
                capsuleCollider = GetComponent<CapsuleCollider>();
                particleActivation = GetComponent<ParticleActivation>();

                // Find the GameController tag, and then find the attached script 'Minion_controller'.
                    scriptMinionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Minion_Controller>();

        } // Awake()



        // This function will be called once the actor has been active within the scene
        private void Start()
        {
            // Set the actor's unique attributes.
                SetAttributes();
            // Detect the minion's animation and event state
                StartCoroutine(MinionEventState());
            // Check references
                CheckReferences();
        } // Start()



        // Check the actor's current event and state and probably change the animation state and attributes when necessary.
        private IEnumerator MinionEventState()
        {
            while (true) // Never ending loop
            {
                // Check to see if the minion needs to walk or climb
                if (isWalking == !false)
                    Walk();
                else if (isClimbing == !false)
                    Climb();

                // Brief wait time to ease the CPU
                yield return new WaitForSeconds(0.01f);
            } // While
        } // MinionEventState()



        // Set the actor's unique attributes
        private void SetAttributes()
        {
            // Climbing speed
                climbSpeed = scriptMinionController.ClimbSpeed;

            // Walking speed
                walkSpeed = scriptMinionController.RunningSpeed;

            // Thrust Force
                // The force (or push) used when the minion is selected
                force = scriptMinionController.thrustForce;

            // Thrust Direction
                // Push the minion to the desired direction when selected.
                forceDirection = scriptMinionController.ThrustDirection;

        } // SetAttributes()



        // When the minions 'hit' with other objects, this function is going to be called.
        private void OnTriggerEnter(Collider other)
        {

            // Collided With: Ladder
                if (other.tag == "Ladder")
                {
                    // The minion is climbing
                        isClimbing = true;
                        isWalking = false;
                        minionAnim.SetTrigger("Climb");
                } // End if (ladder)

            // Collided With: Forward Enabler
                else if (other.tag == "ForwardEnabler")
                {
                    // The minion is walking
                        isClimbing = false;
                        isWalking = true;
                        minionAnim.SetTrigger("Walk");
                } // End if (ForwardEnabler)

            // Collided With: FinalDestroyer
                else if (other.tag == "exit")
                {
                    // Do nothing
                } // End if (exit)

            // Collided With: Another Minion
                else if (other.tag == "Minion")
                {
                    // Disable the actor's bounding box
                        MinionCollision_ToggleBoundingBox(other.gameObject as GameObject, false);
                    // Execute the function that will select which minion is the alpha male!
                        MinionCollision(other.gameObject.GetComponent<Minion_Behavior>());
                    // Enable the actor's bounding box
                        MinionCollision_ToggleBoundingBox(other.gameObject as GameObject, true);
                } // End if (Minion)
        } // OnTriggerEnter()



        // Toggle the minion's bounding box during the collision algorithm; this is to avoid the minions from re-executing the entire algorithm again.
        private void MinionCollision_ToggleBoundingBox(GameObject minionActor, bool state)
        {
            // Get the object's colliders
                CapsuleCollider actor1 = gameObject.GetComponent<CapsuleCollider>();
                CapsuleCollider actor2 = minionActor.GetComponent<CapsuleCollider>();
            // ----

            // Toggle the colliders of the objects
                actor1.isTrigger = state;
                actor2.isTrigger = state;
            // ----
        } //MinionCollision_ToggleBoundingBox()



        // When the minions collide with each other, this function will determine which one is going to be the alpha male!
        private void MinionCollision(Minion_Behavior actor)
        {

            int actorSelected = scriptMinionController.MinionCollision(FetchObjectIDAddress(), actor.FetchObjectIDAddress());
            // This gameObject has been selected
                if (actorSelected == FetchObjectIDAddress())
                {
                    if (isClimbing == true)
                    {
                        MinionCollision_Selected_Climbing(true, actor);
                    }
                    else if (isWalking == true)
                    {
                        MinionCollision_Selected_Walking(true, actor);
                    }
                } // if

            // The colliding actor was selected
                else if (actorSelected == actor.FetchObjectIDAddress())
                {
                    if (isClimbing == true)
                    {
                        MinionCollision_Selected_Climbing(false, actor);
                    }
                    else if (isWalking == true)
                    {
                        MinionCollision_Selected_Walking(false, actor);
                    }
                } // else
        } // MinionCollision()



        // Collision occurred during the climbing; the selected minion will fling.
        private void MinionCollision_Selected_Climbing(bool selected, Minion_Behavior actor = null)
        {
            if (selected == true)
                Flick();
            else
                actor.Flick();
        } // MinionCollision_Selected_Climbing()



        // Collision occurred during walking; the selected minion will fling.
        private void MinionCollision_Selected_Walking(bool selected, Minion_Behavior actor = null)
        {
            if (selected == false)
                Flick();
            else
                actor.Flick();
        } // MinionCollision_Selected_Walking()



        // This function will return the object's internal ID Address that is generated from the Unity Engine itself.
        public int FetchObjectIDAddress()
        {
            return GetInstanceID();
        } // FetchObjectIDAddress()



        // This function is called when the minions are going to walk.
        private void Walk()
        {
            transform.Translate(new Vector3(0, 0, 1) * walkSpeed * Time.deltaTime);
        } // Walk()



        // This functions is called when the minions are going to climb
        private void Climb()
        {
            transform.Translate(new Vector3(0, 1, 0) * climbSpeed * Time.deltaTime);
        } // Climb()



        // When the creature has been 'selected', this function will be called
        private void OnMouseDown()
        {
            // Only possible when the minion is on the ladder
            if (isClimbing != false)
            {
                // Selected action
                    Flick();
                // Play sound
                    MinionSqueal();
            }
        } // End of OnMouseDown



        // Actions to take place when the minion has been selected
        public void Flick()
        {
                particleActivation.Emit();
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                isClimbing = false;
                isWalking = false;
                GetComponent<Rigidbody>().AddForce(forceDirection * force);
                Destroy(gameObject, 1f);
                minionAnim.SetBool("isFlicked", true);
                Destroy(capsuleCollider);
        } // Flick()



        // When the Minion actor has been selected, play a sound clip within a array.
        public void MinionSqueal()
        {
            // Select a sound within the array
                GetComponent<AudioSource>().clip = clickedSound[Random.Range(1, clickedSound.Length)];
            // Play the sound clip
                GetComponent<AudioSource>().Play();
        } // MinionSqueal()



        // This function will check to make sure that all the references has been initialized properly.
        private void CheckReferences()
        {
            if (scriptMinionController == null)
                MissingReferenceError("Minion Controller");
        } // CheckReferences()



        // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
        private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
        {
            Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
            Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
            Time.timeScale = 0; // Halt the game
        } // MissingReferenceError()
    } // End of Class
} // Namespace