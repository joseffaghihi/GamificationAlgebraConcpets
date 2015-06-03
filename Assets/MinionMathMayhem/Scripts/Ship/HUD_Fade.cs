using UnityEngine;
using System.Collections;


namespace MinionMathMayhem_Ship
{
    public class HUD_Fade : MonoBehaviour
    {
        /*
         *              HEADS UP DISPLAY FADE
         *
         * This class will simply have the HUD fade in and fade out depending on the situation.  The idea of this feature is to clear the screen 
         * when an event is happening.  An example of an event, tutorial, game over, and the like.
         * 
         * DEPENDENCY OBJECT NOTES:
         *      HUD_Fade
         *          |_ Score
         *          |_ Problem Box
         *          |_ Letter
         *          
         * GOALS:
         *      * Clear the HUD off the screen by fading it out
         *          The objects are NOT removed from the world!
         *      * Bring back the HUD on the screen by fading it back in.
         *
         */


        // Declarations and Initializations
        // ---------------------------------
            // Normal Fade Target [when the game is playing]
                private float alphaChannelNormal;
            // Hide Fade Target [Fade out]
                public float alphaChannelHide = 0.0f;
            // Speed of fader
                public float alphaChangeSpeed = 0.0f;
        // ---------------------------------




        // Signal Listener: Detected
        private void OnEnable()
        {
            // Tutorial states
                MoviePlay.TutorialStateEnded += RestoreHUD;
                GameController.TutorialStateStart += HideHUD;
            // Game State
                GameController.GameStateEnded += HideHUD;
                GameController.GameStateRestart += RestoreHUD;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Tutorial states
                MoviePlay.TutorialStateEnded -= RestoreHUD;
                GameController.TutorialStateStart -= HideHUD;
            // Game State
                GameController.GameStateEnded -= HideHUD;
                GameController.GameStateRestart -= RestoreHUD;
        } // OnDisable()



        // Immediately execute when the game object is available within the scene.
        private void Start()
        {
            // Make sure that the mutable variables do not contain negative values.
                CheckValues();
            // Initialize the default fade level with the desired current alpha value.
                alphaChannelNormal = gameObject.GetComponent<CanvasGroup>().alpha;
        } // Start()



        // Hide the HUD from the scene [NOTE: it's _NOT_ thrashed nor disabled]
        private void HideHUD()
        {
            gameObject.GetComponent<CanvasGroup>().alpha = alphaChannelHide;
        } // HideHUD()



        // Restore the HUD back to the scene
        private void RestoreHUD()
        {
            gameObject.GetComponent<CanvasGroup>().alpha = alphaChannelNormal;
        } // RestoreHUD()



        // Prevent negative values
        private void CheckValues()
        {
            if (alphaChannelHide < 0)
                alphaChannelHide = (alphaChannelHide * -1);
            if (alphaChangeSpeed < 0)
                alphaChangeSpeed = (alphaChangeSpeed * -1);
            if (alphaChannelNormal < 0) // Added this just incase it's possible to add negative values within Unity's inspector.
                alphaChannelNormal = (alphaChannelNormal * -1);
        } // CheckValues()
    } // End of Class
} // Namespace