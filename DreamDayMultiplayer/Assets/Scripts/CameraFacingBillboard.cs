using UnityEngine;
using System.Collections;

//COPIED SCRIPT

public class CameraFacingBillboard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Camera cam = Camera.main;

		transform.LookAt(cam.transform);
	}

}