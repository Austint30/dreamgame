using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameObjectActivator : MonoBehaviour
{
    public GameObject gameObjectToActivateWhenTriggered;
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            gameObjectToActivateWhenTriggered.SetActive(true);
        }
    }
}
