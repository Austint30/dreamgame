using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject trigger;
    [SerializeField] public bool isOnEnter;
    private bool callOnStay;

    // Start is called before the first frame update
    void Start()
    {
        trigger.SetActive (false);
        callOnStay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(callOnStay){
            if(Input.GetKeyDown(KeyCode.B)){
                trigger.SetActive (true);
                var dialogueVar = trigger.GetComponent<DialogueHolder>();
                StartCoroutine(dialogueVar.dialogueSequence());
                callOnStay = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _col){
        if (_col.gameObject.CompareTag ("DialogueTrigger") && isOnEnter) {
            trigger.SetActive (true);
            var dialogueVar = trigger.GetComponent<DialogueHolder>();
            StartCoroutine(dialogueVar.dialogueSequence());
        }
    }

    void OnTriggerStay2D(Collider2D _col)
    {
        if (_col.gameObject.CompareTag ("DialogueTrigger") && !isOnEnter) {
                Debug.Log("Trigger");
            callOnStay = true;
        }
    }
}
