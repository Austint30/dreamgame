using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class Ground_Patrol : MonoBehaviour
{
    public float distance;

    [SerializeField]
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        //EnemySound.PlaySound(EnemySound.Sound.RatSound, MainMenuButtonsScript.sfxVol);
    }
}
