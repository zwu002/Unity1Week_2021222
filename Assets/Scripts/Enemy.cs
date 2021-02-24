using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float defaultSpeed = 0f;
    public float dashSpeed = 100f;
    public float dashTime = 0.1f;

    public float currentSpeed;

    public float tempo;
    public int tempoPerMovement;

    private float timer;
    private float timeBetweenBeats;

    public Rigidbody2D rb;

    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        Dash();

        timeBetweenBeats = 60f / tempo;
        timer = Time.time;

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

        if (Time.time - timer >= timeBetweenBeats)
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
}
