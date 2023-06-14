using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

	private Rigidbody rb;
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (Input.GetMouseButton(0)) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			float pointerX = Input.GetAxis("Mouse X");
			if (Input.touchCount > 0) {
				//rb.AddRelativeTorque(transform.up * 5f * Input.touches[0].deltaPosition.x);
			}
			else {
				float pointerY = Input.GetAxis("Mouse Y");
				rb.AddRelativeTorque(55f * pointerY, 75 * pointerX,0);
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate() {
	    CameraDecelerationY();
	    
	}

    private void CameraDecelerationY()
    {
	    if (Mathf.Abs(rb.angularVelocity.y) > 0.6f)
	    {
		    if (rb.angularVelocity.y > 0)
		    {
			    rb.angularVelocity -= new Vector3(0, 0.4f, 0) * Time.fixedDeltaTime;
		    }
		    else
		    {
			    rb.angularVelocity -= new Vector3(0, -0.4f, 0) * Time.fixedDeltaTime;
		    }
	    }
	    else if (Mathf.Abs(rb.angularVelocity.y) < 0.1f) {
		    rb.angularVelocity = Vector3.zero;
	    }
	    else {
		    if (rb.angularVelocity.y > 0) {
			    rb.angularVelocity -= new Vector3(0, 0.2f, 0) * Time.fixedDeltaTime;
		    }
		    else {
			    rb.angularVelocity -= new Vector3(0, -0.2f, 0) * Time.fixedDeltaTime;
		    }
	    }
    }
}
