using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed = 10f;
    //[SerializeField] float speed = 3000f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float aIFiringRateVariance = 0f;
    [SerializeField] float aIMinimumFiringRate = 0.2f;

    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                                transform.position, 
                                                Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * speed;
                //rb.velocity = transform.up * speed * Time.deltaTime;
            }

            Destroy(instance, projectileLifetime);

            float randomFiringRate = Random.Range(baseFiringRate - aIFiringRateVariance,
                                                    baseFiringRate + aIFiringRateVariance);
            randomFiringRate = Mathf.Clamp(randomFiringRate, 
                                            aIMinimumFiringRate,
                                            float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(randomFiringRate);
        }
    }
}
