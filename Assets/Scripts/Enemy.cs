using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float defaultSpeed = 0f;
    public float dashSpeed = 100f;
    public float dashTime = 0.1f;

    public float currentSpeed;

    public int tempoPerMovement;

    private float timePerBeat;
    private float timer;

    public Rigidbody2D rb;

    public Vector2 movement;

    public int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        Dash();

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
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

        if (Time.time - timer >= timePerBeat)
        {
            timer = Time.time;
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        currentSpeed = dashSpeed;

        yield return new WaitForSeconds(dashTime);

        currentSpeed = defaultSpeed;
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
