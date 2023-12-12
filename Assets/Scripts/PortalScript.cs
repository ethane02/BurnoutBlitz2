using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Collider portalA;
    public Collider portalB;

    private bool isTeleporting;

    // Use OnTriggerEnter for detecting when the player enters the portal trigger zone
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        Debug.Log("Hit");
     
        TeleportPlayer(other.transform);
        
    }

    void TeleportPlayer(Transform other)
    {
        Debug.Log("Activating teleport script");

        isTeleporting = true;
        
        other.position = portalB.transform.position;

        Debug.Log("After teleport script");



        isTeleporting = false;
    }
}

