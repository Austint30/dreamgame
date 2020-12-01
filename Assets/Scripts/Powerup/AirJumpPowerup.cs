using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpPowerup : AbstractPowerup
{
    private Player playerScript;
    public override void Initialize(){
        base.Initialize(); // Call parent class Initialize method
        playerScript = GetComponentInParent<Player>();
        Debug.Log("Air jump powerup activated!");
        if (playerScript){
            playerScript.maxJumps = 2;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
