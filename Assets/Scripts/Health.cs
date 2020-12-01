using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Image[] healthBars;
    public Player player;
    public bool isDamage;
    public bool comingFromKillZone;
    private bool callOnStay;

    void Update()
    {
        if(callOnStay){
            if(Input.GetKeyDown(KeyCode.Return) && !isDamage){
                playerHeal();
            }
            else if(comingFromKillZone){
                toggleGameOver();
            }
        }
    }

    private bool CanDamage(){
        float lastTimePlayed = 0f;
        float playerMoveTimerMax = 0.2f;
        if(lastTimePlayed + playerMoveTimerMax < Time.time){
            lastTimePlayed = Time.time;
            return true;
        } else {
            return false;
        }
    }

    void OnCollisionEnter(Collision _col)
    {
        if(_col.gameObject.CompareTag("DialogueTrigger") && isDamage && CanDamage()){
            playerDamage();
        }
    }

    void OnTriggerStay2D(Collider2D _col)
    {
        if (_col.gameObject.CompareTag ("DialogueTrigger")) {
            callOnStay = true;
        }
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject.CompareTag ("DialogueTrigger") && comingFromKillZone) {
            callOnStay = true;
        }
    }

    public void playerDamage()
    {
        if(player.health > 0) player.health--;
        if(player.health == 2){
            healthBars[0].enabled = (false);
        }
        if(player.health == 1){
            healthBars[1].enabled = (false);
        }
        if(player.health == 0){
            healthBars[2].enabled = (false);
            toggleGameOver();
        }
    }

    public void playerHeal()
    {
        if(player.health < 3) player.health++;
        if(player.health == 3){
            healthBars[0].enabled = (true);
        }
        if(player.health == 2){
            healthBars[1].enabled = (true);
        }
        if(player.health == 1){
            healthBars[2].enabled = (true);
        }
    }

    private void toggleGameOver()
    {
        SceneManager.LoadScene(5);
    }

    // void OnTriggerExit2D(Collider2D _col)
    // {
    //     if (_col.gameObject.CompareTag ("DialogueTrigger")) {
    //             // Debug.Log("Trigger");
    //         callOnStay = false;
    //     }
    // }


}
