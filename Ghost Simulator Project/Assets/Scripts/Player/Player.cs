using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player",order = 51)]
public class Player : ScriptableObject //,ISerializationCallbackReceiver
{
    public enum Type {Spiritual,SemiSpiritual,Organic};
    [SerializeField]
    private Type GhostType;

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
    private FloatVariable lowHealth;

    [SerializeField]
    [Header("Invisible_04")]
    private BoolVariable canTurnInvisible;

    [SerializeField]
    [Header("PhaseTypes_05")]
    private StringArray phaseTypes;
    
    [SerializeField]
    [Header("ScareTypes_06")]
    private ScareTypes scareTypes;

    [SerializeField]
    [Header("ScareDurations_07")]
    private FloatVariable[] scareDurations;

    [SerializeField]
    [Header("ScareLevels_08")]
    private FloatVariable[] scareLevels;

    [SerializeField]
    [Header("GhostLevels_09")]
    private FloatVariable[] ghostLevels;
    
        //Set Get Methods
    public Type CurrentGhostType{
        get{ return GhostType; }
        set{ GhostType = value; }
    }
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
    public ScareTypes SpiritualScares{
        get{
            return scareTypes;
        }
    }
    public FloatVariable[] ScareDurations{
        get{
            return scareDurations;
        }
    }
    public FloatVariable[] ScareLevels{
        get{
            return scareLevels;
        }
    }

    public FloatVariable[] GhostLevel{
        get{
            return ghostLevels;
        }
    }
    // public void OnAfterDeserialize()    //Implements necessary functions for ISerializationCallbackReceiver interface
    // {
    //     //currentHealth = maxHealth;
    // }

    // public void OnBeforeSerialize() //Implements necessary functions for ISerializationCallbackReceiver interface
    // {
        
    // }

    public void Print(){
        Debug.Log("Npc || Name : " + name + " |Description: " + devDescription + " |Health: " + maxHealth);
    }
}
