using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Enemy: Entity
{
    protected Collider2D coll;
    
    protected GameObject room;
    protected Collider2D roomCollider;

    [SerializeField]
    protected int _maxHp;

    [SerializeField]
    protected GameObject expOrbPREFAB;

    [field:SerializeField]
    public int DangerLevel { get; private set; }


    protected override void Awake()
    { 
        base.Awake();
        hp = _maxHp + gameValues.E_MaxHealth.Value;
        coll = GetComponent<Collider2D>();
    }

    protected virtual void Update() 
    {
     
    }

    protected virtual void Start() 
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position,
             0.1f, new Vector2(0, 0), 0.1f, 1 << 7);

        roomCollider = hit.collider;
        room = hit.collider.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (inInvincibility) return;

        if (collision.gameObject.tag == "Player")
        {
            AttackPlayer(collision.gameObject.GetComponent<Entity>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inInvincibility) return;

        if (collision.gameObject.tag == "Attack")
        {
            rigid.gravityScale = 1;
            RecieveDamage(1, collision.gameObject);
        }
    }

    protected virtual void AttackPlayer(Entity player)  
    {
        player.RecieveDamage(1,this.gameObject);
    }


    protected override void Death()
    {
        Destroy(gameObject);
        int rnd = Random.Range(4,8);

        for (int i = 0; i < rnd; i++)
        {
            GameObject o = Instantiate(expOrbPREFAB, transform.position, Quaternion.identity);
            Rigidbody2D r = o.GetComponent<Rigidbody2D>();
            r.AddForce(new Vector2(Random.Range(-100,100),200));
        }
    }
}
