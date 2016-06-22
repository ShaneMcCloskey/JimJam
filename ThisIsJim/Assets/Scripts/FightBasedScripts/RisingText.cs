using UnityEngine;
using System.Collections;

public class RisingText : MonoBehaviour
{
    public float duration = 1.5f;
    public float speed = 1.5f;
    public float fadeSpeed = 0.5f;
    public Color[] colors = new Color[] { Color.red, Color.green, Color.blue};

    // private variables:
    Vector3 delta;
    public float alpha = 1.0f;
    float valueDisplay;
    Camera cam;
    TextMesh textMesh;
    //public Color color = Color.white;

    // SETUP - call this once after having created the object, to make it 
    // "points" shows the points.
    // "duration" is the lifespan of the object
    // "rise speed" is how fast it will rise over time.
    public void setup(int colorRedGreenBlue, string text)
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = colors[colorRedGreenBlue]; //0 = red, 1 = blue, 2 = green

        //valueDisplay =1.0f / duration;
       // delta = new Vector3(0f, 0f, speed);
    }

    // Use this for initialization
    void Start ()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        textMesh = GetComponent<TextMesh>();
        delta = new Vector3(0f, 0f, speed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(delta * Time.deltaTime, Space.World);
        alpha -= Time.deltaTime * fadeSpeed;
        //textMesh.color = new Color(color.r, color.g, color.b, alpha);

        // if completely faded out, die:
        if (alpha <= 0f)
        {
            Destroy(gameObject);
        }

        transform.LookAt(cam.transform.position);
        transform.rotation = cam.transform.rotation;
    }
}
