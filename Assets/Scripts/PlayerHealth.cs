using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public string hitTag;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(hitTag))
        {
            if (hitTag == "Enemy")
            {
                Debug.Log("Ouch! Player got hit!");

                GameManager.GetInstance().miss++;

                Destroy(collision.gameObject);
            }

        }

    }
}
