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

    public int health = 3;

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
    }

    void Die()
    {
        GameManager.GetInstance().score += 100;
        Destroy(gameObject);
    }
}
