using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fadeUiOnLoad : MonoBehaviour
{
    public float timeFromStart = 1.5f;
    public float fadeTime = 0.2f;
    public float alphaTarget = 0.75f;
    public bool rawImage = false;
    float timer = 0.0f;
    Image image;
    RawImage rImage;
    bool startFade = false;

    // Use this for initialization
    void Start()
    {
        if (rawImage == false)
        {
            image = gameObject.GetComponent<Image>();
        }
        else
        {
            rImage = gameObject.GetComponent<RawImage>();
        }
        StartCoroutine(WaitToStart());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            timer += 1 * Time.deltaTime;
        }
        Color tempColor = Color.black;
        if (rawImage == false && timer < 1.0f)
        {
            tempColor.a = Mathf.Lerp(0.0f, alphaTarget, timer);
            image.color = tempColor;
        }
        if (rawImage == true && timer < 1.0f)
        {
            tempColor.a = Mathf.Lerp(0.0f, alphaTarget, timer);
            rImage.material.color = tempColor;
        }
    }
    
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(timeFromStart);
        startFade = true;
    }
}
