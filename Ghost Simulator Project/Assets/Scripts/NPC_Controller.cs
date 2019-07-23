using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Controller : MonoBehaviour
{
#region PUBLIC
    public GameObject m_Player;
    public Text m_DebugText;
    public Image FearTimer;
    public Image FearBar;
    public Color scaredColor;
    public Color reliefColor;
    public GameObject ScreamText;
    public float m_TimeForRelief;   //Time it takes for the NPC to be relieved from the scare
    public bool isScared;   //NPC scared state
    public int maxScareTimes;   //MaxTimes a NPC be scared?
#endregion

#region PRIVATE
    AI_Movement aI_Movement;
    Renderer[] npcRenderer;
    int childRenderCount;
    float timePassedAfterScare;
    int timeScared;
#endregion

    void Start()
    {
        isScared = false;
        aI_Movement = gameObject.GetComponent<AI_Movement>();
        npcRenderer = gameObject.GetComponentsInChildren<Renderer>();
        childRenderCount = npcRenderer.Length;
    }
    void Update()
    {
        if(isScared){
            if(aI_Movement.hasReachedDestination){
                timePassedAfterScare += Time.deltaTime;
                HandleFearTimer();
                if(timePassedAfterScare >= m_TimeForRelief){
                    HandleRelief();
                }
            }
        }
        if(timeScared > maxScareTimes){
            //Handle next stage of NPC
        }
    }

#region PUBLIC_METHODS
    ///<summary>NPC is scared by ghost</summary>  
    public void HandleScare(){
        //Debug.Log("Times scared " + timeScared);
        timeScared += 1;
        HandleFearBar();
        PlayerController.NpcScared(true);
        for (int i = 0; i < childRenderCount; i++){
            npcRenderer[i].material.SetColor("_Color",scaredColor);
        }
        m_DebugText.text = "NPC is scared";
        ScreamText.SetActive(true);
        Invoke("HideScreamText",1);
        isScared = true;
        aI_Movement.MoveToSafeZone();   //Move NPC to SafeZone
    }
    
    ///<summary>NPC is not afraid</summary>  
    public void HandleRelief(){
        timePassedAfterScare = 0f;
        isScared = false;
        PlayerController.NpcScared(false);
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
    void HandleFearTimer(){
        var fillPercent = timePassedAfterScare / m_TimeForRelief;
        FearTimer.fillAmount = Mathf.Lerp(0,1,fillPercent);
    }
    void HandleFearBar(){
        float fillPercent =timeScared / (float) maxScareTimes;
        FearBar.fillAmount = fillPercent;
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerController.NpcInRange(true);
            if(Physics.Linecast(transform.position,m_Player.transform.position)){
                PlayerController.NpcDirectContact(true);
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform.tag == "Player")
        {
            PlayerController.NpcInRange(false);
        }
    }
#endregion
}
