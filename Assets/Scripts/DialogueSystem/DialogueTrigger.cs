using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace DialogueSystem{
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField]private DialogueHolder dH;
    public static bool disabled = true;

    void OnTriggerEnter2D(Collider2D other)
     {
         if (Input.GetKeyDown(KeyCode.Return)) 
         {
             dH.isActive = true;
         }
         else{
             dH.isActive = false;
         }
  
     }

     void Update () {
         if (disabled){
             dH.isActive = false;
         }
         else{
             dH.isActive = true;
         }
     }
}
//}
