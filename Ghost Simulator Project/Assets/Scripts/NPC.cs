using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC", order = 52)]
public class NPC : ScriptableObject,ISerializationCallbackReceiver
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private float maxHealth;
    [System.NonSerialized]
    private float currentHealth;
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
    public float CurrentHealth{
        get{
            return currentHealth;
        }
        set{
            currentHealth = value;
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
    public void OnAfterDeserialize()    //Implements necessary functions for ISerializationCallbackReceiver interface
    {
        currentHealth = maxHealth;
    }

    public void OnBeforeSerialize() //Implements necessary functions for ISerializationCallbackReceiver interface
    {
        
    }
}
