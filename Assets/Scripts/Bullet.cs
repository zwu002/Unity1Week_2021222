using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float liveTime = 1f;
    float timer;

    public string hitTag;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy Hit + " + collision.gameObject);

        if (collision.gameObject.CompareTag(hitTag))
        {
            if (hitTag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(3);
            }

            Destroy(gameObject);
        }
        
    }

}
