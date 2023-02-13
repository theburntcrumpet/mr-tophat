using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    private int level = 1;
    private float speedMultiplier = 1f;
    private float damageMultiplier = 5f;
    private float luckMultiplier = 1f;
    private int coins = 0;
    private Vector2 playerScreen;
    private float levelSeconds = 60f;
    private float remainingSeconds = 60f;
    private bool isPlaying = false;
    private Rigidbody2D playerRigidbody;
    private bool isDead = false;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject levelUpScreen;
    // Start is called before the first frame update
    void Start()
    {
        playerScreen = new Vector2();
        playerRigidbody = GetComponent<Rigidbody2D>();
  
    }

    // Update is called once per frame
    void Update()
    {
        playerScreen.x = (int) (transform.position.x + 8) / 8;
        playerScreen.y = (int)(transform.position.y + 8) / 8;
        levelSeconds = 58f + (level * 2f);
        HandleStartLevel();
    }

    public void boostSpeed(float speedBoost){
        speedMultiplier += speedBoost;
    }

    public float getSpeedMultiplier() {
        return speedMultiplier;
    }

    public void boostDamage(float damageBoost){
        damageMultiplier += damageBoost;
    }

    public float getDamageMultiplier() {
        return damageMultiplier;
    }

    public void boostLuck(float luckBoost) {
        luckMultiplier += luckBoost;
    }

    public float getLuckMultiplier() {
        return luckMultiplier;
    }

    public void advanceLevel() {
        level += 1;
    }

    public int getLevel() {
        return level;
    }

    public bool canAfford(int coins) {
        if (this.coins - coins < 0)
            return false;
        return true;
    }

    public void spendCoins(int coins) {
        this.coins -= coins;
    }

    public Vector2 getPlayerScreen() {
        return playerScreen;
    }

    public void addCoin() {
        coins++;
    }

    public int getCoins() {
        return coins;
    }


    private void HandleStartLevel() {
        if (playerScreen == new Vector2(1, 0)) {
            return;
        }

        if (isPlaying) {
            return;
        }
        Debug.Log("StartLevel");
        remainingSeconds = levelSeconds;
        isPlaying = true;
        StartCoroutine(timeLevel());
    }


    IEnumerator timeLevel() {
        while (remainingSeconds > 0f)
        {
            
            remainingSeconds--;
            if (remainingSeconds == 0f)
            {
                isPlaying = false;
                if (!isDead) {
                    advanceLevel();
                    levelUpScreen.SetActive(true);
                    playerRigidbody.velocity = Vector2.zero;
                    transform.position = new Vector2(4.7f, -3.6f);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public float getTimeRemaining() {
        return remainingSeconds;
    }

    public bool getIsPlaying() {
        return isPlaying;
    }

    public void kill() {
        isPlaying = false;
        isDead = true;
        transform.position = new Vector2(4.7f, -3.6f);
        playerRigidbody.velocity = Vector2.zero;
        GetComponent<PlayerController>().enabled = false;
        gameOverScreen.SetActive(true);
    }
}
