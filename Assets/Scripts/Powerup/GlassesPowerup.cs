using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlassesPowerup : AbstractPowerup
{
    private bool initialized = false;
    private bool active = false;
    public override void Initialize(){
        base.Initialize(); // Call parent class Initialize method
        initialized = true;
    }
    
    void Update(){
        if (initialized && Input.GetButton("Glasses")){
            Activate();
        }
        if (initialized && Input.GetButtonUp("Glasses")){
            Deactivate();
        }
    }

    void Toggle(){
        if (active)
            Deactivate();
        else
            Activate();
    }

    void Activate(){
        active = true;
        GlassesRevealer[] rev = FindRevealers();
        foreach (var r in rev)
        {
            r.Reveal();
        }
    }

    void Deactivate(){
        active = false;
        GlassesRevealer[] rev = FindRevealers();
        foreach (var r in rev)
        {
            r.Hide();
        }
    }

    private  GlassesRevealer[] FindRevealers(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GlassesInvisible");
        return objects.Select(r => r.GetComponent<GlassesRevealer>()).Where(r => r != null).ToArray();
    }
}
