using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player",order = 51)]
public class Player : ScriptableObject //,ISerializationCallbackReceiver
{
    [Header("Name_01")]
    [SerializeField]
    private new StringVariable name;

    [Header("Description_02")]
    [SerializeField]
    private StringVariable devDescription;

    [SerializeField]
    private StringVariable userDescription;

    [SerializeField]
    [Header("Health_03")]
    private FloatVariable maxHealth;

    [SerializeField]
    [Header("Invisible_04")]
    private BoolVariable canTurnInvisible;

    [SerializeField]
    [Header("PhaseTypes_05")]
    private StringArray phaseTypes;
    
    [SerializeField]
    [Header("SpiritualScares_06")]
    private StringArray spiritualScares;
    
    [SerializeField]
    private Type GhostType;

    [SerializeField]
    private string[] attack;
    [SerializeField]
    public enum Type {Spiritual,SemiSpiritual,Organic};
    //Set Get Methods
    public StringVariable Name{
        get{
            return name;
        }
    }
    public StringVariable DevDescription{
        get{
            return devDescription;
        }
    }
    public StringVariable UserDescription{
        get{
            return userDescription;
        }
    }
    public FloatVariable MaxHealth{
        get{
            return maxHealth;
        }
    }
    public StringArray PhaseTypes{
        get{
            return phaseTypes;
        }
    }
    public StringArray SpiritualScares{
        get{
            return spiritualScares;
        }
    }

    public string[] Attack{
        get{
            return attack;
        }
    }

    public Type CurrentGhostType{
        get{ return GhostType; }
        set{ GhostType = value; }
    }

    // public void OnAfterDeserialize()    //Implements necessary functions for ISerializationCallbackReceiver interface
    // {
    //     //currentHealth = maxHealth;
    // }

    // public void OnBeforeSerialize() //Implements necessary functions for ISerializationCallbackReceiver interface
    // {
        
    // }

    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + devDescription + " |Health: " + maxHealth + " |Attack: ");
        foreach(string type in attack){
            Debug.Log(type + " ");
        }
    }
}
