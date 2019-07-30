using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
#region PUBLIC

#endregion
#region PRIVATE
    GameManager gameManager;
    Renderer meshRenderer;  //Deactivate on start (visual cue in edit mode)
#endregion
    void Start()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("GameController");
        gameManager = manager.GetComponent<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.enabled = false;
    }
    void Update()
    {
        
    }

#region PUBLIC_METHODS

#endregion

#region PRIVATE_METHODS
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player"){    //TakeDamage
            gameManager.PlayerDamage(10f);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player"){    //StopTaking Damage
            
        }
    }
#endregion
}
