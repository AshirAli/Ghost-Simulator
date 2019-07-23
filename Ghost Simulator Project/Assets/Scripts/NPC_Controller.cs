using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Controller : MonoBehaviour
{
#region PUBLIC
    public Text m_DebugText;
    public Color scaredColor;
    public Color reliefColor;
    public GameObject ScreamText;
#endregion

#region PRIVATE
    AI_Movement aI_Movement;
    Renderer[] npcRenderer;
    int childRenderCount;
#endregion

    void Start()
    {
        aI_Movement = gameObject.GetComponent<AI_Movement>();
        npcRenderer = gameObject.GetComponentsInChildren<Renderer>();
        childRenderCount = npcRenderer.Length;
    }
    void Update()
    {
        
    }

#region PUBLIC_METHODS
    ///<summary>NPC is scared by ghost</summary>  
    public void HandleScare(){
        for (int i = 0; i < childRenderCount; i++){
            npcRenderer[i].material.SetColor("_Color",scaredColor);
        }
        m_DebugText.text = "NPC is scared";
        ScreamText.SetActive(true);
        Invoke("HideScreamText",1);
        aI_Movement.MoveToSafeZone();   //Move NPC to SafeZone
    }
    
    ///<summary>NPC is not afraid</summary>  
    public void HandleRelief(){
        for (int i = 0; i < childRenderCount; ++i){
            npcRenderer[i].material.SetColor("_Color",reliefColor);
        }
        m_DebugText.text = "NPC is relieved";
    }
#endregion

#region PRIVATE_METHODS
    void HideScreamText(){
        ScreamText.SetActive(false);
    }
#endregion
}
