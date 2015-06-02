using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class SpawnController : MonoBehaviour
    {
        /*                    SPAWNER CONTROLLER
         * This class will simply send a signal to the spawner actors to summon the minion actors into the scene.
         *  
         * GOALS:
         *  Determine the next spawn
         *  Send spawn signal to the spawners
         */



        // Declarations and Initializations
        // ---------------------------------

        // Accessors and Communication
            // Spawners (Summoning batches)
                public delegate void SpawnBatchMinions();
                public static event SpawnBatchMinions EnableSpawnPoint;
            // Spawner Objects
                // These will be used to have complete utter control over the spawners individually.
                    // Spawner 0
                        public Spawner spawnerObject0;
                    // Spawner 1
                        public Spawner spawnerObject1;
                    // Spawner 2
                        public Spawner spawnerObject2;
        // ----


        

        // Signal Listener: Detected
        private void OnEnable()
        {
            Waves.SummonMinion_Batches += SpawnMinionBatch;
            Waves.SummonMinion_OnlyOne += SpawnMinion;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            Waves.SummonMinion_Batches -= SpawnMinionBatch;
            Waves.SummonMinion_OnlyOne -= SpawnMinion;
        } // OnDisable()



        // Only spawn 'one' minion from a random spawner.
        private void SpawnMinion()
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    spawnerObject0.SpawnForcibly_public();
                    break;
                case 1:
                    spawnerObject1.SpawnForcibly_public();
                    break;
                case 2:
                    spawnerObject2.SpawnForcibly_public();
                    break;
            } // Switch
        } // SpawnMinion()



        // Send a broadcast message to the spawners to activate.
        private void SpawnMinionBatch()
        {
            EnableSpawnPoint();
        } // SpawnMinionBatch()
    } // End of Class
} // Namespace