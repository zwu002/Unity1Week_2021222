using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public bool isInitialised = false;

    public Transform firePoint;
    public GameObject bulletNotOnBeatPrefab;
    public GameObject bulletOnBeatPrefab;

    float timePerBeat;
    float timer;

    public float onBeatThresholdIndex;
    float onBeatThreshold;

    public int onBeatMultiplier = 4;
    public bool onBeat = false;


    public float bulletForceNotOnBeat = 20f;
    public float bulletForceOnBeat = 50f;


    void Start()
    {
        timePerBeat = GameManager.GetInstance().timePerBeat;
        timer = Time.time;

        onBeatThreshold = timePerBeat * onBeatThresholdIndex / 2f; 

        isInitialised = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerShoot();
        }
    }

    void FixedUpdate()
    {
        if (!isInitialised && GameManager.GetInstance().isMusicPlaying)
        {
            isInitialised = true;
        }

            if (isInitialised && Time.time - timer + onBeatThreshold >= timePerBeat * onBeatMultiplier)
            {
                timer = Time.time + onBeatThreshold;
                StartCoroutine(TriggerOnBeat());
            }
    }

    void PlayerShoot()
    {
        GameObject bulletPrefab;
        float bulletForce;

        if (onBeat)
        {
            bulletPrefab = bulletOnBeatPrefab;
            bulletForce = bulletForceOnBeat;
        }
        else 
        {
            bulletPrefab = bulletNotOnBeatPrefab;
            bulletForce = bulletForceNotOnBeat;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator TriggerOnBeat()
    {
        onBeat = true;
        Debug.Log("OnBeat!");

        yield return new WaitForSeconds(onBeatThreshold * 2);

        onBeat = false;
    }
}
