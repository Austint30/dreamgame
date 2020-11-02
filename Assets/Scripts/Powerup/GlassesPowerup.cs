using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesPowerup : AbstractPowerup
{
    private Player playerScript;
    public override void Initialize(){
        base.Initialize(); // Call parent class Initialize method
        playerScript = GetComponentInParent<Player>();
        Debug.Log("Glasses of Truth activated!");
        if (playerScript){
            playerScript.speed = 15;
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
