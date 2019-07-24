using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Loader : MonoBehaviour
{
    public GameObject m_GameManager;
    void Awake()
    {
        if(GameManager.instance == null){
            Instantiate(m_GameManager);
        }
    }
}
