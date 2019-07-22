using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
#region PUBLIC
    public Text m_DebugText;
    public float m_GhostPhaseDelay = 1f; //Delay between phasing of ghost
    public bool isVisible = false;
#endregion

#region PRIVATE
    float colorChangeInterval = 1f;
    float timePassed = 0f; 
    GameObject ghostBody;
    Renderer ghostBodyRenderer;
    Color ghostColor;
#endregion
    void Start()
    {
        m_DebugText.text = "Invisible at start";
        ghostBody = transform.Find("Body").gameObject; 
        ghostBodyRenderer = ghostBody.GetComponent<Renderer>();
        GhostPhase(isVisible);
    }
    void Update()
    {
        HandleInput();
    }
    void HandleInput(){
        timePassed += Time.deltaTime;
        if(timePassed >= m_GhostPhaseDelay){
            if(Input.GetKey(KeyCode.Space)){
                Debug.Log("Ghost phased");
                GhostPhase(isVisible);
                timePassed = 0f;
            }
        }
    }
    ///<summary>Make Player-ghost visible or invisible (Bool visible?)</summary>  
    void GhostPhase(bool visible){
        isVisible = !visible;
        if(visible){
            ghostColor = Color.white;
            m_DebugText.text = "Ghost now visible";
            //ghostBodyRenderer.material.SetColor("_Color", Color.Lerp(ghostBodyRenderer.material.color, ghostColor, Time.deltaTime * colorChangeInterval));
            ghostBodyRenderer.material.SetColor("_Color",ghostColor);
        }
        else{
            m_DebugText.text = "Ghost now invisible";
            ghostColor = Color.blue;
            ghostBodyRenderer.material.SetColor("_Color",ghostColor);
           //ghostBodyRenderer.material.SetColor("_Color", Color.Lerp(ghostBodyRenderer.material.color, ghostColor, Time.deltaTime * colorChangeInterval));
        }
    }
}
