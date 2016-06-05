using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic; //if using lists
using UnityEngine.UI;

public class savedInfo : MonoBehaviour {

    public int dayCount = 0;
    public int walletAmount = 100;
    public int memoriesExperienced = 0;
    public int memoriesToEndDay = 1; //NOT serialized
    public int lastMemoryExperienced = 0;


    public List<int> initialEquippedSkills = new List<int>(4);       //designer-set
    public List<bool> initialUnlockedSkills = new List<bool>();      //first 2 should always be unlocked, first doesn't matter

    public List<int> equippedSkills = new List<int>(4);       //equippedSkills
    //public List<Skill> skillsInGame = new List<Skill>();      //list of every skill   //no point in doing this, dev controlled, not player
    public List<bool> unlockedSkills = new List<bool>();      //skills, and if they're unlocked
    public bool[] memoryExperienced = new bool[14];

    // Use this for initialization
    void Awake () {
        savedInfo.SetEnvironmentVariables();
        Load();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
    {
        //SetEnvironmentVariables();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        serializedInfo data = new serializedInfo();
        //List of saved variables
        data.dayCount = dayCount;
        data.walletAmount = walletAmount;
        data.memoriesExperienced = memoriesExperienced;
        data.memoryExperienced = memoryExperienced;
        data.lastMemoryExperienced = lastMemoryExperienced;

        data.equippedSkills = equippedSkills;
        //data.skillsInGame = skillsInGame;
        data.unlockedSkills = unlockedSkills;

        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveProxy()
    {
        Save();
    }


    public void Load()
    {
        savedInfo.SetEnvironmentVariables();
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            serializedInfo data = (serializedInfo)bf.Deserialize(file);
            file.Close();
            //List of saved variables
            dayCount = data.dayCount;
            walletAmount = data.walletAmount;
            memoriesExperienced = data.memoriesExperienced;
            memoryExperienced = data.memoryExperienced;
            lastMemoryExperienced = data.lastMemoryExperienced;

            equippedSkills = data.equippedSkills;
            //skillsInGame = data.skillsInGame;
            unlockedSkills = data.unlockedSkills;

        }
    }

    public void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

            serializedInfo data = new serializedInfo();
            //List of saved variables
            data.dayCount = 0;
            data.walletAmount = 0;
            data.memoriesExperienced = 0;
            data.memoryExperienced = new bool[14]; //will set all to false
            data.lastMemoryExperienced = 1000;

            //data.equippedSkills = new List<int>() { 0, 0, 0, 0 };
            data.equippedSkills = initialEquippedSkills;
            data.unlockedSkills = initialUnlockedSkills;
                                        //the first 1 should always be true
            bf.Serialize(file, data);
            file.Close();
        }
    }


    private static void SetEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
    }

}

[Serializable] //serialized data below
class serializedInfo
{
    public int dayCount;
    public int walletAmount = 100;
    public int memoriesExperienced = 0;
    public bool[] memoryExperienced;
    public int lastMemoryExperienced = 0;

    public List<int> equippedSkills = new List<int>() { 0, 0, 0, 0 };       //equippedSkills
    public List<Skill> skillsInGame = new List<Skill>();      //list of every skill
    public List<bool> unlockedSkills = new List<bool>();      //skills, and if they're unlocked
}
