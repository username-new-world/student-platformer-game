using System;
using Unity.VisualScripting;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    [SerializeField] bool isTriggered = true;
    [SerializeField] float speed = 4f;
    [SerializeField] float rotationSpeed = 4f;
    [SerializeField] int damage = 10;
    [SerializeField] private Transform playerDistance;
    [SerializeField] private float triggerDistance = 10f;
    private bool movingRight;
    void Start()
    {
        
    }

    void Update()
    {
        if (Mathf.Abs(playerDistance.position.x - transform.position.x) > triggerDistance)   
            return;

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        

        if (transform.localPosition.x >= 1.24f)
            movingRight = false;

        if (transform.localPosition.x <= -1.24f)
            movingRight = true;

        if (movingRight)
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        else
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealt = other.GetComponent<PlayerHealth>();

        if(playerHealt != null)
        {
            Debug.Log("hey");
            playerHealt.TakeDamage(damage);
        }
    }
}
