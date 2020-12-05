using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorTrigger : Portal
{
    public string restrictToTag = "Player"; // Restrict portal access to gameobjects with a certain tag. Otherwise all rigidbody GameObjects will be transferred through.
    [Tooltip("Position the player/object on the floor after travelling through.")]
    public bool positionOnFloor = true;
    public bool isCheckpoint = true;
    public bool locked = false;
    public GameObject destinationTitleTextObject;

    [TextArea(15, 20)]
    public string doorDestinationText;

    [Header("Screen Fading")]
    public bool fadeScreen = true;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    [Header("Audio")]
    public AudioSource doorAudioSource;

    private Vector2 exitPosition;
    private GameObject passingObject;

    protected override void Start(){
        base.Start();
        destinationTitleTextObject.SetActive(false);
    }

    public override void OnObjectExit(GameObject obj)
    {
        base.OnObjectExit(obj);
        if (obj.tag == "Player" && CheckpointManager.instance){
            CheckpointManager.instance.SetCheckpoint(exitPosition, obj);
        }
    }

    protected override void OnLevelLoaded(GameObject passingObject, Portal otherPortal){
        this.otherPortal = otherPortal;
        // Move player to floor
        if (positionOnFloor){
            MoveToFloor(passingObject);
        }
        if (fadeScreen){
            SceneFade sceneFade = FindSceneFadeScript();
            if (sceneFade){
                this.passingObject = passingObject;
                sceneFade.SetFade(true);
                sceneFade.FadeOut(fadeOutDuration);
                sceneFade.AfterFadeOut(AfterFadeOut);
            }
        }
    }

    void AfterFadeOut(){
        Time.timeScale = 1;
    }

    void StartTrigger(){
        Trigger(passingObject);
    }
    
    void OnTriggerStay2D(Collider2D other){
        if (
            (restrictToTag.Length > 0 && restrictToTag == other.tag) ||
            (restrictToTag.Length <= 0 && other.GetComponent<Rigidbody>())
        ){
            SceneFade sceneFade = FindSceneFadeScript();
            if (Input.GetButtonDown("Interact") && !locked){
                if (doorAudioSource)
                    doorAudioSource.Play();
                if (sceneFade){
                    passingObject = other.gameObject;
                    Time.timeScale = 0;
                    sceneFade.FadeIn(fadeInDuration);
                    sceneFade.AfterFadeIn(StartTrigger);
                }
                else
                {
                    Trigger(other.gameObject);
                }
            }
        }
    }

    void MoveToFloor(GameObject passingObject){
        if (!otherPortal) return;
        Vector3 otherSpawnPos = otherPortal.spawnPosition;
        Vector2 offset = Vector3.zero;
        RaycastHit2D hit = Physics2D.Raycast(otherSpawnPos, Vector2.down, 10f);
        if (hit){
            Collider2D objCollider = passingObject.GetComponent<Collider2D>();
            if (objCollider){
                offset = Vector3.up * (objCollider.bounds.extents.y - 0.05f); // Subtracting by 0.05f fixes the issue where the character slightly falls by a pixel after being placed on the ground.
            }
            exitPosition = hit.point + offset;
            passingObject.transform.position = exitPosition;
        }
    }

    private SceneFade FindSceneFadeScript(){
        Transform sfobj = Camera.main.transform.Find("Canvas/SceneFade");
        if (!sfobj) return null;
        return sfobj.GetComponent<SceneFade>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player" && destinationTitleTextObject != null){
            var tm = destinationTitleTextObject.GetComponent<TextMeshPro>();
            if (tm && locked){
                tm.text = doorDestinationText + " (Locked)";
            }
            else if (tm){
                tm.text = doorDestinationText;
            }
            destinationTitleTextObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player" && destinationTitleTextObject != null){
            destinationTitleTextObject.SetActive(false);
        }
    }
}
