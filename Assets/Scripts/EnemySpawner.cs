using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float timePerBeat;
    float timer;

    int totalBeat;

    public int startBeat;
    public int beatBeforeKill;

    public int beatsBetweenSpawn = 4;

    public GameObject enemyPrefab;
    public Vector2 moveDirection;

    public bool isInitialised = false;
    public bool isActive = false;

    public bool isHorizontal;

    public int dashGrid = 1;
    public float horizontalShift = 0f;
    public float verticalShift = 0.25f;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInitialised && GameManager.GetInstance().isMusicPlaying)
        {
            isInitialised = true;

            timePerBeat = GameManager.GetInstance().timePerBeat;
            timer = Time.time;

            totalBeat = 0;
        }

        if (isInitialised && totalBeat >= startBeat)
        {
            isActive = true;
        }

        if (isInitialised && totalBeat - startBeat >= beatBeforeKill)
        {
            isActive = false;
        }

        if (Time.time - timer >= timePerBeat * beatsBetweenSpawn)
        {
            timer = Time.time;
            totalBeat++;

            EnemySpawn();
        }

    }

    void EnemySpawn()
    {
        if (!isInitialised || !isActive)
        {
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

        enemy.GetComponent<Enemy>().dashGrid = dashGrid;
        enemy.GetComponent<Enemy>().horizontalShift = horizontalShift;
        enemy.GetComponent<Enemy>().verticalShift = verticalShift;
    }
}
