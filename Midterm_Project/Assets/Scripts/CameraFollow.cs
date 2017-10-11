using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero; // x, y, and z coordinates
	public Transform target; // transform helps us change position of objects. Ex. Player

	public bool  bounds;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (target) {
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position); //place the camera's view (viewport) on to the player object.
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(.50f,.50f,point.z)); //character pos - camera position. Create new point for camera.
			Vector3 destination = transform.position + delta; // changing point of camera 

			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime); // makes the camera follow smoothly.

			if (bounds) {
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x), Mathf.Clamp(transform.position.y, minCameraPos.y,maxCameraPos.y), Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
				// makes sure that the camera stays within the min and max position set by chosen coordinates

			}

		}
	}
}

