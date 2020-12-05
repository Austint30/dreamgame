using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToBeContinued : MonoBehaviour
{
    void OnTriggerEnter2D(){
        SceneManager.LoadScene(7);
    }
}
