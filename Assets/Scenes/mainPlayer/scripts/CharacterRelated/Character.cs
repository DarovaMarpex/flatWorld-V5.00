using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour //this class i need to collect functions for player and all other characters 
{
    [SerializeField]
    private float speed;
    private Vector2 direction;

    public Animator MyAnimator { set; get; }

    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float initHealt;

    [SerializeField]
    public stat health;

    public Transform MyTarget { get; set; }

    public stat MyHealth
    {
        get
        {
            return health;
        }
    }

    public bool IsAttacking { get; set; }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;
    public bool isMoving
    {
        get
        {
            return Direction.x != 0 || Direction.y != 0;
        }
    }

    public Vector2 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }

    public bool IsAlive
    {
        get
        {
            return health.myCurrentValue > 0;
        }
    }

    protected virtual void Start()
    {

        health.initialize(initHealt, initHealt);
        //reference to smth
        MyAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        playerMove();
    }
    public void playerMove()
    {
        if (IsAlive)
        {
            //move this game object and use direction speed and frames
            myRigidBody.velocity = Speed * Direction.normalized;
        }
    }

    public void HandleLayers()
    {

        if (IsAlive)
        {
            //condition to use right animation (walk animation)
            if (isMoving)
            {
                layerActivator("Walk_Layer");
                //combaine player x adn y and animator x and y
                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);
            }
            else if (IsAttacking)
            {
                layerActivator("Attack_Layer");
                //Debug.Log("allWorks");
            }
            else
            {
                //to use the right animation layer
                layerActivator("Idle_Layer");
            }
        }
        else
        {
            layerActivator("Death_Layer");
        }
    }
    //helps to activate layers using only layers names
    public void layerActivator(string layerName)
    {
        for(int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        //i can only write the layer name and this will be same like  animator.SsetLayerWeight(1, 0);
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }
  
    public virtual void TakeDamage(float damage, Transform source)
    {
        

        health.myCurrentValue -= damage;

        if (health.myCurrentValue <= 0 )
        {
            Direction = Vector2.zero;
            myRigidBody.velocity = direction;
            MyAnimator.SetTrigger("die");
            
        }
    }
}
