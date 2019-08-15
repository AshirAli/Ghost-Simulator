using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  public class PlayerSetup 
public class PlayerController : MonoBehaviour
{
#region PUBLIC
    public Text m_DebugText;
    public bool isVisible = false;  //Is the ghost visible to NPC's
#endregion

#region PRIVATE
    [SerializeField]
    private FloatVariable m_CurrentHealth;
    [SerializeField]
    private FloatVariable m_PhaseDelay;
    [SerializeField]
    private GameObject m_NpcTarget;
    [SerializeField]
    private GameObject m_PlayerDamageImage;
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
    private GameManager gameManager;
    private int layerMask;  //Layer ignored by raycast- Layer 2
#endregion
    void Start()
    {
        layerMask = 1 << 2;     // This would cast rays only against colliders in layer 2.
        layerMask = ~layerMask;    // But instead we want to collide against everything except layer 2. The ~ operator does this, it inverts a bitmask.
        GhostInitialize();
        if(m_NpcTarget != null){
            npcController = m_NpcTarget.GetComponent<NPC_Controller>();
        }
        GameObject manager = GameObject.FindGameObjectWithTag("GameController");
        gameManager = manager.GetComponent<GameManager>();
    }
    void Update()
    {
        HandleInput();
        if(m_IsNpcInRange && isVisible && !npcScared)
        {
            HandleContact();
        }
        Vector3 direction = m_NpcTarget.transform.localPosition - transform.localPosition + Vector3.up;
        Debug.DrawRay(transform.position,direction,Color.red);
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
    public void TakeDamage(float damage){
        m_PlayerDamageImage.SetActive(true);
        Invoke("HidePlayerDamageUI",0.5f);
        m_CurrentHealth.value -= damage;
        Debug.Log("Current Player Health " + m_CurrentHealth);
        if(m_CurrentHealth.value <= 0 ){
            HandlePlayerDeath();
        }
        //currentPlayer.health -= damage;
        //currentPlayer.Print();
    }
#endregion

#region PRIVATE_METHODS

    ///<summary>Initialize properties of Ghost</summary>  
    private void GhostInitialize(){
        m_DebugText.text = "Invisible at start";
        ghostBody = transform.Find("Body").gameObject; 
        ghostBodyRenderer = ghostBody.GetComponent<Renderer>();
        GhostPhase(isVisible);  //Invisible at start
        m_PlayerDamageImage.SetActive(false);
    }

    ///<summary>Handle contact with Ghost and Target-NPC</summary>  
    private void HandleContact(){
        // if(m_NpcDirectContact){
        //     DirectContact();
        // }
            Vector3 direction = m_NpcTarget.transform.localPosition - transform.localPosition + Vector3.up; //Vector3.up is a shortcut for (0, 1, 0)
            Ray ray = new Ray (transform.position, direction);
            RaycastHit raycastHit;
            
            if(Physics.Raycast(ray, out raycastHit,10f,layerMask,QueryTriggerInteraction.Collide))
            {
                Debug.Log(raycastHit.collider.name);
                //Debug.DrawRay(transform.position,direction,Color.red,10f);
                if(raycastHit.collider.transform == m_NpcTarget.transform)
                {
                    //PlayerController.NpcDirectContact(true);
                    DirectContact();
                    Debug.Log("Direct contact");
                }
                else{
                    m_IsNpcInRange = false;
                    Debug.Log("No direct contact");
                }
            }
    }

    ///<summary>Direct contact of Ghost and NPC</summary>  
    private void DirectContact(){
        npcController.HandleScare();
    }

    ///<summary>HandleInput for ghost</summary>  
    private void HandleInput(){
        timePassed += Time.deltaTime;
        if(timePassed >= m_PhaseDelay.value){
            if(Input.GetKey(KeyCode.Space)){
                if(isVisible){
                    GhostPhase(false);
                    gameManager.HandlePostFx(false);
                }
                else{
                    GhostPhase(true);
                    gameManager.HandlePostFx(true);
                }
                timePassed = 0f;
            }
        }
    }

    ///<summary>Make Player-ghost visible or invisible (Bool visible?)</summary>  
    private void GhostPhase(bool visible){
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

    private void HandlePlayerDeath(){
        Debug.Log("Player Died");
        //Game Ending
    }
    private void HidePlayerDamageUI(){
        m_PlayerDamageImage.SetActive(false);
    }
#endregion
}
