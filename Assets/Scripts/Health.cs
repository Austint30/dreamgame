using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Image[] healthBars;
    public bool isDamage;
    public bool comingFromKillZone;
    private bool callOnStay;
    float lastTimePlayed = 0f;
    [SerializeField]
    float playerMoveTimerMax = 0.2f;

    void Update()
    {
        if(callOnStay){
            if(Input.GetKeyDown(KeyCode.Return) && !isDamage){
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj){
                    Player player = playerObj.GetComponent<Player>();
                    if (player)
                        playerHeal(player);
                }
                callOnStay = false;
            }
            else if(comingFromKillZone){
                toggleGameOver();
            }
        }
    }

    private bool CanDamage(){
       
        if(lastTimePlayed + playerMoveTimerMax < Time.time){
            lastTimePlayed = Time.time;
            return true;
        } else {
            return false;
        }
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        Player player = _col.gameObject.GetComponentInParent<Player>();
        if(player && isDamage && CanDamage()){
            playerDamage(player);
        }
    }

    void OnTriggerStay2D(Collider2D _col)
    {
        Player player = _col.gameObject.GetComponentInParent<Player>();
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

    public void playerDamage(Player player)
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

    public void playerHeal(Player player)
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
