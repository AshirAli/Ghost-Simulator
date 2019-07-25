using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player",order = 51)]
public class Player : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float phaseDelay;    //Delay between phasing of ghost
    [SerializeField]
    private string[] attack;
    public string Name{
        get{
            return name;
        }
    }
    public string Description{
        get{
            return description;
        }
    }
    public float MaxHealth{
        get{
            return maxHealth;
        }
    }
    public float PhaseDelay{
        get{
            return phaseDelay;
        }
    }
    public string[] Attack{
        get{
            return attack;
        }
    }
    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + description + " |Health: " + maxHealth + " |Attack: ");
        foreach(string type in attack){
            Debug.Log(type + " ");
        }
    }
}
