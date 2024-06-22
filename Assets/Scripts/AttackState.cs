using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject hitVFX;
    public  GameObject player;
    Animator animator;
    ZombieScript smbs;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 8f;

    NavMeshAgent agent;
    float timePassed;
    float newDestinationCD = 0.5f;

    CapsuleCollider capsuleCollider;
    void Start()
    {
        animator = GetComponent<Animator>();
        smbs = GetComponent<ZombieScript>();
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        if (health > 0)
        {


            animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
            if (timePassed >= attackCD)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    agent.isStopped = true;
                    animator.SetTrigger("attack");
                    timePassed = 0;
                }
                else
                {
                    agent.isStopped = false;
                }
            }
            timePassed += Time.deltaTime;

            if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
            {
                newDestinationCD = 0.5f;
                agent.SetDestination(player.transform.position);
            }
            newDestinationCD -= Time.deltaTime;
            transform.LookAt(player.transform);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die() { 
        smbs.EnableRagdoll();
        capsuleCollider.enabled = false;
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }

    public void StopDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        hit.GetComponent<ParticleSystem>().Play();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
