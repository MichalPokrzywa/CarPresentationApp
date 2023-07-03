using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class OutsideCameraRotation : MonoBehaviour {

	public float distance = 3f;
	public float rotationSpeed = 5f;
	public Transform car;

	private float polar;  // Polar angle
	private float elevation;    // Azimuthal angle
	private float radius;
	public float minY = 0.5f;
	public float maxY = 4f;
	private Vector3 velocity;

	void Start() {

	}

	void Update() {
		//if (Input.GetMouseButton(0)) {
		//	rb.velocity = Vector3.zero;
		//	rb.angularVelocity = Vector3.zero;
		//	float pointerX = Input.GetAxis("Mouse X");
		//	if (Input.touchCount > 0) {
		//		rb.AddRelativeTorque(transform.up * 5f * Input.touches[0].deltaPosition.x);
		//	}
		//	else {
		//		float pointerY = Input.GetAxis("Mouse Y");
		//		rb.AddRelativeTorque(55f * pointerY, 75 * pointerX,0);
		//	}
		//}
		// Update the angles based on input

		if (Input.GetMouseButton(0)) {
			// Convert the camera's position to spherical coordinates
			Vector3 relativePosition = transform.position - car.position;
			CartesianToSpherical(relativePosition, out radius,out polar,out elevation);
			// Update the angles based on mouse input
			polar += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
			elevation += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
			// Convert spherical coordinates back to Cartesian coordinates
			Vector3 cartesianCoords;
			SphericalToCartesian(radius, polar, elevation, out cartesianCoords);
			// Set the camera position relative to the car
			transform.position = car.position + cartesianCoords;

			// Clamp the final transform position's Y coordinate between the specified range
			Vector3 finalPosition = transform.position;
			finalPosition.y = Mathf.Clamp(finalPosition.y, minY, maxY);
			transform.position = finalPosition;
		}

		// Look at the car
		transform.LookAt(car);
	}
	public static void CartesianToSpherical(Vector3 cartCoords, out float outRadius, out float outPolar, out float outElevation) {
		if (cartCoords.x == 0)
			cartCoords.x = Mathf.Epsilon;
		outRadius = Mathf.Sqrt((cartCoords.x * cartCoords.x)
		                       + (cartCoords.y * cartCoords.y)
		                       + (cartCoords.z * cartCoords.z));
		outPolar = Mathf.Atan(cartCoords.z / cartCoords.x);
		if (cartCoords.x < 0)
			outPolar += Mathf.PI;
		outElevation = Mathf.Asin(cartCoords.y / outRadius);
	}
	public static void SphericalToCartesian(float radius, float polar, float elevation, out Vector3 outCart) {
		float a = radius * Mathf.Cos(elevation);
		outCart.x = a * Mathf.Cos(polar);
		outCart.y = radius * Mathf.Sin(elevation);
		outCart.z = a * Mathf.Sin(polar);
	}
	// Update is called once per frame
	void FixedUpdate() {
	    //CameraDecelerationY();
	    
	}

    private void CameraDecelerationY()
    {
	    //if (Mathf.Abs(rb.angularVelocity.y) > 0.6f)
	    //{
		   // if (rb.angularVelocity.y > 0)
		   // {
			  //  rb.angularVelocity -= new Vector3(0, 0.4f, 0) * Time.fixedDeltaTime;
		   // }
		   // else
		   // {
			  //  rb.angularVelocity -= new Vector3(0, -0.4f, 0) * Time.fixedDeltaTime;
		   // }
	    //}
	    //else if (Mathf.Abs(rb.angularVelocity.y) < 0.1f) {
		   // rb.angularVelocity = Vector3.zero;
	    //}
	    //else {
		   // if (rb.angularVelocity.y > 0) {
			  //  rb.angularVelocity -= new Vector3(0, 0.2f, 0) * Time.fixedDeltaTime;
		   // }
		   // else {
			  //  rb.angularVelocity -= new Vector3(0, -0.2f, 0) * Time.fixedDeltaTime;
		   // }
	    //}
    }
}
