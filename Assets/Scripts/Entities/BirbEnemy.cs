using System;
using System.Collections.Generic;
using UnityEngine;

public class BirbEnemy : Enemy
{
    private StateMachine _stateMachine;

    //Direction the enemy is moving towards
    private int _dir;

    //Player object
    private Player _player;
    private Vector2 _targetPos;

    private float _height;

    [SerializeField]
    private float _offset;

    //Range the enemy can detect things
    [SerializeField]
    private float _viewRange;

    private float _windupTime = 0.5f;
    [SerializeField]
    private float _windupSpeed;

    private bool _didAttack;

    //Change to a statem machine 
    public System.Action state;

    protected override void Start()
    {
        base.Start();

        _dir = 1;

        State idle = new State(null, EndInvincibility, Idle);
        State returnStart = new State( null, null, null);
        State chase = new State(() => { _didAttack = false; }, EndInvincibility, WindUp);
      
        Transition idleToChase = new Transition(idle, chase, EnemySeesPlayer);
        Transition returnStartToIdle = new Transition(returnStart, idle, () => (RewindBirb()));
        Transition chaseTorreturnStart = new Transition(chase, returnStart, () => (_didAttack));

        List<Transition> transitions = new List<Transition> { };

        transitions.Add(idleToChase);
        transitions.Add(returnStartToIdle);
        transitions.Add(chaseTorreturnStart);

        _stateMachine = new StateMachine();
        _stateMachine.Initialize(idle, transitions);

        _height = transform.position.y;
    }

    protected override void Update()
    {      
        if(inInvincibility) return;
        
        if (gameState.IsWorldStopped)
        {
            if(stoppedAux)
            {
                //Do this on the first frame stopped
                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                stoppedAux = false;
            }
            return;
        }
        else
        {
             if(!stoppedAux)
            {
                //Do this on the first frame after regaining movement
                rigid.constraints = initialConstraints;
                stoppedAux = true;
            }
        }

        base.Update();
        _stateMachine.Update();
    }

    #region Idle
    public void Idle()
    {       
        anim.speed = 1;

        if (inInvincibility || gameState.IsWorldStopped
            || inKnobackProtection) return;

        transform.position += new Vector3(_dir
            * _speed / 2 * Time.deltaTime, 0f, 0f);

        //Create ray to know when the birds has something in front of him
        RaycastHit2D hit =
           Physics2D.Raycast(transform.position, new Vector3(1 * _dir, 0f, 0),
           0.6f, LayerMask.GetMask("GRound"), 0);

        if (hit.collider != null)
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

    #endregion

    public bool EnemySeesPlayer()
    {
        //Maybe a bit much
        RaycastHit2D[] hits =
        Physics2D.CircleCastAll(transform.position, _viewRange, new Vector2(0, 0));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider?.gameObject.tag == "Player")
            { 
                _player = hit.collider.gameObject.GetComponent<Player>();
                _targetPos = _player.transform.position;
                return true;
            }
        }

        _player = null;
        return false;
    }

    public bool RewindBirb()
    {
        anim.speed = 1;

        if (inInvincibility || gameState.IsWorldStopped
            || inKnobackProtection) return false;

        Vector2 vectorPos = new Vector2(transform.position.x, _height);

        transform.position =
            Vector3.MoveTowards(transform.position, vectorPos, 0.01f);

        if (Vector2.Distance(transform.position, vectorPos) < 0.1f)
        {
            return true;
        }

        return false;
    }

    #region Attack
    public void Attack()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, _targetPos, 0.03f);    

        if(Vector3.Distance(transform.position, _targetPos) <= 0.01f)
        {
            _didAttack = true;
            _windupTime = 0.5f; // Reset the wait time for the next attack (if needed).
        }
    }

    public void WindUp()
    {
        if (_windupTime <= 0f)
        {
            // Code to execute after the wait (if needed).
            Attack();
        }
        else
        {
            // Calculate the direction from the object to the player
            Vector2 directionToPlayer = _targetPos - (Vector2)transform.position;
            // Calculate the opposite direction
            Vector3 oppositeDirection = -directionToPlayer.normalized;

            // Move the object slightly in the opposite direction
            transform.position += oppositeDirection * (_windupSpeed * Time.deltaTime);

            _windupTime -= Time.deltaTime;
        }
    }

    #endregion

    protected override void EndInvincibility()
    {
        base.EndInvincibility();
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
    }

    #region Editor
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRange);
    }

    #endregion
}
