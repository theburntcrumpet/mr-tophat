using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour
{
    private float speed = 7f;
    private GameObject[] enemies;
    private GameObject nearest = null;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null) {
            Destroy(gameObject);
            return;
        }
        foreach (GameObject enemy in enemies) {
            if (nearest == null) {
                nearest = enemy;
                continue;
            }

            Vector2 distance = transform.position - enemy.transform.position;
            if (distance.magnitude < (transform.position - nearest.transform.position).magnitude) {
                nearest = enemy;
            }
        }
        if (nearest == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nearest == null) {
            DestroyImmediate(gameObject);
            DestroyImmediate(this);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, nearest.transform.position, speed * Time.deltaTime);
    }
}
