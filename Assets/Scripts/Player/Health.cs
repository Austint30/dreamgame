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
            if(Input.GetKeyDown(KeyCode.Return) && isDamage){
                playerDamage();
            }
            else if(Input.GetKeyDown(KeyCode.Return) && !isDamage){
                playerHeal();
            }
            else if(comingFromKillZone){
                toggleGameOver();
            }
        }
    }

    void OnTriggerStay2D(Collider2D _col)
    {
        if (_col.gameObject.CompareTag ("DialogueTrigger")) {
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

    void OnTriggerExit2D(Collider2D _col)
    {
        if (_col.gameObject.CompareTag ("DialogueTrigger")) {
                // Debug.Log("Trigger");
            callOnStay = false;
        }
    }


}
