using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class memorySceneManager : MonoBehaviour {

	public float totalTime = 5.0f; //time until loading to a fight, or back to the apartment
	public MovieTexture movTexture;
    public string sceneToLoad = "JimApartment_Main";
    public Texture cloudShape;

    RawImage rawImage;

    // Use this for initialization
    void Start () {
		StartCoroutine (EndScene ());
       // StartCoroutine(AnimateCloud());
        //transform.GetComponent<Renderer>().material.mainTexture = movTexture;
       rawImage = transform.GetComponent<RawImage>();
       // movTexture = rawImage.material.mainTexture;
		movTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () {
        //rawImage.material.SetTexture("_AlphaCutout", cloudShape);
	}



    IEnumerator EndScene(){
		yield return new WaitForSeconds (totalTime);
		//load to fight or apartment
		SceneManager.LoadScene(sceneToLoad);
	}
}
