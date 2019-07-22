using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 1.0f;
    public float turnSpeed = 20f;
    bool isWalking;
    bool canMove;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        AllowPlayerMovement();
    }

    void FixedUpdate()
    {
        if(canMove){
            float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;

        //m_Animator.SetBool ("IsWalking", isWalking);

        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play ();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }    
        float step = m_Speed * Time.deltaTime;
        
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
        transform.rotation = m_Rotation; 
        transform.position = transform.position + m_Movement * m_Speed * Time.deltaTime;
        }
    }
    
    public void AllowPlayerMovement(){
        canMove = true;
    }

}
