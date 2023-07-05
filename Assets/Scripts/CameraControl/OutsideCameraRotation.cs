using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class OutsideCameraRotation : MonoBehaviour {

	public float rotationSpeed = 5f;
	public Transform car;

	private float polar;  // Polar angle
	private float elevation;    // Azimuthal angle
	private float radius;
	public float minY = 0.2f;
	public float maxY = 0.7f;
	public float zoomSpeed = 50f;
	private float zoomLevel = 1f;
	private Vector3 velocity = Vector3.zero;
	private Vector3 lastPosition;
	private float lastPolar;
	private bool flag = false;
	Vector3 cartesianCoordsNextPosition;
	void Start() {
		lastPolar = 0;
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
		
		Vector3 relativePosition = transform.position - car.position;
		lastPosition = transform.position;
		
		CartesianToSpherical(relativePosition, out radius, out polar, out elevation);
		if (Input.GetMouseButton(0)) {
			lastPolar = polar;
			radius -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
			polar += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
			if (elevation < 0.6f && elevation > 0.25f) {
				elevation += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
			}

			radius = Mathf.Clamp(radius, 4.1f, 5.9f);
			elevation = Mathf.Clamp(elevation, 0.25f, 0.6f);
			Debug.Log($"mouse input: {Input.GetAxis("Mouse X")} | elevation:{elevation} | polar:{polar} | lastPolar:{lastPolar} | radius:{radius}");
			// Convert spherical coordinates back to Cartesian coordinates
			Vector3 cartesianCoords;
			SphericalToCartesian(radius, polar, elevation, out cartesianCoords);
			// Set the camera position relative to the car
			transform.position = car.position + cartesianCoords;
			//Debug.Log($"ACT POS: {transform.position} | LAST POS: {lastPosition}");
		}
		if (Input.GetMouseButtonUp(0)) {
			
			float deltaPolar = polar - lastPolar;
			float temp;
			if (deltaPolar < 0) {
				temp = polar + (deltaPolar * 10) * rotationSpeed;
			}
			else {
				temp = polar - (deltaPolar * 10) * rotationSpeed;
			}
			SphericalToCartesian(radius, temp, elevation, out cartesianCoordsNextPosition);
		}
		if (lastPolar != 0 && !Input.GetMouseButton(0)) {
			//smooth time jako input speed
			transform.position = Vector3.SmoothDamp(transform.position, car.position + cartesianCoordsNextPosition, ref velocity, 1f);
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
