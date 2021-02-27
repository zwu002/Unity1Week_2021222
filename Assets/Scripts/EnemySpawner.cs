using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timePerBeat;
    float timer;

    public GameObject enemyPrefab;
    public Vector2 moveDirection;

    public bool isInitialised = false;


    // Start is called before the first frame update
    void Start()
    {
        timePerBeat = GameManager.GetInstance().timePerBeat;
        timer = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInitialised && GameManager.GetInstance().isMusicPlaying)
        {
            isInitialised = true;
        }

        if (Time.time - timer >= timePerBeat)
        {
            timer = Time.time;
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        if (!isInitialised)
        {
            return;
        }

        GameObject bullet = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(moveDirection * 20f, ForceMode2D.Impulse);
    }
}
