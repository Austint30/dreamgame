using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuButtonsScript : MonoBehaviour
{

    public void PlayGame () {
        PlayMenuSound();
        System.Threading.Thread.Sleep(1000);
        SceneManager.LoadScene(1); //will have to change this with where current player last saved if already started game
    }

    public void QuitGame()
    {
        PlayMenuSound();
        System.Threading.Thread.Sleep(1000);
        Application.Quit();
    }

    public void PlayMenuSound(){
        SoundHub.PlaySound(SoundHub.Sound.MenuButtonSound);
    }
}