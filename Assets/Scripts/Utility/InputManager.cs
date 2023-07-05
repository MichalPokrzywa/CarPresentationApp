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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetXAxis() {
	    if (isIdle) {
		    return 0.5f;
	    }
	    if (Input.GetMouseButton(0)) {
		    return Input.GetAxis("Mouse X");
	    }
	    if (Input.touchCount > 0) {
		    return Input.touches[0].deltaPosition.x;
	    }
	    return 0;
    }
    public float GetYAxis() {
	    if (isIdle) {
		    return 0.5f;
	    }
	    if (Input.GetMouseButton(0)) {
		    return Input.GetAxis("Mouse Y");
	    }
	    if (Input.touchCount > 0) {
		    return Input.touches[0].deltaPosition.y;
	    }
	    return 0;
    }


}
