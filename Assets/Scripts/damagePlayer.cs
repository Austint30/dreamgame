/*using UnityEngine; 
using System.Collections;

public class damagePlayer : MonoBehaviour  {

    public int playerHealth =30;
    int damage = 10;

    void Start()[
        print(playerHealth) ;

    ]

    void OnCollisionEnter(Collision _collision){
        if (_collision.gameObject.tag == "enemyDong"){
            playerHealth -= damage;
            print("player just touched something weird and health left" + playerHealth);
        }
    }
}*/