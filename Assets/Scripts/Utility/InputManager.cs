using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private static InputManager _instance;
	public static InputManager instance => _instance;

	[SerializeField] float simulateAxisValue = 0.5f; // Value to simulate when the program is idle
	public bool isIdle;

	void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(gameObject);
		}
		else { 
			_instance = this;
		}
	}

	public bool DetectMovement() {
		if (Input.GetMouseButton(0) || Input.touchCount > 0) {
			return true;
		}
		return false;
	}


	public float GetXAxis() {
	    if (isIdle) {
		    return simulateAxisValue;
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
	    if (Input.GetMouseButton(0)) {
		    return Input.GetAxis("Mouse Y");
	    }
	    if (Input.touchCount > 0) {
		    return Input.touches[0].deltaPosition.y;
	    }
	    return 0;
    }

    public float GetZoom() {

	    if (Input.touchCount == 2) {
		    Touch touchZero = Input.GetTouch(0);
		    Touch touchOne = Input.GetTouch(1);

		    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

		    float difference = currentMagnitude - prevMagnitude;
		    return difference * 0.01f;
	    }
	    if (Input.GetMouseButton(0)) {
		    return Input.GetAxis("Mouse ScrollWheel");
	    }
	    return 0;
    }
}
