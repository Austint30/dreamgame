using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsScript : MonoBehaviour
{
    public void PlayGame () {
        SceneManager.LoadScene(0); //will have to change this with where current player last saved if already started game
    }
}
