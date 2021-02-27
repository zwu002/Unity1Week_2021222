using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int tempoPerMovement;

    private float timePerBeat;
    private float timer;

    public Rigidbody2D rb;

    public Vector2 movement;

    public int health = 2;

    public bool isPerfectHit = false;

    // Start is called before the first frame update
    void Start()
    {
        timePerBeat = GameManager.GetInstance().timePerBeat;
        timer = Time.time;
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }


    void FixedUpdate()
    {
        if (Time.time - timer >= timePerBeat)
        {
            timer = Time.time;
            Dash();
        }
    }

    void Dash()
    {
        gameObject.transform.Translate(movement);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (damage == 2)
        {
            isPerfectHit = true;
        }
    }

    void Die()
    {
        if (isPerfectHit)
        {
            GameManager.GetInstance().perfectHit++;
        }
        else
        {
            GameManager.GetInstance().hit++;
        }

        Destroy(gameObject);
    }
}
