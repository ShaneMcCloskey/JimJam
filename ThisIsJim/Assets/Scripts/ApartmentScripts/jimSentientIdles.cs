using UnityEngine;
using System.Collections;

public class jimSentientIdles : MonoBehaviour
{
	//notified from JimControl2D
	jimControl2D jimControl;

	public AnimationClip[] idles; 

	// Use this for initialization
	void Start ()
    {
		jimControl = transform.GetComponent<jimControl2D> ();
	}

	public void RandomIdle(int randomNo)
    {
		//play random idle animation
        //AnimationState idleAnimState = jimControl.anim.set
		//randomNo = Random.Range(1,3);
		jimControl.anim.SetInteger("idleIndex", randomNo);
		print ("anim: " + randomNo);
		StartCoroutine (Wait (idles[randomNo - 1].length));
		//jimControl.anim.SetInteger("idleIndex", 0);
	}

	IEnumerator Wait(float waitTime)
    {
        //tempWaitNumber = jimControl.anim.GetCurrentAnimatorClipInfo(0).Length;
        
        //print("current clip length: " + jimControl.anim.GetCurrentAnimatorClipInfo(0).Length);
        //yield return new WaitForSeconds(tempWaitNumber);

        yield return new WaitForSeconds(waitTime - 0.5f);
        jimControl.anim.SetInteger("idleIndex", 0); //resets animations, 0 is a null number
        jimControl.jimIsBusy = false;
    }
}
