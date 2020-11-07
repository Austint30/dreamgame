using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesPowerup : AbstractPowerup
{
    private Player _playerScript;
    private bool _initialized = false;

    // Allow read access to external components
    public bool activated {
        get {
            return _activated;
        }
    }

    private bool _activated;

    public override void Initialize(){
        base.Initialize(); // Call parent class Initialize method
        _playerScript = GetComponentInParent<Player>();
        _initialized = true;
        Debug.Log("Glasses of Truth initialized!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_initialized) return;
        if (Input.GetButtonDown("Glasses")) ToggleActive();
    }

    public void ToggleActive(){
        if (_activated) DeActivate();
        if (!_activated) Activate();
    }

    public void Activate(){
        _activated = true;
        // TODO: Play glasses animation
        Debug.Log("Glasses powerup activated!");
        _playerScript.disableInput = true;
    }

    public void DeActivate(){
        _activated = false;
        Debug.Log("Glasses powerup deactivated!");
        _playerScript.disableInput = false;
    }
}
