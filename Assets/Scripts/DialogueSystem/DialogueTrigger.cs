using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem{
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField]private DialogueHolder dH;

    void OnTriggerEnter2D(Collider2D other)
     {
         if (other.tag == "Player" && Input.GetKeyDown(KeyCode.Return)) 
         {
             dH.setA(true);
            StartCoroutine(dH.dialogueSequence());
         }
  
     }
}
}
