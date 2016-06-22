using UnityEngine;
using System.Collections;

public class autoDestruct : MonoBehaviour
{
    public float timeToDestruct = 5.0f; //seconds to destruct

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Destruct());
    }
	
    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(timeToDestruct);
        Destroy(this.gameObject);
    }
}
