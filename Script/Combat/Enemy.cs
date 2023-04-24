using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHealth;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float towerAttackRange = 5f;
    [SerializeField] float attackRange = 2.5f;
    [SerializeField] float sightRange = 50f;
    [SerializeField] float dmgToDeal = 1f;
    [SerializeField] float entryTime = 2f;

    [SerializeField] GameObject dmgPopUp;
    [SerializeField] GameObject healPopUp;
    [Space]
    [SerializeField] AudioTrigger entry;
    [SerializeField] AudioTrigger attack;
    [SerializeField] AudioTrigger death;

    Collider col;
    Animator anim;
    Transform target;
    NavMeshAgent agent;

    bool alreadyAttacked, playerInSightRange, playerInAttackRange, isRunning, isDead;

    int randomNumber;

    public float timer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();

        timer = entryTime;

        randomNumber = Random.Range(0, 100);

        if (randomNumber > 50) target = GameObject.Find("Turm").GetComponent<Tower>().hitPlace;
        else target = GameObject.Find("Player").GetComponent<PlayerMovement>().hitPlace;
    }

    private void Start()
    {
        entry.Play(transform.position);
    }

    private void Update()
    {
        if(timer > -1) timer -= Time.deltaTime;
        if (timer > 0) return;

        UpdateAnimation();

        if(maxHealth <= 0)
        {
            agent.SetDestination(transform.position);
            Die();
            return;
        }

        //Check for sight and attack range
        playerInSightRange = Vector3.Distance(transform.position, target.position) < sightRange;
        playerInAttackRange = Vector3.Distance(transform.position, target.position) < (randomNumber > 50 ? towerAttackRange : attackRange);

        if (!playerInSightRange && !playerInAttackRange) Idle();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Idle()
    {
        isRunning = false;
    }

    private void ChasePlayer()
    {
        isRunning = true;
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        isRunning = false;
        agent.SetDestination(transform.position);

        transform.LookAt(target);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);


        if (!alreadyAttacked)
        {
            attack.Play(transform.position);
            anim.SetTrigger("Attack");
            IDamagable dealDmg = target.transform.GetComponentInParent<IDamagable>();
            dealDmg.ApplyDamage(dmgToDeal);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void UpdateAnimation()
    {
        anim.SetBool("isRunning", isRunning);
    }
    
    public void ApplyDamage(float dmg)
    {
        if (timer >= 0.01f) return;
        maxHealth = Mathf.Max(maxHealth - dmg, 0);
    }

    void Die()
    {
        if (isDead) return;
        isRunning = false;
        col.enabled = false;
        death.Play(transform.position);
        anim.Play("Die");

        if (randomNumber <= 30 && randomNumber > 10) Instantiate(healPopUp, transform.position, Quaternion.identity);
        else if(randomNumber <= 10) Instantiate(dmgPopUp, transform.position, Quaternion.identity);

        Destroy(this.gameObject, 5f);

        isDead = true;
    }


}
