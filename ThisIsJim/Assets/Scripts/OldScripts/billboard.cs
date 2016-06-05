using UnityEngine;
using System.Collections;

public class billboard : MonoBehaviour {

	public bool billboardThis = true;
	public bool noRotate3D = false;
	public bool noRotate2D = false;
	public bool limitedRotation = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (billboardThis) {
			transform.LookAt (Camera.main.transform.position, -Vector3.up);
		}

		if (noRotate3D) {
			transform.eulerAngles = new Vector3(0,0,0);
		}

		if (noRotate2D) {
			transform.eulerAngles = new Vector3(90,0,0);
		}

		if (limitedRotation) {
			//Mathf.Clamp(transform.rotation.y, 0, 45);
			//transform.eulerAngles = new Vector3(0, Mathf.Clamp(transform.rotation.y, -80, 80), 0);
		}
	}
}
