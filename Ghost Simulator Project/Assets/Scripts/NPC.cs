using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public new string name;
    public string description;
    public float health;
    public string[] attack;
    public float timeForRelief;   //Time it takes for the NPC to be relieved from the scare
    public int maxScareTimes;   //MaxTimes a NPC be scared?

    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + description + " |Health: " + health + " |ReleifTime: " + timeForRelief + " |Attack: ");
        foreach(string type in attack){
            Debug.Log(type + " ");
        }
    }
}
