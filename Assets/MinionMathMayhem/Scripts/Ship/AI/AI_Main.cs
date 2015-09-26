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
         *  and enforces the AI components to execute by the main heartbeat fequency defined.
         *
         * NOTES:
         *  Since I am treating this like a daemon or 'always alive' monitoring service, this is going to be rather intensive with the host resources.
         *
         * STRUCTUAL DEPENDENCY NOTES:
         *      MAIN [AI]
         *          |_ AI_UserResponse
         *          |_ AI_UserMastery
         *
         * GOALS:
         *      Always running service; depends on the heartbeat frequency.
         *      Allows AI components to execute and monitor or control the environment based on the user's feedback.
         *      Try to aid the user, based on their mastery over the material, by either assisting them to learn or to challenge them.
         */
    }
}
