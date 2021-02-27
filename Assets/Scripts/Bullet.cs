using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Bullet : MonoBehaviour
{

    [SerializeField] float liveTime = 1f;
    float timer;

    public int damage;
    public bool isPiercing;

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

        if (collision.gameObject.CompareTag(hitTag))
        {
            if (hitTag == "Enemy")
            {
                Debug.Log("Enemy Hit + " + collision.gameObject);

                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

                GameManager.GetInstance().hit++;
            }

            if (!isPiercing)
            {
                Destroy(gameObject);
            }
            else
            {
                isPiercing = false;
            }
        }
        
    }

}
