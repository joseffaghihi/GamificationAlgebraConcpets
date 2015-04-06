using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class ParticleActivation : MonoBehaviour
    {
        private ParticleSystem particles;

        // Use this for initialization
        void Start()
        {
            particles = gameObject.GetComponentInChildren<ParticleSystem>();
            if (particles == null)
            {
                Debug.Log("No Particle System Found on minion game object");
            }
            else
            {
                Debug.Log("Found!");
            }
        }

        public void Emit()
        {
            particles.Play();
        }
    }
} // Namespace