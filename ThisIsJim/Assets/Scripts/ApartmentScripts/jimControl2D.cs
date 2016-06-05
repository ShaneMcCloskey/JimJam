using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
	 Jim Navigation works like AI. There needs to be points of interest within an area that Jim can randomly grab and.
	 	play animations to accordingly.
	 	
 	Jim needs a set time without player input before he starts moving and deciding picking his own actions.

    BUG LIST:
    Sentient Jim will interact with a thing, the narrator box comes up, Jim leaves in the middle, the box is still up,
    then he interacts with something else and the box gets cancelled out. Gotta make sure narrator box gets reset
    inactiveTimer keeps going if Jim is simply moving to a random spot or performing an idle

 */

public class jimControl2D : MonoBehaviour {

	//MOVEMENT Variables:
	public NavMeshAgent agent;
	public Animator anim;
	public SpriteRenderer graphicHolder;

	public float inactiveTimer = 0.0f;
    public float initialTimerLimit = 8.0f;
    public float inactiveTimerLimit = 3.0f;
	public Vector2 inactiveTimerMinMax = new Vector2(2.0f, 5.0f);
	public float walkRadius = 5.0f; //radius in which Jim will randomly walk

	public GameObject narratorBox;
	public Text narratorBoxText;
	public float typeTimer = 1.5f; //how long it takes for the text to type out
	public GameObject[] interactableObjects;
    public GameObject[] idlePointsOfInterest; //randomly track Jim to these then play an idle
    jimSentientIdles jimIdles; //Updated from RandomSuggestion RandomIdle
	public bool clickToMoveEnabled = true; //means Jim is sentient, or in the middle of something, the exitTrigger script is currently affecting this
    public bool jimIsBusy = false;
    public bool jimSentient = false;
    bool sentientCheck = false;
	RaycastHit hit;
	Ray ray;

	bool trackingToObject = false; //check to see if Jim is still heading towards the player's intended interaction
	Vector3 currentDestination = Vector3.zero;
	float storedSpeed;

	void Awake (){
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		jimIdles = GetComponent<jimSentientIdles> ();
		interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        idlePointsOfInterest = GameObject.FindGameObjectsWithTag("IdlePOI");        narratorBoxText = narratorBox.GetComponent<Text>();
		narratorBox.SetActive (false);
		currentDestination = agent.destination;
		storedSpeed = agent.speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, 0f); //should activate collision check every frame, when not moving
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//CLICK to MOVE
		if (Input.GetMouseButtonDown(0) && clickToMoveEnabled == true)
        {
            inactiveTimerLimit = initialTimerLimit;
            jimSentient = false;
            jimIsBusy = false;
            trackingToObject = false; //resets
			//clickToMoveEnabled = false; //this will freeze jim if he clicks outside of the apt scene
			if (Physics.Raycast(ray, out hit, 100)) {
				agent.destination = hit.point;
                StartCoroutine(Wait(0.45f, true));
            }
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    clickToMoveEnabled = false;
                    //agent.destination = hit.collider.GetComponent<interactableObject>().pointOfInterest.position;
                    //print ("hit interactable object");
                    StartCoroutine(moveToObject(hit.collider));
                    StartCoroutine(Wait(0.75f, true));
                }
            }
            if (sentientCheck != jimSentient)
            {
                sentientCheck = jimSentient;
                narratorBox.SetActive(false);
            }

        }

		//Blink test
		if (agent.destination != null && agent.destination != currentDestination) {
			//print ("blinking");
			currentDestination = agent.destination;
			StartCoroutine(Blink ());
		}

		//HOVER
		if (Physics.Raycast(ray, out hit, 100)){
			//if (hit.collider.gameObject.tag == "Interactable") {
			//	hit.collider.gameObject.SendMessage ("Hover");
			//}
		}

		//INACTIVE JIM
		if (Input.anyKeyDown || jimIsBusy == true) {
			inactiveTimer = 0.0f;
		} else {
			inactiveTimer += 1.0f * Time.deltaTime; 
		}

		if (clickToMoveEnabled == false) {
			inactiveTimer = 0.0f;
		}

        if (inactiveTimer >= inactiveTimerLimit) {
			inactiveTimer = 0.0f;
			RandomSuggestion ();
		}



		//print ("Speed: " + agent.desiredVelocity);
		//MOVEMENT:
		
		if (agent.desiredVelocity != Vector3.zero) {
			anim.SetBool ("walking", true);
		}
		if (agent.desiredVelocity == Vector3.zero) {
			anim.SetBool ("walking", false);
		}
		if (agent.desiredVelocity.x < -1.0f) {
			graphicHolder.flipX = true; //print ("left");
		} 
		if (agent.desiredVelocity.x > 1.0f) {
			graphicHolder.flipX = false; //print ("right");
		}

}
	IEnumerator Blink(){
		agent.speed = (storedSpeed / 1000.0f);
		anim.SetTrigger ("blink");
		yield return new WaitForSeconds (0.2f);
		currentDestination = agent.destination;
		yield return new WaitForSeconds (0.05f);
		agent.speed = storedSpeed;
	}

	IEnumerator moveToObject(Collider other){
		trackingToObject = true;
		interactableObject iObject = other.GetComponent<interactableObject>();
        if (iObject.anim)
        {
            iObject.anim.SetBool("HoverLock", true);
        }
		agent.destination = iObject.pointOfInterest.position;
		//not at object yet, but still tracking
		while (agent.transform.position.x != iObject.pointOfInterest.position.x && 
			agent.transform.position.z != iObject.pointOfInterest.position.z && trackingToObject == true) {
			print ("tracking to object");
			yield return null;
		}
		//at object and still tracking
		if (trackingToObject == true) {
            clickToMoveEnabled = false;
            iObject.typePrompt ();
            if (iObject.memoryScenes.Length > 0)
            {
                anim.SetBool("interacting", true);
            }
            else
            {
                StartCoroutine(Wait(0.25f, true));
            }
            print("type object");
			narratorBox.SetActive (true);
		}
	
		while (agent.transform.position.x == iObject.pointOfInterest.position.x &&
		       agent.transform.position.z == iObject.pointOfInterest.position.z && trackingToObject == true) {
			yield return null;
		}
		print ("cancelled interaction");
        anim.SetBool("interacting", false);
        if (iObject.anim)
        {
            iObject.anim.SetBool("Hover", false);
        }
        iObject.Answer (false);
		narratorBox.SetActive (false);
        StartCoroutine(Wait(0.25f, true));
    }


	//RANDOM STUFF

	void RandomSuggestion (){
        jimSentient = true;
        narratorBox.SetActive(false);
        jimIsBusy = true;
        //Jim should be able to randomly decide between doing an Idle, Interact with a random object, and move somewhere
        inactiveTimerLimit = Random.Range (inactiveTimerMinMax.x, inactiveTimerMinMax.y);
		int suggest = Random.Range(0,100);
		if (suggest <= 70) {
            //jimIdles.RandomIdle();
           // StartCoroutine(RandomIdle());
            StartCoroutine(RandomInteraction());
            StartCoroutine(Wait(0.1f, true));
        }
		if (suggest > 70) {
            //RandomInteraction();
            //StartCoroutine(RandomInteraction());
            StartCoroutine(RandomIdle());
            StartCoroutine(Wait(0.5f, true));
        }
		if (suggest > 500) {
            StartCoroutine(RandomMove());

            StartCoroutine(Wait(0.75f, true));
        }
	}

	IEnumerator RandomInteraction (){
		int objectNumber = Random.Range (0, interactableObjects.Length);
		interactableObject objectScript;
        print("Interact with " + interactableObjects[objectNumber].name);
		if (interactableObjects [objectNumber].GetComponent<interactableObject> () != null) {
			objectScript = interactableObjects [objectNumber].GetComponent<interactableObject> ();
			agent.SetDestination (objectScript.pointOfInterest.position);
			while (agent.transform.position.x != objectScript.pointOfInterest.position.x && agent.transform.position.z != objectScript.pointOfInterest.position.z) {
				//print ("stuck");
				yield return null;
			}
			//print ("find random item");
			narratorBox.SetActive (true);
			objectScript.sentientInteraction ();
		} else {
			print ("no interactableObject script found on: " + interactableObjects [objectNumber].name);
		}
		yield return new WaitForSeconds (4.5f);
		narratorBox.SetActive (false);
        jimIsBusy = false;
    }

    public IEnumerator RandomIdle()
    {
        int objectNumber = Random.Range(0, idlePointsOfInterest.Length);
        agent.SetDestination(idlePointsOfInterest[objectNumber].transform.position);
        Vector3 pointOfInterest = idlePointsOfInterest[objectNumber].transform.position;
        while (agent.transform.position.x != pointOfInterest.x && agent.transform.position.z != pointOfInterest.z) {
            //print ("stuck");
            yield return null;
        }

        int randomNo = (Random.Range(0, jimIdles.idles.Length)) +1;
        jimIdles.RandomIdle(randomNo);
        yield return new WaitForSeconds(jimIdles.idles[randomNo - 1].length + 0.25f);
        jimIsBusy = false;
    }

    public IEnumerator RandomMove(){
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;
		agent.SetDestination (finalPosition);
        //bool moving = true;
        yield return new WaitForSeconds(1.0f);
        jimIsBusy = false;
        //return null;
    }

	IEnumerator Wait(float waitTime, bool active){
        //print("waiting");
		yield return new WaitForSeconds (waitTime);
        clickToMoveEnabled = active;
    }
 
}