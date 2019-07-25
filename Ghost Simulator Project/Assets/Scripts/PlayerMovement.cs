using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1.0f;
    [SerializeField]
    private float turnSpeed = 20f;
    private bool isWalking;
    private bool canMove;
    private Vector3 m_Movement = Vector3.zero;
    private Quaternion m_Rotation = Quaternion.identity;
    private AudioSource m_AudioSource;
    private new Rigidbody rigidbody;
    private float horizontal;
    private float vertical;
    private CharacterController characterController;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        AllowPlayerMovement();
    }

    void FixedUpdate()
    {
        if(canMove){
            horizontal = Input.GetAxis ("Horizontal");
            vertical = Input.GetAxis("Vertical");
            
            //m_Movement.Set(horizontal, 0f, vertical);
            m_Movement = new Vector3(horizontal,0f,vertical);

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

            //Call either MovementCharacterController OR MovementBasic
            //MovementCharacterController();
            MovementBasic();
        }
    }
    
    public void AllowPlayerMovement(){
        canMove = true;
    }

    ///<summary>Movement using Character Controller </summary>
    void MovementCharacterController(){
        Vector3 moveDirection = m_Movement * m_Speed;
        //moveDirection.y -= 10f * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
        transform.rotation = m_Rotation; 
    }
    void MovementBasic(){
        //m_Movement.Normalize ();  
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
        transform.rotation = m_Rotation; 
        //transform.position = transform.position + m_Movement * m_Speed * Time.deltaTime;
        rigidbody.AddForce(m_Movement * m_Speed * Time.deltaTime);
    }
}
