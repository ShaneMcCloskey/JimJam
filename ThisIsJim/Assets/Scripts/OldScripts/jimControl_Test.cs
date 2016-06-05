using UnityEngine;
using System.Collections;

public class jimControl_Test : MonoBehaviour {

	//MOVEMENT Variables:
	public CharacterController controller;
	public Animator anim;
	public Transform graphicHolder;

	public float speed = 10.0f;
	public float initialSpeed = 10.0f;
	public float jumpSpeed = 350.0f;
	public bool jumpEnabled = false;
	public float gravity = 10.0f;
	public bool grounded = true; 
	public Vector3 moveDirection = Vector3.zero;
	private bool stopped = false; //for in-air?
	//because
	public float x;
	public float z;

	void Awake (){

	}

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<CharacterController>(); //CharacterController Component
		initialSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, 0f); //should activate collision check every frame, when not movin

		x = Input.GetAxis("Horizontal"); //see old NUBs script about getting controllers in here
		z = Input.GetAxis("Vertical");

		//MOVEMENT:
		grounded = controller.isGrounded; //checking grounded functionality
		moveDirection.y -= gravity * Time.deltaTime; //Gravity
		controller.Move(moveDirection * Time.deltaTime); //General Movement //any .Move function should be called after gravity or not called every frame

		Vector3 dir = new Vector3(x,0f,z); 
		moveDirection = new Vector3(x, 0f, z); //allows input from axes
		moveDirection = transform.TransformDirection(moveDirection); //tells it how to move
		moveDirection.x *= (speed); //how fast to move
		moveDirection.z *= (speed); //how fast to move
		
		
		if (controller.isGrounded == true) { //Abilities player has while grounded
			if (dir.x > 0.1f || dir.z > 0.1f){ //moving
				//walking sound here
			}
			else{
				//audio.Stop();
				if (stopped == false){
					speed = initialSpeed;
				}
			}
		}
		else //In the AIR/not grounded
		{
			speed = (initialSpeed/1.2f);
		}
		//Vector3 temp = graphicHolder.localScale;
		if (dir.x > 0.1f) {
			anim.SetBool ("walking", true);
			//ballLaunch.localScale = temp;
			//graphicHolder.localScale.x = temp.x;
			graphicHolder.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
		if (dir.x < -0.1f) {
			anim.SetBool ("walking", true);
			graphicHolder.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
			//graphicHolder.localScale.x = -1.0f;
		}
		if (dir.x < 0.1f && dir.x > -0.1f) {
			anim.SetBool ("walking", false);
		}

}

	void FixedUpdate (){
		if (controller.isGrounded == true) {	
			if (Input.GetButton ("Jump") && jumpEnabled == true) { 
				//anim.SetTrigger("JumpingTrigger");
				Jump (jumpSpeed);
				jumpEnabled = false;
			}
		}
		else{
			jumpEnabled = true;
		}
	}

	void Jump (float addedJumpHeight) {
		moveDirection.y = (addedJumpHeight * Time.deltaTime);	
	}
}