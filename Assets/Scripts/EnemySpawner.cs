using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float tempo;
    public float timer;

    private float timeBetweenBeats;

    public GameObject enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        timeBetweenBeats = 60f / tempo;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= timeBetweenBeats)
        {
            timer = Time.time;
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        GameObject bullet = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gameObject.transform.up * 20f, ForceMode2D.Impulse);
    }
}
