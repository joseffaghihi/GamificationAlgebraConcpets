using UnityEngine;
using System.Collections;


namespace MinionMathMayhem_Ship
{
    public class WhatIsDisplay : MonoBehaviour
    {

        // Declarations and Initializations
        // ---------------------------------
        // Text Object: What Is
        public GameObject whatIsTextbox;
        // Animation: What Is
        private Animator whatIsAnim;
        // Text Object: Index Char
        public GameObject eventLetterTextbox;
        // Animation: Index Char
        private Animator eventLetterAnim;
        // ----




        // This function will be called when the object is declared in the scene and activated.
        void Awake()
        {
            whatIsAnim = whatIsTextbox.GetComponent<Animator>();
            eventLetterAnim = eventLetterTextbox.GetComponent<Animator>();
        } // Awake()



        // Plays the what is "A, B, or C" animation
        private IEnumerator NextLetterEventPlay(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            whatIsAnim.SetTrigger("Slide");
            eventLetterAnim.SetTrigger("SlideIn");
        } // NextLetterEventPlay()



        // Allow other objects to gain access to the 'NextLetterEventPlay' function.
        public void Access_NextLetterEventPlay(float waitTime)
        {
            StartCoroutine(NextLetterEventPlay(waitTime));
        } // Access_NextLetterEventPlay()
    } // End of Class
} // Namespace