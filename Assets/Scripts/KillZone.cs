using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour
{
    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){

        //TODO: Connect with health management script on player gameobject and subtrack health to 0.
        Player playerScript = other.GetComponent<Player>();
        if (playerScript){
            Debug.Log("Player has hit kill zone! Player needs to be killed and respawned at last checkpoint.");
        }
    }
}
