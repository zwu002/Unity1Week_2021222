﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int tempoPerMovement;

    private float timePerBeat;
    private float timer;

    public Rigidbody2D rb;

    public int health = 2;
    public string colourTag;

    public bool isPerfectHit = false;

    public int dashGrid;
    public float horizontalShift;
    public float verticalShift;

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
        gameObject.transform.Translate(Vector3.up * verticalShift * dashGrid);
        gameObject.transform.Translate(Vector3.left * horizontalShift);
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

        GameManager.GetInstance().hit++;

        Destroy(gameObject);
    }
}
