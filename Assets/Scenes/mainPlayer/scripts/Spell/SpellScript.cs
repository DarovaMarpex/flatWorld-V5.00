using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    private Rigidbody2D spellRigidbody;

    [SerializeField]
    private float speed;

    public Transform MyTarget { get; private set; }

    private Transform source;

    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        spellRigidbody = GetComponent<Rigidbody2D>();

        //target = GameObject.Find("Target").transform;
    }

    public void Initialize(Transform target, int damage, Transform source )
    {
        this.MyTarget = target;
        this.damage = damage;
        this.source = source;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector2 direction = MyTarget.position - transform.position;

            spellRigidbody.velocity = direction.normalized * speed;

            float angel = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angel, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hitBox" && collision.transform == MyTarget)
        {
            Character c = collision.GetComponentInParent<Character>();
            speed = 0;
            c.TakeDamage(damage, source);
            GetComponent<Animator>().SetTrigger("impact");
            spellRigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}
