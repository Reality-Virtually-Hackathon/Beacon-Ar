using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour 
{
	public GameObject cameraAR;
	public GameObject camera360;

	private bool gyroEnabled;
	private Gyroscope gyro;

	private GameObject cameraContainer;
	private Quaternion rot;

	private bool is360;

	private void Start()
	{
		is360 = false;
		cameraAR.SetActive (true);
		camera360.SetActive (false);

		cameraContainer = new GameObject ("Camera Container");
		cameraContainer.transform.position = transform.position;
		transform.SetParent (cameraContainer.transform);

		gyroEnabled = EnableGyro ();
	}

	private bool EnableGyro()
	{
		if (SystemInfo.supportsGyroscope) 
		{
			gyro = Input.gyro;
			gyro.enabled = true;

			cameraContainer.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
			rot = new Quaternion (0, 0, 1, 0);

			return true;
		}
		return false;
	}
	private void Update()
	{
		if (Input.touchCount > 0 && is360 == false) {
			is360 = true;
			print ("clicked");
			camera360.SetActive (true);
			cameraAR.SetActive (false);
		}

		if (is360) {
			transform.localRotation = gyro.attitude * rot;
		}
}	
}