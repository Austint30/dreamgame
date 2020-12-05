using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;
    public Sprite[] healthBarFrames; 
    [SerializeField]
    [Range(0, 3)]
    private int currentHealth = 3;

    [Header("Visual Properties")]
    public float damageBlinkDuration = 4f;
    public float damageBlinkRate = 0.2f;

    void Start(){
        ResetHealth(currentHealth);
    }

    public void ResetHealth(int health){
        currentHealth = Mathf.Clamp(health, 0, healthBarFrames.Length - 1);
        UpdateUI(health);
    }

    public void ChangeHealth(int health){
        if (health > currentHealth){
            StopCoroutine(DecreaseHealthUI(health));
            currentHealth = Mathf.Clamp(health, 0, healthBarFrames.Length - 1);
            UpdateUI(currentHealth);
        }
        else if (health < currentHealth)
        {
            StopCoroutine(DecreaseHealthUI(health));
            StartCoroutine(DecreaseHealthUI(health));
            currentHealth = Mathf.Clamp(health, 0, healthBarFrames.Length - 1);
        }
    }

    private IEnumerator DecreaseHealthUI(int health){
        int lastHealth = currentHealth;
        int nextHealth = health;
        float endTime = Time.time + damageBlinkDuration;
        while (Time.time < endTime)
        {
            yield return new WaitForSeconds(damageBlinkRate);
            UpdateUI(nextHealth);
            yield return new WaitForSeconds(damageBlinkRate);
            UpdateUI(lastHealth);
        }
        UpdateUI(health);
    }

    private void UpdateUI(int health){
        if (healthBarFrames.Length > 0){
            image.sprite = healthBarFrames[Mathf.Clamp(health, 0, healthBarFrames.Length - 1)];
        }
    }

}
