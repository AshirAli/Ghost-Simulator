using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC", order = 52)]
public class NPC : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private string[] attack;
    [SerializeField]
    private float timeForRelief;   //Time it takes for the NPC to be relieved from the scare
    [SerializeField]
    private int maxScareTimes;   //MaxTimes a NPC be scared?

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
    public string[] Attack{
        get{
            return attack;
        }
    }
    public float TimeForRelief{
        get {
            return timeForRelief;
        }
    }
    public int MaxScareTimes{
        get{
            return maxScareTimes;
        }
    }
    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + description + " |Health: " + maxHealth + " |ReleifTime: " + timeForRelief + " |Attack: ");
        foreach(string type in attack){
            Debug.Log(type + " ");
        }
    }
}
