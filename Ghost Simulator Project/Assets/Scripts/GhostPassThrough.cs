using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPassThrough : MonoBehaviour
{
#region PUBLIC
#endregion

#region PRIVATE
    private Renderer wallMesh;
    private Renderer[] wallChildMesh;
#endregion
    void Start()
    {
        wallMesh = GetComponent<Renderer>();
        wallChildMesh = GetComponentsInChildren<Renderer>();
        MaterialPhase(false);
    }

    void Update()
    {
        
    }

#region PRIVATE_METHODS
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player"){
            MaterialPhase(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player"){
            MaterialPhase(false);
        }
    }
    ///<summary>Enables renderer in parent and disable renderer in children and vice versa</summary>
    private void MaterialPhase(bool state){
        foreach (Renderer r in wallChildMesh){
            r.enabled = state;
        }
        wallMesh.enabled = !state;
    }
#endregion
}
