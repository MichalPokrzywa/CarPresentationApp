using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public float idleTimeThreshold = 60f; // Time threshold for considering the program idle (in seconds)
	public float simulateAxisValue = 0.5f; // Value to simulate when the program is idle
	private float timer;
	private float idleTimer;
	private bool isIdle;
	// Start is called before the first frame update
	void Start()
    {
        timer = Time.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetXAxis() {
	    if (isIdle) {
		    return 0.5f;
	    }
	    return Input.GetAxis("Mouse X");
    }

}
