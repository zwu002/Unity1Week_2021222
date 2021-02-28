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

    public string bulletColourTag;
    public bool onBeat;
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

                if (bulletColourTag == collision.gameObject.GetComponent<Enemy>().colourTag)
                {
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

                    if (GameManager.GetInstance().cameraShakeMagnitude < GameManager.GetInstance().cameraShakeMaxMagnitude)
                    {
                        switch (onBeat)
                        {
                            case false:
                                GameManager.GetInstance().cameraShakeMagnitude += 0.1f;
                                break;
                            case true:
                                GameManager.GetInstance().cameraShakeMagnitude += 0.5f;
                                break;
                        }
                        
                    }
                    else
                    {
                        GameManager.GetInstance().cameraShakeMagnitude = GameManager.GetInstance().cameraShakeMaxMagnitude;
                    }
                }            
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
