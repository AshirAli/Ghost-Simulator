using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Controller : MonoBehaviour
{
#region PUBLIC
    public GameObject m_Player;
    public GameObject m_PrayerField;
    public Text m_DebugText;
    public Image FearTimer;
    public Image FearBar;
    public Color scaredColor;
    public Color reliefColor;
    public GameObject ScreamText;
    public bool isScared;   //NPC scared state
    public static float currentNpcReliefTime;
#endregion

#region PRIVATE
    [SerializeField]
    NPC currentNpc;
    private AI_Movement aI_Movement;
    private Renderer[] npcRenderer;
    private int childRenderCount;
    private float timePassedAfterScare;
    private int timeScared;
#endregion

    void Start()
    {
        isScared = false;
        currentNpcReliefTime = currentNpc.TimeForRelief;
        aI_Movement = gameObject.GetComponent<AI_Movement>();
        npcRenderer = gameObject.GetComponentsInChildren<Renderer>();
        childRenderCount = npcRenderer.Length;
    }
    void Update()
    {
        if(isScared){
            if(aI_Movement.hasReachedDestination){
                m_PrayerField.SetActive(true);
                timePassedAfterScare += Time.deltaTime;
                HandleFearTimer();
                if(timePassedAfterScare >= currentNpc.TimeForRelief){
                    HandleRelief();
                }
            }
        }
        if(timeScared > currentNpc.MaxScareTimes){
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
        m_PrayerField.SetActive(false);
        timePassedAfterScare = 0f;
        isScared = false;
        PlayerController.NpcScared(false);
        for (int i = 0; i < childRenderCount; ++i){
            npcRenderer[i].material.SetColor("_Color",reliefColor);
        }
        m_DebugText.text = "NPC is relieved";
    }

    ///<summary>Relief time at safe zone is increased by Delay at waypoint</summary> 
    public void AddToReliefTime(float duration){
        currentNpcReliefTime += duration;
    }  
#endregion

#region PRIVATE_METHODS
    void HideScreamText(){
        ScreamText.SetActive(false);
    }
    void HandleFearTimer(){
        var fillPercent = timePassedAfterScare / currentNpc.TimeForRelief;
        FearTimer.fillAmount = Mathf.Lerp(0,1,fillPercent);
    }
    void HandleFearBar(){
        float fillPercent =timeScared / (float) currentNpc.MaxScareTimes;
        FearBar.fillAmount = fillPercent;
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerController.NpcInRange(true);
            // if(Physics.Linecast(transform.position,m_Player.transform.position)){
            //     PlayerController.NpcDirectContact(true);
            // Vector3 direction = m_Player.transform.position - transform.position + Vector3.up; //Vector3.up is a shortcut for (0, 1, 0)
            // Ray ray = new Ray (transform.position, direction);
            // RaycastHit raycastHit;
            // Debug.DrawLine(transform.position,direction,Color.red);
            // if(Physics.Raycast(ray, out raycastHit))
            // {
            //     Debug.Log(raycastHit.collider.name);
            //     if(raycastHit.collider.tag == "Player")
            //     {
            //         PlayerController.NpcDirectContact(true);
            //     }
            // }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform.tag == "Player")
        {
            PlayerController.NpcInRange(false);
            PlayerController.NpcDirectContact(false);
        }
    }
#endregion
}
