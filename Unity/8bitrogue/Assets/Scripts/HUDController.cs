using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    private GameObject levelTextObject;
    private TMPro.TextMeshProUGUI levelText;
    private GameObject player;
    private PlayerStatsController playerStatsController;
    private GameObject timeRemainingTextObject;
    private TMPro.TextMeshProUGUI timeRemainingText;
    private GameObject coinTextObject;
    private TMPro.TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
        levelTextObject = GameObject.FindGameObjectWithTag("LevelText");
        levelText = levelTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        timeRemainingTextObject = GameObject.FindGameObjectWithTag("TimeRemainingText");
        timeRemainingText = timeRemainingTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        coinTextObject = GameObject.FindGameObjectWithTag("Coins");
        coinText = coinTextObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        levelText.text = $"Level {playerStatsController.getLevel()}";
        timeRemainingText.text = $"{playerStatsController.getTimeRemaining()}";
        coinText.text = $"{playerStatsController.getCoins()} Coins";
    }
}
