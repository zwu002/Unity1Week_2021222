using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerShooting : MonoBehaviour
{
    public bool isInitialised = false;

    public Transform firePoint;

    public GameObject bulletNotOnBeatPrefabRed;
    public GameObject bulletOnBeatPrefabRed;
    public GameObject bulletNotOnBeatPrefabOrange;
    public GameObject bulletOnBeatPrefabOrange;

    public GameObject beatIndicator;

    [SerializeField] float timePerBeat;
    float timer;

    public float onBeatThresholdIndex;
    float onBeatThreshold;

    public int onBeatMultiplier = 4;
    public bool onBeat = false;


    public float bulletForceNotOnBeat = 20f;
    public float bulletForceOnBeat = 50f;



    void Start()
    {
        isInitialised = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerShootRed();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            PlayerShootOrange();
        }
    }

    void FixedUpdate()
    {
        if (!isInitialised && GameManager.GetInstance().isMusicPlaying)
        {
            isInitialised = true;

            timer = Time.time + onBeatThreshold;

            timePerBeat = GameManager.GetInstance().timePerBeat;

            onBeatThreshold = timePerBeat * onBeatThresholdIndex / 2f;
        }

            if (isInitialised && Time.time - timer + onBeatThreshold >= timePerBeat * onBeatMultiplier)
            {
                timer = Time.time + onBeatThreshold;
                StartCoroutine(TriggerOnBeat());
            }
    }

    void PlayerShootRed()
    {
        GameObject bulletPrefab;
        float bulletForce;

        if (onBeat)
        {
            bulletPrefab = bulletOnBeatPrefabRed;
            bulletForce = bulletForceOnBeat;
        }
        else 
        {
            bulletPrefab = bulletNotOnBeatPrefabRed;
            bulletForce = bulletForceNotOnBeat;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void PlayerShootOrange()
    {
        GameObject bulletPrefab;
        float bulletForce;

        if (onBeat)
        {
            bulletPrefab = bulletOnBeatPrefabOrange;
            bulletForce = bulletForceOnBeat;
        }
        else
        {
            bulletPrefab = bulletNotOnBeatPrefabOrange;
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

        beatIndicator.GetComponent<SpriteRenderer>().enabled = true;

        if (GameManager.GetInstance().cameraShakeMagnitude > 0)
        {
            GameManager.GetInstance().cameraShakeMagnitude -= 0.25f;
        }
        else
        {
            GameManager.GetInstance().cameraShakeMagnitude = 0;
        }

        CameraShaker.Instance.ShakeOnce(GameManager.GetInstance().cameraShakeMagnitude, 3f, onBeatThreshold, 0.05f);

        yield return new WaitForSeconds(onBeatThreshold * 2);

        onBeat = false;
        beatIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }
}
