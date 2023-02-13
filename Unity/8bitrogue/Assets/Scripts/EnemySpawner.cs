using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyParent;

    [SerializeField]
    private Vector2 rangeMin, rangeMax = Vector2.zero;

    private GameObject player;
    private PlayerStatsController playerStatsController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
        StartCoroutine(SpawnEnemy());
    }
    private void HandleEnemySpawn() {
        // Decide where to spawn monster
        Vector3 spawnPosition = new Vector2(Random.Range(rangeMin.x, rangeMax.x), Random.Range(rangeMin.y, rangeMax.y));

        if ((spawnPosition - player.transform.position).magnitude < 2f)
        {
            HandleEnemySpawn(); // it's too close, don't spawn it, start again
            return;
        }
        // Spawn Monster
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, new Quaternion());
        enemy.transform.parent = enemyParent.transform;
    }
    IEnumerator SpawnEnemy()
    {
        for (; ; )
        {
            if (!playerStatsController.getIsPlaying()) {
                yield return new WaitForSeconds(3f);
            }
            for (int i = 0; i < playerStatsController.getLevel(); i++)
            {
                // Decide if monster will spawn this loop
                if (Random.Range(0f, 2f) < .5f)
                    continue;
                HandleEnemySpawn();
            }
            Debug.Log("Spawn");
            yield return new WaitForSeconds(3f);
        }
    }
}
