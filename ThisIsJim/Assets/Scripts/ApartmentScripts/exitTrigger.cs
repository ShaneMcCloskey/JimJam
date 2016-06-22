using UnityEngine;
using System.Collections;

public class exitTrigger : MonoBehaviour
{
	public Transform pointOfExit;
	//public string levelToLoad = "JimApartment_LR";
	// Use this for initialization
	public roomTrigger roomTrig;

	void OnTriggerEnter (Collider other)
    {	
		if(other.tag == "Player")
        {
            //print ("RAN");
            NavMeshAgent nav = other.GetComponent<NavMeshAgent>();
            jimControl2D jim = other.GetComponent<jimControl2D>();
            Vector3 ogDestination;
            ogDestination = nav.destination;
            
            //nav.Warp(pointOfExit.position);

            if (jim.jimSentient)
            {
                // nav.enabled = false;
                other.transform.position = pointOfExit.position;
                nav.enabled = true;
            }
            else
            {
                nav.enabled = false;
                other.transform.position = pointOfExit.position;
                nav.enabled = true;
                other.GetComponent<jimControl2D>().jimIsBusy = false;
            }
            //nav.destination = ogDestination; //causes Jim to try and go to a point between rooms repeatedly

            // StartCoroutine(SpeedUp(other));
            
            //other.GetComponent<jimControl2D>().clickToMoveEnabled = false; //for now, makes sure Jim doesn't get stuck when moving from one room
            //to the living room
            //problem is, Jim 's navmesh is getting confused when he enters a new room
            roomTrig.SendMessage ("enterNewRoom");
		}
	}

    IEnumerator SpeedUp(Collider other) //doesn't work with regular player action :C
    {
        float tempSpeed;
        tempSpeed = other.GetComponent<NavMeshAgent>().speed;
        other.GetComponent<NavMeshAgent>().speed = 15.0f;
        yield return new WaitForSeconds(0.15f);
        other.GetComponent<NavMeshAgent>().speed = tempSpeed;
    }
}
