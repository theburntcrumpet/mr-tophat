using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSelectController : MonoBehaviour
{
    private GameObject[] selects;
    private GameObject player;
    private GameObject damageCostTextObject;
    private GameObject speedBoostTextObject;
    enum Selection { damageBoost, speedBoost, nothing };
    Selection selection = Selection.damageBoost;
    private PlayerStatsController playerStatsController;
    private PlayerController playerController;
    private float damageBoostAmount, speedBoostAmount = 0f;
    private int damageBoostCost, speedBoostCost = 0;
    TMPro.TextMeshProUGUI damageCostText;
    TMPro.TextMeshProUGUI speedCostText;
    // Start is called before the first frame update
    void Awake()
    {
        selects = new GameObject[]
        {
            GameObject.FindGameObjectWithTag("LeftSelect"),
            GameObject.FindGameObjectWithTag("RightSelect"),
            GameObject.FindGameObjectWithTag("BottomSelect")
        };
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
        playerController = player.GetComponent<PlayerController>();
        damageCostTextObject = GameObject.FindGameObjectWithTag("DamagePriceText");
        speedBoostTextObject = GameObject.FindGameObjectWithTag("SpeedPriceText");
        speedCostText = speedBoostTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        damageCostText = damageCostTextObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        
        SelectLeft();

        damageBoostAmount = (playerStatsController.getLevel() - 1) * 0.1f;
        speedBoostAmount = (playerStatsController.getLevel() - 1) * 0.05f;

        damageBoostCost = (playerStatsController.getLevel() -1) * 15;
        speedBoostCost = (playerStatsController.getLevel() -1) * 10;

        speedCostText.text = $"{speedBoostCost}";
        damageCostText.text = $"{damageBoostCost}";
        
        playerController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        if (v < 0) {
            SelectBottom();
            return;
        }

        if (v > 0 || h < 0) {
            SelectLeft();
            return;
        }

        if (h > 0) {
            SelectRight();
            return;
        }

        if (Input.GetButtonDown("Jump")) {
            HandleSelection();
        }
    }

    void SelectLeft() {
        selects[0].SetActive(true);
        selects[1].SetActive(false);
        selects[2].SetActive(false);
        selection = Selection.damageBoost;
    }

    void SelectRight() {
        selects[0].SetActive(false);
        selects[1].SetActive(true);
        selects[2].SetActive(false);
        selection = Selection.speedBoost;
    }

    void SelectBottom() {
        selects[0].SetActive(false);
        selects[1].SetActive(false);
        selects[2].SetActive(true);
        selection = Selection.nothing;
    }

    void HandleSelection() {
        if (selection == Selection.damageBoost) {
            if (!playerStatsController.canAfford(damageBoostCost))
            {
                return;
            }
            playerStatsController.spendCoins(damageBoostCost);
            playerStatsController.boostDamage(damageBoostAmount);
        }

        if (selection == Selection.speedBoost) {
            if (!playerStatsController.canAfford(speedBoostCost)) {
                return;
            }
            playerStatsController.spendCoins(speedBoostCost);
            playerStatsController.boostSpeed(speedBoostAmount);
        }
        playerController.enabled = true;
        gameObject.SetActive(false);
    }
}
