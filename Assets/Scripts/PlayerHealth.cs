using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityTimeInSecs = 4;
    [Tooltip("Container of all character sprites ")]
    public SpriteRenderer[] spritesToBlink;
    [Tooltip("By default, how hard the player will be pushed if damaged from another entity.")]
    public float defaultDamageKnockbackStrength = 3f;
    private Rigidbody2D playerRb;
    private Player playerScript;
    private bool invincible = false;
    private bool spritesVisible = true;

    public int currentHealth;
    public bool stunned = false;
    public float stunDuration = 1f;
    // Start is called before the first frame update

    [Header("Visual Properties")]
    public float damageBlinkRate = 0.2f;

    [Header("Game Over")]
    public int gameOverScene = -1;

    void Start()
    {
        currentHealth = maxHealth;
        playerRb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isGrounded && stunned){
            StopCoroutine(Stun());
            playerScript.disableInput = false;
            stunned = false;
        }
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    // Initialize health bar on scene load
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.isLoaded){
            HealthBar healthBar = FindHealthBar();
            if (healthBar){
                healthBar.ResetHealth(currentHealth);
            }
        }
    }

    public void Damage(int healthPts){
        if (!invincible){
            SubtractHealth(healthPts);
            StartCoroutine(BeginInvincibility());
            UpdateHealthBar(currentHealth);
            CheckDead();
        }
    }

    // Like Damage(), but pushes the player in the opposite direction of the contact position
    public void DamageAndKnockback(int healthPts, Vector2 knockbackDirection, float knockbackStrength=3f){
        if (!invincible){
            SubtractHealth(healthPts);
            StartCoroutine(Stun());

            // Push the player in the air, even if hit exactly horizontally
            // Vector2 modifiedDirection = (knockbackDirection + Vector2.up * knockbackStrength*Mathf.Abs(knockbackDirection.x)/4).normalized;
            // Vector2 modifiedDirection = new Vector2(knockbackDirection.x, knockbackDirection.y + knockbackStrength*Mathf.Abs(knockbackDirection.x)/4).normalized;
            if (playerScript == null || !playerScript.isGrounded){
                playerRb.velocity = knockbackDirection*knockbackStrength;
            }
            else if (playerScript == null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, knockbackStrength);
            }
            StartCoroutine(BeginInvincibility());
            UpdateHealthBar(currentHealth);
            CheckDead();
        }
    }

    public void Heal(int healthPts){
        AddHealth(healthPts);
        UpdateHealthBar(currentHealth);
    }

    private IEnumerator BeginInvincibility(){
        invincible = true;
        yield return StartCoroutine(BeginBlinking());
        invincible = false;
    }

    private IEnumerator BeginBlinking(){
        var endTime = Time.time + invincibilityTimeInSecs;
        while (Time.time < endTime)
        {
            SetSpritesVisible(!spritesVisible);
            yield return new WaitForSeconds(damageBlinkRate);
        }
        SetSpritesVisible(true);
    }

    private IEnumerator Stun(){
        playerScript.disableInput = true;
        yield return new WaitForSeconds(stunDuration);
        playerScript.disableInput = false;
        stunned = false;
    }

    void UpdateHealthBar(int health){
        HealthBar healthBar = FindHealthBar();
        if (healthBar){
            healthBar.ChangeHealth(health);
        }
        
    }

    private HealthBar FindHealthBar(){
        GameObject hbObj = GameObject.FindGameObjectWithTag("HealthBar");
        HealthBar healthBar;
        if (!hbObj){
            Debug.LogError("Health bar could not be found in scene! Make sure the health bar has the 'HealthBar' tag.");
            return null;
        }
        healthBar = hbObj.GetComponent<HealthBar>();
        if (healthBar == null){
            Debug.LogError("Health bar has no 'HealthBar' script attached!");
        }
        return healthBar;
    }

    private void SubtractHealth(int healthPts){
        currentHealth -= healthPts;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void AddHealth(int healthPts){
        currentHealth += healthPts;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void SetSpritesVisible(bool visible){
        foreach (var sprite in spritesToBlink)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.b, sprite.color.g, visible ? 1 : 0);
        }
        spritesVisible = visible;
    }

    private void CheckDead(){
        if (currentHealth <= 0 && gameOverScene > -1){
            DontDestroyOnLoad(this.gameObject);
            gameObject.SetActive(false);
            Camera.main.gameObject.SetActive(false);
            SceneManager.LoadScene(gameOverScene);
        }
    }
}
