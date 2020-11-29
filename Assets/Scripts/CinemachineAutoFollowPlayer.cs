using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineAutoFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cm = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        var player = GameObject.FindGameObjectWithTag("Player");
        if(cm && player){
            cm.enabled = false;
            cm.Follow = player.transform;
            cm.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
