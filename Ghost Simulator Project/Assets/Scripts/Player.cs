using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public new string name;
    public string description;
    public float health;
    public float phaseDelay;    //Delay between phasing of ghost
    public string[] attack;
    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + description + " |Health: " + health + " |Attack: ");
        foreach(string type in attack){
            Debug.Log(type + " ");
        }
    }
}
