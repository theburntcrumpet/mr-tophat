using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private PlayerStatsController playerStatsController;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //    Vector2 playerScreen = playerStatsController.getPlayerScreen();
        //   transform.position = new Vector3(initialPosition.x + (((playerScreen.x -1) * 2) * initialPosition.x) , initialPosition.y -(playerScreen.y * initialPosition.y * 2), transform.position.z);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
