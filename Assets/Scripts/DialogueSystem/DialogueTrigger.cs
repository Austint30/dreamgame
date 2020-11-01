using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem{
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField]private GameObject dH;

    void OnTriggerEnter(Collider other)
     {
         if (Input.GetKeyDown(KeyCode.Return)) 
         {
             dH.SetActive(true);
         }
  
     }
}
}
