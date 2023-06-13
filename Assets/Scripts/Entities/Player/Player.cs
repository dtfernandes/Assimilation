using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : Entity
{
    private float _dumbtimer;

    [SerializeField]
    private float _jumpForce;

    private int _jumps;

    private bool _onAttack;
    private bool _onGround;

    private int _dir;

    [field: SerializeField]
    public ScriptableInt Exp { get; private set; }

    [field: SerializeField]
    private ScriptableInt _health;

    private Rigidbody2D _rb;

    [Header("MovementDust")]
    [SerializeField] private GameObject _jumpDustPREFAB;
    [SerializeField] private Vector3 _jumpDustOffset;
    [SerializeField] private GameObject _moveDusrPREFAB;

    protected override void Awake()
    {
        base.Awake();

        _health.OnChange -= SyncHealth;
        _health.OnChange += SyncHealth;

        hp = _health.Value;
        _health.Value = hp;

        _jumps = gameValues.P_MaxJumps.Value;
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    public void SyncHealth()
    {
        hp = _health.Value;
    }

    void FixedUpdate()
    {
        if (inInvincibility || gameState.IsWorldStopped || _onAttack) return;

        //Reset horizontal velocity
        _rb.velocity = new Vector2(0, _rb.velocity.y);

        float xMove = 0;
        anim.SetBool("isMove", false);

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            anim.SetBool("isMove", true);

            //Simple Rotation 
            if (Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                anim.SetFloat("dir", 1);
                 _dir = 1;
                transform.eulerAngles = new Vector3(0,0,0);
            }
            else
            {
                 _dir = 0;
                anim.SetFloat("dir", 0);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            xMove = Input.GetAxisRaw("Horizontal");
        }     

        float xProper = xMove * Time.deltaTime * gameValues.P_Speed.Value * 1000;
        _rb.AddForce(new Vector2(xProper, 0));        
    }

    public void Update()
    {
        if (inInvincibility || gameState.IsWorldStopped) return;

        if (_dumbtimer > 0)
            _dumbtimer -= 0.01f;
        else { _dumbtimer = 0; }

        //Create ray        
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, new Vector3(0, -1f, 0), 0.6f, (LayerMask.GetMask("GRound")), 0);


        if(hit.collider == null && _onGround && !_onAttack){
             //Player is in the air
            
            _onGround = false;
        }
        else if (hit.collider != null && _dumbtimer == 0)
        {       
            _jumps = gameValues.P_MaxJumps.Value;
            _onGround = true;
        }
        
        anim.SetBool("onGround", !_onGround);


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {

            
            if (_jumps > 0)
            {
                EndAttackAnimation();
                Jump();
                _jumps -= 1;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {   
            
            bool down = false;

             if(Input.GetAxisRaw("Horizontal") >= 0.5f)
            {
                _dir = 1;
                anim.SetFloat("dir", 1);
            }
            if(Input.GetAxisRaw("Horizontal") <= -0.5f)
            {
                _dir = 0;
                anim.SetFloat("dir", 0);
            }

            if(Input.GetAxisRaw("Vertical") >= 0.5f)
            {
                _dir = 2;
                anim.SetFloat("dir", 2);
            }
            if(Input.GetAxisRaw("Vertical") <= -0.5f)
            {
                _dir = 3;
                anim.SetFloat("dir", 3);
                down = true;
            }

            if(!down || !_onGround)
            {

                if(_onGround)
                {
                    _rb.velocity = new Vector2(0, _rb.velocity.y);
                }

                _onGround = true;
                anim.SetBool("onGround", true);

                anim.SetTrigger("attack");
                _onAttack = true;
            }

        }

    }


    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _dumbtimer = 0.1f;
        _rb.AddForce(new Vector2(0, _jumpForce));
        Instantiate(_jumpDustPREFAB, transform.position + _jumpDustOffset, Quaternion.identity);
       
    }

    public void EndAttackAnimation()
    {

        _onGround = false;
        _onAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector3(0, -0.6f, 0));
        Gizmos.DrawWireSphere(transform.position + _jumpDustOffset, 0.3f);
    }

    protected override void DamageReaction()
    {
        _health.Value = hp;
        Camera.main.GetComponent<ScreenShake>().Shake();
    }

    protected override void Death()
    {
        UnityEngine.SceneManagement
            .SceneManager.LoadScene("GameLose");
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(!_onAttack) return;
        if(coll.gameObject.GetComponent<Enemy>() == null) return;
            
        if(_dir == 3)
            Jump();
        
    }

}
