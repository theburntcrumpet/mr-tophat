using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private PlayerStatsController playerStatsController;
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private float baseSpeed = 2f;

    private float baseHp = 15f;

    [SerializeField]
    private GameObject coinPrefab;


    private GameObject deathSound;
    private AudioSource deathClip;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
        hp = baseHp + (playerStatsController.getLevel());
        deathSound = GameObject.FindGameObjectWithTag("DeathSound");
        deathClip = deathSound.GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStatsController.getIsPlaying()) {
            DestroyImmediate(gameObject);
            return;
        }
        float speed = baseSpeed + (.1f * playerStatsController.getLevel());
        rigidbody2D.velocity = Vector2.zero;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        if (hp <= 0f) {
            deathClip.Play();
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= .5f;
            Instantiate(coinPrefab, spawnPosition, new Quaternion());
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            deathClip.Play();
            playerStatsController.kill();
        }

        if (collision.gameObject.tag == "Cane") {
            hp -= (15f * playerStatsController.getDamageMultiplier());
            Destroy(collision.gameObject);
        }
    }
}
