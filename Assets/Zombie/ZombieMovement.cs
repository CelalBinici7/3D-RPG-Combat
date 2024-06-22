using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class ZombieMovement : MonoBehaviour
{
    /* 1-- Zombi karakter rastgele hareket edicek
     * 2-- araba veya karater yaklaþýnc arba veya karaketre doðru hareket edicek
     * 3-- yaklaþtýðýnda belli bir mesaefede vurma animasyonu devreye girecek ve can azaltýcak;
     * 
     */
    public float Health;
    Rigidbody rb;
    bool isWalking;
    bool isFollowing;
    bool isAttacing;
    bool isReturn;
    public Transform pos;
    public float speed;
    public Animator anim;
    public float desiredSpeed;
    public GameObject Target;
    public NavMeshAgent agent;
    [Header("Patrol")]
    public List<GameObject> patrolPoints;
    public int numberOfPoints = 10;
    public float minRadius = 10f;
    public float maxRadius = 20f;
    Vector3 posPatrol;
    [Header("Radius")]
    public float walkingDistance;
    public float followingDistance;
    public float attackDistance;
    public enum ZombieState
    {
        walking,
        follow,
        attack,
        isreturn

    }
    ZombieState state;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        posPatrol = createPatrolPoint();
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
       // walkFunction();
        followFunction();
        attackFunction();
      //  print(Vector3.Distance(transform.position, Target.transform.position));
    }

    public void stateHandler()
    {
        if (isWalking)
        {
            state = ZombieState.walking;
        }else if (isFollowing)
        {
            state = ZombieState.follow;
        }else if(isAttacing)
        {
            state = ZombieState.attack;
        }else if(isReturn)
        {
            state = ZombieState.isreturn;
        }
    }

    public void walkFunction()
    {
        if (!isFollowing && !isAttacing)
        {
            Vector3 direction = pos.position - transform.position;

            if (direction.magnitude > 0.5f)
            {
                agent.SetDestination(patrolPoints[0].transform.position);
                anim.SetBool("walk", true);

                transform.rotation = Quaternion.LookRotation(direction.normalized);
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }
       
      
    }
    public void followFunction()
    {
        if (!isAttacing)
        {
            Collider[] hitcolliders = Physics.OverlapSphere(transform.position, followingDistance);
            print("asa");

            foreach (var objects in hitcolliders)
            {

                if (objects.gameObject.CompareTag("Player") || objects.gameObject.CompareTag("car"))
                {
                    agent.isStopped = false;
                    isFollowing = true;
                    anim.SetBool("run", true);
                    anim.SetBool("walk", false);
                    agent.SetDestination(Target.transform.position);
                    transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);


                }
                else if (Vector3.Distance(transform.position, Target.transform.position) > 20f)
                {
                    agent.isStopped = false;
                 
                    returnFunction();


                }
            }

        }
      
    }
    public void attackFunction()
    {
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, attackDistance);


        foreach (var objects in hitcolliders)
        {

            if ( objects.gameObject.CompareTag("car"))
            {
                if (objects.GetComponentInParent<Rigidbody>().velocity.magnitude<7f)
                {
                    anim.SetTrigger("attackZomb2");
                  //  anim.SetBool("run", false);
                  //  anim.SetBool("walk", false);
                   
                    agent.isStopped = true;
                    isAttacing = true;
                }
              
              //  transform.rotation = Quaternion.LookRotation(Target.transform.position);

             
            }else if (objects.gameObject.CompareTag("Player"))
            {
                
                    anim.SetTrigger("attackZomb");
                 //   anim.SetBool("run", false);
                  //  anim.SetBool("walk", false);
                  
                    agent.isStopped = true;
                    isAttacing = true;
                
            }
            else if (Vector3.Distance(transform.position, Target.transform.position) > 3f)          
            {
                //agent.isStopped = false;
                isAttacing=false;
            }
          
        }
    }
    public void PatrolFunctions()
    {
      
       
       
    }
    public void returnFunction()
    {
        if (!isAttacing)
        {
            agent.SetDestination(posPatrol);
            anim.SetBool("run", false);
            anim.SetBool("walk", true);
            if (Vector3.Distance(transform.position, posPatrol) < 0.5f)
            {
                posPatrol = createPatrolPoint();
                anim.SetBool("run", false);
                anim.SetBool("walk", true);
                agent.SetDestination(posPatrol);
            }
        }
       
       
    }
    public Vector3 createPatrolPoint()
    {
        float radius = Random.Range(minRadius, maxRadius);
        float angle = Random.Range(0f, 360f);

        Vector3 point = transform.position + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * radius;
        
        return point;
    }

    private void OnDrawGizmos()
    {
          Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,followingDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,walkingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackDistance);

    }
}
