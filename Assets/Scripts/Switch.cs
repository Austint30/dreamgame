using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Platform platform;
    public Transform opos1, opos2; //original positions
    public Transform pos1, pos2;
    public static bool isSwitched = false;

    public void ChangeDirection(){
        if(isSwitched){
            opos1 = platform.pos1;
            opos2 = platform.pos2;
            platform.pos1 = pos1;
            platform.pos2 = pos2;
        }
        else{
            platform.pos1 = opos1;
            platform.pos2 = opos2;
        }
    }

    public void ToggleSwitch(bool toggle){
        isSwitched = toggle;
    }

    private void OnTriggerEnter2D(Collider2D _col){
        if (_col.gameObject.CompareTag ("DialogueTrigger") && Input.GetKeyDown(KeyCode.Return)) {
            
            //Have to add in animation
            
            if(isSwitched == false){
                ToggleSwitch(true);
                ChangeDirection();
            }
            else{
                ToggleSwitch(false);
                ChangeDirection();
            }
        }
    }
}
