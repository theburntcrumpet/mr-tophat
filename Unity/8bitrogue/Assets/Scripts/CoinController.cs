using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject player;
    private PlayerStatsController playerStatsController;
    private GameObject coinSound;
    private AudioSource coinSoundSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
        coinSound = GameObject.FindGameObjectWithTag("CoinSound");
        coinSoundSource = coinSound.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!playerStatsController.getIsPlaying()) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            coinSoundSource.Play();
            playerStatsController.addCoin();
            Destroy(gameObject);
        }
    }
}
