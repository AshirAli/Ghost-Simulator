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
    private Player currentPlayer;
    [SerializeField]
    private GameObject m_NpcTarget;
    [SerializeField]
    private Color visibleColor = Color.white;
    [SerializeField]
    private Color invisibleColor = Color.blue;
    private float colorChangeInterval = 1f; //Duration of color change during phasing
    private float timePassed = 0f;  //Time passed since last phasing
    private GameObject ghostBody;
    private Renderer ghostBodyRenderer;
    private Color ghostColor;
    private static bool npcScared;
    private NPC_Controller npcController;
    private static bool m_IsNpcInRange; 
    private static bool m_NpcDirectContact;
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
        else{
            m_NpcDirectContact = false;
        }
    }

#region PUBLIC_METHODS

    ///<summary>Set NPC state (scared/not scared)</summary>  
    public static void NpcScared(bool scare){
        npcScared = scare;
    }
    public static void NpcInRange(bool state){
        m_IsNpcInRange = state;
    }
    public static void NpcDirectContact(bool state){
        m_NpcDirectContact = state;
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
        if(m_NpcDirectContact){
            DirectContact();
        }
        //if(Physics.Linecast(transform.position,m_NpcTarget.transform.position)){
            //Debug.DrawLine(transform.position,m_NpcTarget.transform.position);
            //Debug.Log("Directly Contacted");
        //}
    }

    ///<summary>Direct contact of Ghost and NPC</summary>  
    void DirectContact(){
        npcController.HandleScare();
    }

    ///<summary>HandleInput for ghost</summary>  
    void HandleInput(){
        timePassed += Time.deltaTime;
        if(timePassed >= currentPlayer.phaseDelay){
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

#endregion
}
