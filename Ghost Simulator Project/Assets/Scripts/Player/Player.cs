using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player",order = 51)]
public class Player : ScriptableObject,ISerializationCallbackReceiver
{
    private new StringVariable name;
    private StringVariable description;
    private FloatVariable maxHealth;
    private FloatVariable currentHealth;
    private FloatVariable phaseDelay;    //Delay between phasing of ghost
    private string[] attack;
    public StringVariable Name{
        get{
            return name;
        }
    }
    public StringVariable Description{
        get{
            return description;
        }
    }
    public FloatVariable MaxHealth{
        get{
            return maxHealth;
        }
    }
    public FloatVariable CurrentHealth{
        get{
            return currentHealth;
        }
        set{
            currentHealth = value;
        }
    }
    public FloatVariable PhaseDelay{
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
        //currentHealth = maxHealth;
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
