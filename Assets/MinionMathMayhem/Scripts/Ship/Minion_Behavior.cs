using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MinionMathMayhem_Ship
{
    public class Minion_Behavior : MonoBehaviour
    {

        /*                      MINION BEHAVIOR
         * Designed for the minions, specifically for actor activity and attributes.
         * 
         * GOALS:
         *  Minion walks forward, depending on the given condition
         *  Minion climbs ladder, depending on the given condition
         *  Screech when selected
         *  Specialized activity when selected (or flicked)
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



        // This function will be called once the actor has been summoned within the scene
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
                    // Get the collided object's script
                    Minion_Behavior actorCollided = other.gameObject.GetComponent<Minion_Behavior>();

                    // Determine what was selected
                    if (scriptMinionController.MinionCollision(FetchObjectIDAddress(), actorCollided.FetchObjectIDAddress()) == FetchObjectIDAddress())
                        // This actor was selected
                        Flick();
                    else
                        // The collided actor was selected
                        actorCollided.Flick();
                } // End if (Minion)
        } // OnTriggerEnter()



        // This function will return the object's internal ID Address that is generated from Unity.
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