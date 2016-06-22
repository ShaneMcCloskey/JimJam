using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class characterStatsDisplay : MonoBehaviour
{
    public characterStats charStats;
    public Text healthPoints;
    public Animator healthState;

    public Text recollectionPoints;
    //public Animator recollectionAC;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if (healthPoints != null)
        {
            healthPoints.text = Mathf.Round(charStats.healthPoints) + " HP";
        }
        if (healthState != null)
        {
            healthState.SetFloat("health", charStats.healthPoints);
        }

        recollectionPoints.text = charStats.recollectionPoints + " RP";
    }
}
