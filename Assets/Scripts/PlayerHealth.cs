using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 3;

    public string hitTag;

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            GameManager.GetInstance().GameOver();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(hitTag))
        {
            if (hitTag == "Enemy")
            {
                Debug.Log("Ouch! Player got hit!");

                playerHealth -= collision.gameObject.GetComponent<Enemy>().damage;

                Destroy(collision.gameObject);
            }

        }

    }
}
