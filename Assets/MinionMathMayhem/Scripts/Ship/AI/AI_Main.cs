using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_Main : MonoBehaviour
    {
        /*
         *                                              GAME ARTIFICIAL INTELLIGENCE
         *                                                          MAIN
         * This is the forefront of controlling the environment based on the user's performance and mastery.  This script is a 'always alive' and monitoring
         *  as to how the user is responding to the game and how well they perform.  This is the main part of the AI that centralizes all of the AI components, 
         *  



         * This is the forefront of controlling the environment based on the user performances.  This script is a 'always live' monitoring
         *  the user's performances based on accuracy and reaction time.  This daemon or service will routinely check the user's accuracy and determine if the
         *  game itself should be a bit more difficult or easier, and also check the user's reaction time to determine if the user is capable of clicking the actors
         *  at a expeditious rate or adrenaline rush takes in (which is usually short lived, depending on the user and environment they are in).
         * 
         * 
         * NOTES:
         *  Since I am treating this like a daemon, this is going to hog resources.
         *  
         * 
         * STRUCTURAL DEPENDENCY NOTES:
         *      UserAI
         *          |_ SpawnController
         *              |_ Spawners
         *          |_ < Warten... >
         *              |_ < Warten... >
         *
         * 
         * GOALS:
         *      Always Alive; Monitoring service -- daemon service.
         *      Control the game specific environments
         *      Monitor User Performance
         *          * Accuracy; Spawn more minions
         *                              OR
         *                      Change the complexity level of the DEG.
         *          * Speed; minion speed movement is increased or decreased
         *                      Including climbing leaders and walking.
         *  
         */
    }
}
