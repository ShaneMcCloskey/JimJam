using UnityEngine;
using System.Collections;

/*
	 Jim Navigation works like AI. There needs to be points of interest within an area that Jim can randomly grab and.
	 	play animations to accordingly.
	 	
 	Jim needs a set time without player input before he starts moving and deciding picking his own actions.

 */

public class jimControl : MonoBehaviour {

	//MOVEMENT Variables:
	public NavMeshAgent agent;
	public Animator anim;
	public SpriteRenderer graphicHolder;

	public float inactiveTimer = 0.0f;
	float initialInactiveTimerLimit = 8.0f;
	public float inactiveTimerLimit = 3.0f;
	public Vector2 inactiveTimerMinMax = new Vector2(2.0f, 5.0f);
	public bool inactive = false;
	public float walkRadius = 5.0f; //radius in which Jim will randomly walk

	RaycastHit hit;
	Ray ray;

	void Awake (){
		initialInactiveTimerLimit = inactiveTimerLimit;
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, 0f); //should activate collision check every frame, when not moving

		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonDown(0)) {
			print ("ran");
			if (Physics.Raycast(ray, out hit, 100)) {
				agent.destination = hit.point;
			}
			if (hit.collider.gameObject.tag == "Interactable") {
				print ("works1");
				hit.collider.gameObject.SendMessage ("Hover");
			}
			print (hit.collider.gameObject.name);
		}

		if (Physics.Raycast(ray, out hit, 100)){
			if (hit.collider.gameObject.tag == "Interactable") {
				print ("works1");
				hit.collider.gameObject.SendMessage ("Hover");
			}
		}

		//INACTIVE JIM
		if (Input.anyKeyDown) {
			inactiveTimer = 0.0f;
			inactive = false;
			inactiveTimerLimit = initialInactiveTimerLimit;
		} else {
			inactiveTimer += 1.0f * Time.deltaTime; 
		}

		if (inactive == false) {
			if (inactiveTimer >= inactiveTimerLimit) {
				inactiveTimer = 0.0f;
				inactive = true;
				RandomSuggestion ();
			}
		}
		if (inactive == true) {
			if (inactiveTimer >= inactiveTimerLimit) {
				inactiveTimer = 0.0f;
				RandomSuggestion ();
			}
		}



		print ("Speed: " + agent.desiredVelocity);
		//MOVEMENT:
		
		if (agent.desiredVelocity != Vector3.zero) {
			anim.SetBool ("walking", true);
		}
		if (agent.desiredVelocity == Vector3.zero) {
			anim.SetBool ("walking", false);
		}
		if (agent.desiredVelocity.x < -1.0f) {
			graphicHolder.flipX = true;
			print ("left");
		} 
		if (agent.desiredVelocity.x > 1.0f) {
			graphicHolder.flipX = false;
			print ("right");
		}
	


}

	void FixedUpdate (){
	}

	void RandomSuggestion (){
		inactiveTimerLimit = Random.Range (inactiveTimerMinMax.x, inactiveTimerMinMax.y);
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		agent.SetDestination (finalPosition);
	}
}