using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float liveTime = 1f;
    float timer;

    void Start ()
    {
        timer = Time.time;
    }

    void Update()
    {
        if (Time.time - timer >= liveTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisitonEnter2D (Collision2D collision)
    {
        Destroy(gameObject);
    }

}
