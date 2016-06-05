using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class eventTrigger : MonoBehaviour {

    //right now this is going to end the demo

    public bool dayOver = false;
    public interactableObject bed;
    public jimControl2D jimCtrl;
    public Image uiBlackFade;
    public AudioSource musicSource;
    public audioManager audManager;
    public string sceneToLoad = "EndOfDay";
   // public AudioMixerGroup;
    public string eodMessage = "Jim needs to zonk out";

    bool ending = false;

    public savedInfo savedGame;


    // Use this for initialization
    void Start () {
        uiBlackFade.enabled = false;
        savedGame = GameObject.FindWithTag("SerializedMain").GetComponent<savedInfo>();
        if (savedGame.memoriesExperienced >= savedGame.memoriesToEndDay)    //checks if enough memories are ready in the start so that it doesn't get conflicted with later additive levels
        {
            dayOver = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (dayOver && !ending) {
           StartCoroutine(EndDemo());
        }

	}

    public IEnumerator EndDemo()
    {
        ending = true;
        jimCtrl.jimIsBusy = true;
        jimCtrl.jimSentient = true;
        jimCtrl.clickToMoveEnabled = false;
        yield return new WaitForSeconds(4.5f);
        
        

        jimCtrl.agent.destination = bed.pointOfInterest.position;                               //move Jim to bed

        textTyper.TypeText(bed.narratorBox, eodMessage, (bed.typeSpeed * 2), bed.typeSound);      //Type message
        yield return new WaitForSeconds(4.5f);
        bed.camScript.FramePlayerAndObject(bed.gameObject);                                     //Frame player and bed
        bed.narratorBox.SetActive(false);
        uiBlackFade.enabled = true;

        //print("please fade down");
        audManager.fadeDown = true;
        StartCoroutine(audManager.FadeDown());

        StartCoroutine(LoadEndScreen());
        yield return new WaitForSeconds(1.1f);

        float alpha = uiBlackFade.color.a;
        for (float t = 0.0f; t < 255.0f; t += Time.deltaTime / 4.0f)
        {
            uiBlackFade.color = new Vector4(0f, 0f, 0f, Mathf.Lerp(alpha, 1, t));
            yield return null;
        }
    }

    public IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(5.1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
