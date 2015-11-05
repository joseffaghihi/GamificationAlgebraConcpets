using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialWindow_GeneralScript : MonoBehaviour
    {
        /*                                          TUTORIAL WINDOW [GENERAL SCRIPT]
         * This class is designed to offer a standard base as to how the dialog windows will work with the tutorial protocol.
         *
         * GOALS:
         *  Initalizations for the specific dialog window object
         */

        // Declarations and Initializations
        // ---------------------------------
            public GameObject rulesCanvas;
            //  Rules Control on the rulesCanvas GameObject  
                private RulesControl rulesControl;
        // ---------------------------------

        
        
        /// <summary>
        /// Unity function
        /// When this object is active, this function will be called automatically.
        /// </summary>
        private void Awake()
        {
            rulesControl = rulesCanvas.GetComponent<RulesControl>();
        } // Awake()
    } // End of Class
} // Namespace
