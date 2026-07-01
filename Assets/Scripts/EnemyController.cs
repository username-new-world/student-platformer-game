using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;

    [Header("Stats")]
    [SerializeField] float detectionRange = 4f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] int damage = 10;
    [SerializeField] int maxHealth = 50;

    int currentHealth;
    bool isDead;
    bool canAttack = true;
    bool isFinalLevel = false;

    void Start()
    {
        currentHealth = maxHealth;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if(SceneManager.GetActiveScene().name == "Level_3")
        {
            isFinalLevel = true;
        }
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            Idle();
        }
        else if (distance > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            Attack();
        }

        FacePlayer();

    }

    void Idle()
    {
        animator.SetBool("IsRunning", false);

        agent.isStopped = true;
    }

    void ChasePlayer()
    {
        animator.SetBool("IsRunning", true);

        agent.isStopped = false;

        Vector3 targetPosition = player.position;


        targetPosition.z = transform.position.z;

        agent.SetDestination(targetPosition);
    }

    void Attack()
    {
        animator.SetBool("IsRunning", false);

        agent.isStopped = true;

        if (!canAttack)
            return;

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.3f);

        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            player.GetComponent<PlayerHealth>()
                .TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            transform.rotation =
                Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.rotation =
                Quaternion.Euler(0, -90, 0);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
            
        }
    }

    void Die()
    {

        isDead = true;

        agent.isStopped = true;

        animator.SetTrigger("Die");

        if (isFinalLevel)
            {
                GameManager.Instance.Win();
            }
        
        Destroy(gameObject, 2f);
    }
}