using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private float attackRange;

    public float MyAttackTime { get; set; }

    [SerializeField]
    private float initAgroRange;

    public float MyAgroRange { get; set; }

    public Vector3 MyStartPosition { get; set; }

    [SerializeField]
    private int damage;

    private bool canDoDamage = true;

    public Player player;

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAgroRange;
        }
    }

    public float MyAttackRange
    {
        get
        {
            return attackRange;
        }
        set
        {
            attackRange = value;
        }
    }

    private IState currentState;

    protected void Awake()
    {
        player = GetComponent<Player>();
        MyStartPosition = transform.position;
        MyAgroRange = initAgroRange;
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }

            currentState.Update();
        }

        if(MyTarget != null && !Player.MyInstance.IsAlive)
        {
            ChangeState(new EvadeState());
        }

        base.Update();
    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;

        return base.Select();
    }
    public override void DeSelect()
    {
        healthGroup.alpha = 0;

        base.DeSelect();
    }

    public override void TakeDamage(float damage, Transform source)
    {
        if(!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);

            OnHealthChanged(health.myCurrentValue);
        }
    }

    public void DoDamage()
    {
        if (canDoDamage)
        {
            Player.MyInstance.TakeDamage(damage, transform);
            canDoDamage = false;
        }
    }
    public void canDoDamagee()
    {
        canDoDamage = true;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAgroRange = initAgroRange;
            MyAgroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
        this.MyTarget = null;
        this.MyAgroRange = initAgroRange;
        this.MyHealth.myCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(health.myCurrentValue);
    }

    protected override void Start()
    {
        base.Start();
        MyAnimator.SetFloat("y", -1);
    }
}
