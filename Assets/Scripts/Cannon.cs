using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private Transform playerDistance;
    [SerializeField] private float triggerDistance = 10f;
    AudioManager audioManager;

    private float timer;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {

        if (Mathf.Abs(playerDistance.position.x - transform.position.x) <= triggerDistance)
        {
            timer += Time.deltaTime;

            if (timer >= fireRate)
            {
                timer = 0f;

                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                audioManager.playSFX(audioManager.cannonShoot);
            }
        }
        
    }
}