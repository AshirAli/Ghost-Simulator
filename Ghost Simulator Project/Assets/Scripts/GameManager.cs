using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

#region PUBLIC
    public GameObject m_GlobalPost;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
#endregion
#region PRIVATE
    private PlayerController playerController;
#endregion
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        InitGame();
    }
        
    //Initializes the game for each level.
    void InitGame()
    {
        SceneManager.LoadScene("Prototype-Scene");
    }

    void Start(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        InitializePostFx();
    }
    void Update()
    {

    }
#region PUBLIC_METHODS
    public void PlayerDamage(float damage){
        playerController.TakeDamage(damage);
    }
    public void HandlePostFx(bool state){
        m_GlobalPost.SetActive(state);
    }

#endregion
#region PRIVATE_METHODS
    void InitializePostFx(){
        m_GlobalPost = GameObject.Find("GlobalPost_GhostPhase");
        m_GlobalPost.SetActive(false);
    }
#endregion
}