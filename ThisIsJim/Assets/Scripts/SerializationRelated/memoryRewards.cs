using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable] //serialized data below
public class memoryRewards : MonoBehaviour{

    //put on object in memory scene that is additively loaded , change unlocked skills, and save it
    public int skillToUnlock = 2;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
