using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{

    private static int PlayerLevel = 1;
    private static int PlayerHealth = 100;
    private static int PlayerLives = 5;

    public void setPlayerLevel(int x){
        PlayerLevel = x;
    }

    public void setPlayerHealth(int x){
        PlayerHealth = x;
    }

    public void setPlayerLives(int x){
        PlayerLives = x;
    }
}
