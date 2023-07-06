using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutsideCameraRotation : MonoBehaviour {

	[SerializeField] float rotationSpeed = 5f;
	[SerializeField] Transform car;
	[SerializeField] float minY = 0.25f;
	[SerializeField] float maxY = 0.6f;
	[SerializeField] float minZoom = 4.1f;
	[SerializeField] float maxZooms = 5.9f;
	[SerializeField] float zoomSpeed = 50f;
	float lastPolar;
	float polar;  // Polar angle
	float elevation;    // Azimuthal angle
	float radius;
	Vector3 velocity = Vector3.zero;
	Vector3 cartesianCoordsNextPosition;
	Vector3 cartesianCoords;
	Vector3 relativePosition;
	InputManager inputManager;
	void Start() {
		lastPolar = 0;
		inputManager = InputManager.instance;
	}

	void Update() {
		relativePosition = transform.position - car.position;
		MathUtylity.CartesianToSpherical(relativePosition, out radius, out polar, out elevation);
		if (inputManager.DetectMovement() || inputManager.isIdle) {
			RotateCameraAround();
			CalculateNextPosition();
		}
		if (lastPolar != 0 && !inputManager.DetectMovement() && !inputManager.isIdle) {
			//smooth time as input speed
			transform.position = Vector3.SmoothDamp(transform.position, car.position + cartesianCoordsNextPosition, ref velocity, 1f);
		}
		transform.LookAt(car);
	}

	private void CalculateNextPosition()
	{ 
		float deltaPolar = polar - lastPolar;
		float temp;
		if (deltaPolar < 0)
		{
			temp = polar + (deltaPolar * 10) * rotationSpeed;
		}
		else
		{
			temp = polar - (deltaPolar * 10) * rotationSpeed;
		}
		MathUtylity.SphericalToCartesian(radius, temp, elevation, out cartesianCoordsNextPosition);
	}

	private void RotateCameraAround()
	{
		lastPolar = polar;
		radius -= inputManager.GetZoom() * zoomSpeed * Time.deltaTime;
		polar += inputManager.GetXAxis() * rotationSpeed * Time.deltaTime;
		if (elevation < maxY && elevation > minY)
		{
			elevation += inputManager.GetYAxis() * rotationSpeed * Time.deltaTime;
		}

		radius = Mathf.Clamp(radius, minZoom, maxZooms);
		elevation = Mathf.Clamp(elevation, minY, maxY);
		Debug.Log(
			$"mouse input: {Input.GetAxis("Mouse X")} | elevation:{elevation} | polar:{polar} | lastPolar:{lastPolar} | radius:{radius}");
		// Convert spherical coordinates back to Cartesian coordinates
		MathUtylity.SphericalToCartesian(radius, polar, elevation, out cartesianCoords);
		// Set the camera position relative to the car
		transform.position = car.position + cartesianCoords;
	}
}
