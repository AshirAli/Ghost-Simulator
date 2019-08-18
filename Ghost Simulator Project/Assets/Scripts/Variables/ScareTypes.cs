using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScareTypes", menuName = "Variables SO/ScareTypes SO")]
public class ScareTypes : ScriptableObject{
    public enum Type {Spiritual,Physical};
    [System.Serializable]
    public struct value {
        public Type m_ScareType;
        public string m_Name;
        public float m_Damage;
    }
    public value[] m_SpiritualScares;
    public value[] m_PhysicalScares;
}
