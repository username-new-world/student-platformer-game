using Unity.VisualScripting;
using UnityEngine;

public class fireball : MonoBehaviour
{

    private Vector3 shootDir; 
    private Vector3 spawnPos;
    private float distance = 5f;
    public void Setup(Vector3 shootDir)
    {
        spawnPos = transform.position;
        this.shootDir = shootDir;
    }
    void Update()
    {
        float moveSpeed = 30f; 
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(spawnPos.x - transform.position.x) > distance)
        {
            Destroy(gameObject);
            // Debug.Log("spawn: " + spawnPos.x + "   current: " + transform.position.x);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
    {
        other.GetComponent<EnemyController>()
             .TakeDamage(20);

        Destroy(gameObject);
    }
    }



}
