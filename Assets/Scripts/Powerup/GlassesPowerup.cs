using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class GlassesPowerup : AbstractPowerup
{
    public SpriteRenderer glassesOnCharacter;
    public PlayerBoyAnimControl playerBoyAnimControl;
    private bool initialized = false;
    private bool active = false;
    private GameObject glassesUI;

    public override void Initialize(){
        base.Initialize(); // Call parent class Initialize method
        initialized = true;
        Player playerScript = GetComponentInParent<Player>();
        if (playerScript && playerBoyAnimControl){
            playerBoyAnimControl.playerMovement = playerScript;
        }
    }

    void Start(){
        try{
            Transform cameraTransform = Camera.main.transform;
            glassesUI = cameraTransform.Find("GlobalGameUI/GlassesUI").gameObject;
        }
        catch(System.Exception e){
            Debug.LogWarning("Could not get Glasses UI: " + e);
        }
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
        if (glassesUI){
            glassesUI.GetComponent<CanvasGroup>().alpha = 1;
        }
        glassesOnCharacter.gameObject.SetActive(true);
    }

    void Deactivate(){
        active = false;
        GlassesRevealer[] rev = FindRevealers();
        foreach (var r in rev)
        {
            r.Hide();
        }
        if (glassesUI){
            glassesUI.GetComponent<CanvasGroup>().alpha = 0;
        }
        glassesOnCharacter.gameObject.SetActive(false);
    }

    private  GlassesRevealer[] FindRevealers(){
        GameObject[] objects = GameObject.FindGameObjectsWithTag("GlassesInvisible");
        return objects.Select(r => r.GetComponent<GlassesRevealer>()).Where(r => r != null).ToArray();
    }
}
