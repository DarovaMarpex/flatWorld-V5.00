using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }
    //SerializedField needs to display stat in unity
    [SerializeField]
    private stat mana;

    private spellBook spellBook;

    [SerializeField]
    private float initMana = 50;

    [SerializeField]
    public int damage;

    private Transform source;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    private bool canAttack = false;

    private Vector3 min, max;


    [SerializeField]
    GameObject KeyBindObj;
    KeybindManager keyBinds;


	public void Awake()
	{
		
	}
	//protected override needs to use or modify void Start from the Character 
	protected override void Start()
    {
        spellBook = GetComponent<spellBook>();

		keyBinds = GameObject.FindGameObjectWithTag("kb").GetComponent<KeybindManager>();
		//keyBinds = KeyBindObj.GetComponent<KeybindManager>();

        //we use function initialize wich gives some stats to player at game start
        mana.initialize(initMana, initMana);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
       // call the functions that i added before
        getInput();
        if (canAttack == true)
        {
            ChanceToAttack();

        }

        //Debug.Log(canAttack);
        //Debug.Log(IsAttacking);

        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
        //will kil the update in character
        base.Update();
        
       //InLineOfSight();
    }
    
    //Set the directions by usig asdw buttons
    public void getInput()
    {
        ///test
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.myCurrentValue += 50;
            mana.myCurrentValue += 25;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.myCurrentValue -= 50;
            mana.myCurrentValue -= 25;
        }


        Direction = Vector2.zero;

        if (Input.GetKey(keyBinds.Keybinds["UP"]))
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }
        if (Input.GetKey(keyBinds.Keybinds["DOWN"]))
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }
        if (Input.GetKey(keyBinds.Keybinds["LEFT"]))
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }
        if (Input.GetKey(keyBinds.Keybinds["RIGHT"]))
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }
        if (Input.GetKey(keyBinds.Keybinds["SHIFT"]))
        {
            Speed = 7;
        }
        if (Input.GetKeyUp(keyBinds.Keybinds["SHIFT"]))
        {
            Speed = 3;
        }
        if (isMoving)
        {
            StopAttacking();
        }

        foreach (string action in keyBinds.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(keyBinds.ActionBinds[action]))
            {
                UiManager.MyInstance.ClickActionButton(action);
            }
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }
    //IEnumerator useed to use yield return
    private IEnumerator Attack(string spellNmae)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellNmae);
       
        IsAttacking = true;

        MyAnimator.SetBool("attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLineOfSight())
        {
            //we using our spell inside the array ang transform his position, with  Quaternion.identity it will be send in same direction player is looking
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }

        StopAttacking();

    }
    public void castSpell(string spellName) 
    {
        Block();
        //stop casting attack if ou moving or already attacking
        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !isMoving && InLineOfSight())
        {
            //coroutine its smth u can activate in same time while the rest of the script runs
            attackRoutine = StartCoroutine(Attack(spellName));
            //isMoving = false;
        }
    }
    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            //we input cast from, direction of the hit and distance of the hit(distance between player and target) 8 at the end is the number og layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        }
        return false;
    }
    private void Block()
    {
        // foreach we are looking for class Block inside the array block and we reference that block with b as the variable
        foreach (Block b in blocks )
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

    public void StopAttacking()
    {
        spellBook.StopCasting();
        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
    private void ChanceToAttack()
    {
        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !isMoving && InLineOfSight())
        {
            this.StartCoroutine(AttackEnm());
            //Debug.Log("faound a target");
        }
    }
    public IEnumerator AttackEnm()
    {
        //Debug.Log("attacking the target");
        this.IsAttacking = true;

        Debug.Log(IsAttacking);
        Debug.Log(canAttack);

        this.MyAnimator.SetTrigger("autoAttack");
        Debug.Log("im attacking");

        yield return new WaitForSeconds(this.MyAnimator.GetCurrentAnimatorStateInfo(2).length);

        this.IsAttacking = false;

    }
    private void hitEnemy()
    {
        //Transform currentTarget = MyTarget;
        //SpellScript z = GetComponent<SpellScript>();
        //z.Initialize(currentTarget, damage, transform);

        Character c = MyTarget.GetComponentInParent<Character>();
        c.TakeDamage(damage, source);

    }
}
