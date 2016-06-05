using UnityEngine;
using System.Collections;

public class cameraMain : MonoBehaviour {

	public Transform target;
	public Vector3 targetPos;
	public Vector2 xMinMax = new Vector2 (-5, 5);
	public Vector2 zMinMax = new Vector2 (-5, 5);

	public float defaultCamSize = 6.2f; //size of orthographic Camera
	public float cameraZoomSize = 4.0f; //size of orthographic Camera
	public float camZoomSpeed = 1.5f;
	public float dampTime = 0.15f;

	Transform target2;
	bool zoomedIn = false;
	Camera cam;
	float targetCamSize = 6.0f;


	// Use this for initialization
	void Start () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		cam = gameObject.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (zoomedIn == false) {
			targetPos = target.position;
		} else {
			targetPos = ((target.position - target2.position)/2);
		}
		float destinationSize = Mathf.Lerp (cam.orthographicSize, targetCamSize, Time.deltaTime * camZoomSpeed);
		cam.orthographicSize = destinationSize;

		Vector3 destination = Vector3.Lerp (transform.position, target.position, Time.deltaTime);
		Vector3 newPosition;
		newPosition = new Vector3 (destination.x, transform.position.y, destination.z);
		Vector3 finalPosition;
		finalPosition.x = Mathf.Clamp (newPosition.x, xMinMax.x, xMinMax.y);
		finalPosition.z = Mathf.Clamp (newPosition.z, zMinMax.x, zMinMax.y);

		transform.position = new Vector3 (finalPosition.x, transform.position.y, finalPosition.z);
	}

	public void FramePlayerAndObject (GameObject item){
		target2 = item.transform;
		//cam.orthographicSize = cameraZoomSize;
		targetCamSize = cameraZoomSize;
		zoomedIn = true;
	}

	public void UnFramePlayerAndObject (){
		//cam.orthographicSize = defaultCamSize;
		targetCamSize = defaultCamSize;
		zoomedIn = false;
	}
}
