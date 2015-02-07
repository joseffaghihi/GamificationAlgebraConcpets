using UnityEngine;
using System.Collections;

public class ForwardEnabler : MonoBehaviour
{
    public GameObject minionClone;
    private ForwardDriver_2 forwardDriver_2;
    private UpDriver upDriver;

    void Awake()
    {
        forwardDriver_2 = minionClone.GetComponent<ForwardDriver_2>();
        upDriver = minionClone.GetComponent<UpDriver>();
    }

	void Start ()
    {
	    
	}

    void OnTriggerEnter()
    {
        upDriver.enabled = false;
        forwardDriver_2.enabled = true;
    }
	
}
