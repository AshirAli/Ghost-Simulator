using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player",order = 51)]
public class Player : ScriptableObject,ISerializationCallbackReceiver
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
    public float CurrentHealth{
        get{
            return currentHealth;
        }
        set{
            currentHealth = value;
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

    public void OnAfterDeserialize()    //Implements necessary functions for ISerializationCallbackReceiver interface
    {
        currentHealth = maxHealth;
    }

    public void OnBeforeSerialize() //Implements necessary functions for ISerializationCallbackReceiver interface
    {
        
    }

    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + description + " |Health: " + maxHealth + " |Attack: ");
        foreach(string type in attack){
            Debug.Log(type + " ");
        }
    }
}
