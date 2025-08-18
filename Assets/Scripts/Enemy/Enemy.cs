using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour, IDamageable, IRootMotionParent, IPoolable
{
    [SerializeField] int MaxHealth = 40;
    [SerializeField] float AttackRange = 2f;
    [SerializeField] float AttackRate = 2f;
    [SerializeField] int damage = 2;
    [SerializeField] float disapearTime = 4f;
    public int health { get; private set; }
    public EnemyTarget target;
    public NavMeshAgent navMeshAgent { get; private set; }
    private Animator animator;
    private Vector3 rootMotionDelta;
    private CharacterController characterController;
    private float nextAttackTime = 0f;
    private ObjectPool<Enemy> pool;


    public EnemyState State { get; private set; }
    public enum EnemyState
    {
        idle,
        Move,
        Attack,
        Die
    }


    public void TakeDamage(int amount)
    {
        if (State == EnemyState.Die) return;
        health -= amount;
        if (health <= 0) EnterDieState();
    }


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        navMeshAgent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case EnemyState.idle:
                HandleIdle();
                break;
            case EnemyState.Move:
                HandleMove();
                break;
            case EnemyState.Die:
                HandleDie();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
        }
    }

    void HandleIdle()
    {
        CheckDestination();
    }

    void EnterMoveState()
    {
        animator.SetFloat("Speed", 1f);
        State = EnemyState.Move;
    }

    void HandleMove()
    {
        CheckDestination();
        if (rootMotionDelta != Vector3.zero) characterController.Move(rootMotionDelta);
    }

    void EnterDieState()
    {
        State = EnemyState.Die;
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Die");
        StartCoroutine(CLearCorpse());
        characterController.enabled = false;
        navMeshAgent.enabled = false;
    }

    void HandleDie()
    {

    }

    void EnterAttackState()
    {
        State = EnemyState.Attack;
        animator.SetFloat("Speed", 0f);
    }

    void HandleAttack()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + AttackRate;
        animator.SetTrigger("Attack");
        target.TakeDamage(damage);
    }

    void CheckDestination()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < AttackRange)
        {
            EnterAttackState();
        }
        else
        {
            EnterMoveState();
        }
    }

    public void UpdateRootMotionDelta(Vector3 delta)
    {
        rootMotionDelta = delta;
    }

    IEnumerator CLearCorpse()
    {
        yield return new WaitForSeconds(disapearTime);
        OnRecycleToPool();
    }

    public void OnSpawnFromPool()
    {
        characterController.enabled = true;
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(target.transform.position);
        health = MaxHealth;
        navMeshAgent.updatePosition = false;
        State = EnemyState.idle;
    }

    public void OnRecycleToPool()
    {
        characterController.enabled = false;
        navMeshAgent.enabled = false;
        pool.Release(this);
    }
    
    public void Init(ObjectPool<Enemy> pool)
    {
        this.pool = pool;
    }
}
