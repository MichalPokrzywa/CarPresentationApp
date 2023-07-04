using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class InsideCameraRotation : MonoBehaviour {

	public float rotationSpeed = 5f;
	public float minYX = -50f;
	public float maxYX = 50f;
	private Vector2 currentRotation;

	void Start() {

	}

	void OnEnable() {
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}

	void Update() {

		if (Input.GetMouseButton(0)) {
			// Update the angles based on mouse input
			currentRotation.x += Input.GetAxis("Mouse X") * rotationSpeed;
			currentRotation.y -= Input.GetAxis("Mouse Y") * rotationSpeed;
			currentRotation.y = Mathf.Clamp(currentRotation.y, minYX, maxYX);
			currentRotation.x = Mathf.Clamp(currentRotation.x, minYX, maxYX);
			// Set the camera position relative to the car
			transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
		}
	}

}
