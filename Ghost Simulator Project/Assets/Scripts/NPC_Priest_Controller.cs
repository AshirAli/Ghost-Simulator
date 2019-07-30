using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Priest_Controller : MonoBehaviour
{
#region PUBLIC
    public GameObject m_Player;
    public GameObject m_PrayerField;
    // public Text m_DebugText;
    // public Image FearTimer;
    // public Image FearBar;
    // public Color scaredColor;
    // public Color reliefColor;
    // public GameObject ScreamText;
    public bool isScared;   //NPC scared state
    // public static float currentNpcReliefTime;
    public GameObject priestBody;
#endregion

#region PRIVATE
    [SerializeField]
    NPC currentNpc;
    private Animator priestBodyAnimator;
    private AI_Movement aI_Movement;
    private Renderer[] npcRenderer;
    private int childRenderCount;
    private float timePassedAfterScare;
    private int timeScared;
    private MaterialPropertyBlock materialPropertyBlock;
#endregion

    void Start()
    {
        isScared = false;
        //currentNpcReliefTime = currentNpc.TimeForRelief;
        aI_Movement = gameObject.GetComponent<AI_Movement>();
        npcRenderer = gameObject.GetComponentsInChildren<Renderer>();
        priestBodyAnimator = priestBody.GetComponent<Animator>();
        childRenderCount = npcRenderer.Length;
        materialPropertyBlock = new MaterialPropertyBlock();
        m_PrayerField.SetActive(false);
    }
    void Update()
    {
        if(isScared){
            if(aI_Movement.hasReachedDestination){
                m_PrayerField.SetActive(true);
                timePassedAfterScare += Time.deltaTime;
                //Debug.Log("Time Passed After Scare : " + timePassedAfterScare);
                //HandleFearTimer();
                if(timePassedAfterScare >= currentNpc.TimeForRelief){
                    //HandleRelief();
                }
            }
        }
        if(timeScared > currentNpc.MaxScareTimes){
            //Handle next stage of NPC
        }
    }

#region PUBLIC_METHODS
    ///<summary>NPC Priest is attacking ghost</summary>  
    public void AttackPlayer(){
        priestBodyAnimator.SetBool("attack",true);
        m_PrayerField.SetActive(true);
    }

    ///<summary>NPC is scared by ghost</summary>  
    // public void HandleScare(){
    //     //Debug.Log("Times scared " + timeScared);
    //     timeScared += 1;
    //     HandleFearBar();
    //     PlayerController.NpcScared(true);
    //     for (int i = 0; i < childRenderCount; i++){
    //         materialPropertyBlock.SetColor("_BaseColor", scaredColor);
    //         npcRenderer[i].SetPropertyBlock(materialPropertyBlock);
    //     }
    //     m_DebugText.text = "NPC is scared";
    //     ScreamText.SetActive(true);
    //     Invoke("HideScreamText",1);
    //     isScared = true;
    //     aI_Movement.MoveToSafeZone();   //Move NPC to SafeZone
    // }
    
    ///<summary>NPC is not afraid</summary>  
    // public void HandleRelief(){
    //     m_PrayerField.SetActive(false);
    //     timePassedAfterScare = 0f;
    //     isScared = false;
    //     PlayerController.NpcScared(false);
    //     for (int i = 0; i < childRenderCount; ++i){
    //         materialPropertyBlock.SetColor("_BaseColor", reliefColor);
    //         npcRenderer[i].SetPropertyBlock(materialPropertyBlock);
    //     }
    //     m_DebugText.text = "NPC is relieved";
    // }

    ///<summary>Relief time at safe zone is increased by Delay at waypoint</summary> 
    // public void AddToReliefTime(float duration){
    //     currentNpcReliefTime += duration;
    // }  
#endregion

#region PRIVATE_METHODS
    // void HideScreamText(){
    //     ScreamText.SetActive(false);
    // }
    // void HandleFearTimer(){
    //     var fillPercent = timePassedAfterScare / currentNpc.TimeForRelief;
    //     FearTimer.fillAmount = Mathf.Lerp(0,1,fillPercent);
    // }
    // void HandleFearBar(){
    //     float fillPercent = timeScared / (float) currentNpc.MaxScareTimes;
    //     FearBar.fillAmount = fillPercent;
    // }
    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerController.NpcInRange(true);
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
