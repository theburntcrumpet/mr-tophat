using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private GameObject canePrefab;

    [SerializeField]
    private GameObject caneParent;

    private Rigidbody2D rigidbody2D;
    private PlayerStatsController playerStatsController;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerStatsController = GetComponent<PlayerStatsController>();
        StartCoroutine(ThrowCane());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1f);
        rigidbody2D.velocity = movement * (speed * playerStatsController.getSpeedMultiplier());
    }

    IEnumerator ThrowCane()
    {
        for (; ; )
        {
            if (playerStatsController.getIsPlaying())
            {
                Vector3 spawnPosition = transform.position;
                GameObject cane = Instantiate(canePrefab, spawnPosition, new Quaternion());
                cane.transform.parent = caneParent.transform;
            }
            // execute block of code here
            yield return new WaitForSeconds(.5f);
        }
    }
}
