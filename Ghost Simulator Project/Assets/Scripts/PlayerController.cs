using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
#region PUBLIC   
    public Text m_DebugText;
    public bool isVisible = false;  //Is the ghost visible to NPC's
#endregion

#region PRIVATE
    [SerializeField]
    float m_GhostPhaseDelay = 1f; //Delay between phasing of ghost
    [SerializeField]
    GameObject m_NpcTarget;
    [SerializeField]
    Color visibleColor = Color.white;
    [SerializeField]
    Color invisibleColor = Color.blue;
    float colorChangeInterval = 1f; //Duration of color change during phasing
    float timePassed = 0f;  //Time passed since last phasing
    GameObject ghostBody;
    Renderer ghostBodyRenderer;
    Color ghostColor;
    bool m_IsNpcInRange; 
    bool npcScared;
    NPC_Controller npcController;
#endregion
    void Start()
    {
        GhostInitialize();
        npcController = m_NpcTarget.GetComponent<NPC_Controller>();
    }
    void Update()
    {
        HandleInput();
        if(m_IsNpcInRange && isVisible && !npcScared)
        {
            HandleContact();
        }
    }

#region PUBLIC_METHODS
    public void NpcScared(){
        npcScared = true;
    }
#endregion

#region PRIVATE_METHODS

    ///<summary>Initialize properties of Ghost</summary>  
    void GhostInitialize(){
        m_DebugText.text = "Invisible at start";
        ghostBody = transform.Find("Body").gameObject; 
        ghostBodyRenderer = ghostBody.GetComponent<Renderer>();
        GhostPhase(isVisible);  //Invisible at start
    }

    ///<summary>Handle contact with Ghost and Target-NPC</summary>  
    void HandleContact(){
        if(Physics.Linecast(transform.position,m_NpcTarget.transform.position)){
             DirectContact();
             Debug.Log("Directly Contacted");
        }
    }

    ///<summary>Direct contact of Ghost and NPC</summary>  
    void DirectContact(){
        npcController.HandleScare();
    }

    ///<summary>HandleInput for ghost</summary>  
    void HandleInput(){
        timePassed += Time.deltaTime;
        if(timePassed >= m_GhostPhaseDelay){
            if(Input.GetKey(KeyCode.Space)){
                Debug.Log("Ghost phased");
                if(isVisible){
                    GhostPhase(false);
                }
                else{
                    GhostPhase(true);
                }
                timePassed = 0f;
            }
        }
    }

    ///<summary>Make Player-ghost visible or invisible (Bool visible?)</summary>  
    void GhostPhase(bool visible){
        isVisible = visible;
        if(visible){    //Ghost now visible
            ghostColor = visibleColor;
            m_DebugText.text = "Ghost now visible";
            //ghostBodyRenderer.material.SetColor("_Color", Color.Lerp(ghostBodyRenderer.material.color, ghostColor, Time.deltaTime * colorChangeInterval));
            ghostBodyRenderer.material.SetColor("_Color",ghostColor);
        }
        else{   //Ghost now invisible
            m_DebugText.text = "Ghost now invisible";
            ghostColor = invisibleColor;
            ghostBodyRenderer.material.SetColor("_Color",ghostColor);
           //ghostBodyRenderer.material.SetColor("_Color", Color.Lerp(ghostBodyRenderer.material.color, ghostColor, Time.deltaTime * colorChangeInterval));
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == m_NpcTarget.transform)
        {
            m_IsNpcInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform == m_NpcTarget.transform)
        {
            m_IsNpcInRange = false;
        }
    }
#endregion
}
