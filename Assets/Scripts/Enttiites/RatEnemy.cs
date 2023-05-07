using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : Enemy
{
    private StateMachine _stateMachine;

    [Header("Rat Specific")]
    [SerializeField]
    private float _view;
    [SerializeField]
    private float _attackRadius;

    private Player _player;

    private int _dir;

    protected override void Start()
    {
        base.Start();

        _dir = 1;

        State idle = new State(null, RemoveInvisibility, Idle);
        State chase = new State(null, RemoveInvisibility, Chase);

        Transition idleToChase = new Transition(idle,chase, EnemySeesPlayer);
        Transition chaseToIdle = new Transition(chase, idle, () => (!EnemySeesPlayer()));
        List<Transition> transitions = new List<Transition> { };

        transitions.Add(idleToChase);
        transitions.Add(chaseToIdle);


        _stateMachine = new StateMachine();
        _stateMachine.Initialize(idle, transitions);
    }

    protected override void Update()
    {
        base.Update();
        _stateMachine.Update();
    }


    public bool EnemySeesPlayer()
    {
        //Maybe a bit much
        RaycastHit2D[] hits =
        Physics2D.CircleCastAll(transform.position, _view, new Vector2(0, 0));

        

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider?.gameObject.tag == "Player")
            {
                
                _player = hit.collider.gameObject.GetComponent<Player>();
                return true;
            }
        }

       
        _player = null;
        return false;
    }

    public void Idle()
    {
        anim.Play("RatWalk");
        anim.speed = 1;

        if (inInvincibility || gameState.IsWorldStopped 
            || inKnobackProtection) return;

        transform.position += new Vector3(_dir
            * _speed/2 * Time.deltaTime, 0f, 0f);

        //Create ray        
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position + new Vector3(0.5f * _dir,0,0), new Vector3(0, -1f, 0), 0.6f, 
            (LayerMask.GetMask("GRound")), 0);

        if (hit.collider == null)
        {
            //Change direction
            ChangeDirection();
        }
    }


    private void ChangeDirection()
    {
        _dir *= -1;
        int angle = _dir == 1 ? 180 : 0;
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void Chase()
    {

        if (inInvincibility || gameState.IsWorldStopped
            || inKnobackProtection) return;

        anim.Play("RatWalk");
        anim.speed = 2;

        float xDistance = _player.transform.position.x - transform.position.x;

        
        if(xDistance * _dir < 0)
        {
            ChangeDirection();
        }

        transform.position += new Vector3(Mathf.Sign(xDistance) * _speed * Time.deltaTime, 0f, 0f);

        if (Mathf.Abs(xDistance) < _attackRadius)
        {
            inKnobackProtection = true;
            //Change Animation
            anim.Play("RatAttack");
            anim.speed = 1;
        }
    }

    public void SlashAttack()
    {
        rigid.AddForce(new Vector2(150 * _dir, 120));
    }

    public void RemoveInvisibility()
    {
        inKnobackProtection = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _view);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + new Vector3(0.5f * _dir, 0, 0), new Vector3(0, -0.6f, 0));
    }
}
