using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvasUpDownButton : MonoBehaviour
{
    //Script is meant to be an alternate way of scrooling through a masked UI window
    //Attached to content and referenced by UI.Button to run increment function

    public RectTransform content;
    public Vector2 minMaxPosition;
    //public float increment = 10.0f;
    bool moving = false;

    public void Increment(float incrementValue)
    {
        if (moving == false)
        {
            print("ran");
            moving = true;

            StartCoroutine(Wait());
            //print("ran");
            if (incrementValue > 0 && content.anchoredPosition.y < minMaxPosition.y) //&& content.position.y <= minMaxPosition.y
            {
                //content.position = new Vector3(0, content.position.y + incrementValue, 0);
                content.position = new Vector2(content.position.x, (content.position.y + incrementValue));
                print("moveUP " + content.anchoredPosition.y);
            }

            if (incrementValue < 0 && content.anchoredPosition.y > minMaxPosition.x) // && content.position.y >= minMaxPosition.x
            {
                //content.position = new Vector3(0, content.position.y + incrementValue, 0);
                content.position = new Vector2(content.position.x, (content.position.y + incrementValue));
                print("moveDOWN " + content.anchoredPosition.y);
            }
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.15f);
        moving = false;

    }
}
