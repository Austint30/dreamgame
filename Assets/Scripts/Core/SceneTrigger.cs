using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    private bool callOnStay;
    [SerializeField] public int sceneNumber;


    // Start is called before the first frame update
    void Start()
    {
        callOnStay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(callOnStay){
            if(Input.GetKeyDown(KeyCode.B)){
                // Debug.Log("Trigger");
                SceneManager.LoadScene(sceneNumber);
                callOnStay = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D _col)
    {
        if (_col.gameObject.CompareTag ("DialogueTrigger")) {
            callOnStay = true;
        }
    }
}
