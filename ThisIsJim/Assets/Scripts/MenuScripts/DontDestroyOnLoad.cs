using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour
{
	public void Awake()
	{
		DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
}
