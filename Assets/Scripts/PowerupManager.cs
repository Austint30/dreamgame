using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("GameObject to store powerups in")]
    private Transform powerupCollection;

    [SerializeField]
    private int powerupSpriteSortingOrder = 11;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Powerup"){
            GrabPowerup(other.transform);
        }
    }

    private void GrabPowerup(Transform objectTransform){
        if (powerupCollection == null){
            Debug.LogError("Powerup Collection is null! There is nothing to store the powerups in.");
            return;
        }
        SpriteRenderer sr = objectTransform.GetComponent<SpriteRenderer>();
        AbstractPowerup pScript = objectTransform.GetComponent<AbstractPowerup>();
        objectTransform.parent = powerupCollection;
        objectTransform.localPosition = Vector3.zero;
        if (sr != null){
            sr.sortingOrder = powerupSpriteSortingOrder;
        }
        if (pScript){
            pScript.Initialize();
        }
    }
}
