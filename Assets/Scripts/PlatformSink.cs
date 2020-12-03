using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlatformSink : MonoBehaviour
{
    private Animator animator;
    private bool sink = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update(){
        animator.SetBool("Sink", sink);
    }

    void OnCollisionStay2D(Collision2D other){
         if (other != null && other.transform.position.y > transform.position.y){
             sink = true;
         }
    }

    void OnCollisionExit2D(Collision2D other){
        sink = false;
    }
}
