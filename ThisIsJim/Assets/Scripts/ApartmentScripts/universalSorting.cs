using UnityEngine;
using System.Collections;

public class universalSorting : MonoBehaviour
{
	private float xPosition; //move down as Z moves up
	private float zPosition;

	public float yOffset = 0.0f;
	public float zOffset = 0.0f;
    float yValue;
    float zValue;

	public Transform target; //if there is a target, the x and zPositions will be based on it
	
	// Update is called once per frame
	void Update ()
    {
		if (target == null)
        {
			xPosition = transform.position.x;
			zPosition = transform.position.z;
            yValue = (-zPosition / 2) + yOffset;
            zValue = (zPosition + zOffset);

            transform.position = new Vector3 (xPosition, yValue, zValue);
		}
        else
        {
			xPosition = target.position.x;
			zPosition = target.position.z;
			transform.position = new Vector3 (xPosition, -zPosition/2 + yOffset, zPosition + zOffset);
		}
	}
}
