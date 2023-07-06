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
	Vector2 currentRotation;
	InputManager inputManager;
	void Start() {
		inputManager = InputManager.instance;
	}

	void OnEnable() {
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}

	void Update() {
		RotateCamera();
	}

	private void RotateCamera()
	{
		if (!inputManager.isIdle) {
			currentRotation.x += inputManager.GetXAxis() * rotationSpeed;
			currentRotation.y -= inputManager.GetYAxis() * rotationSpeed;
			currentRotation.y = Mathf.Clamp(currentRotation.y, minYX, maxYX);
			currentRotation.x = Mathf.Clamp(currentRotation.x, minYX, maxYX);
			transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
		}
	}
}
