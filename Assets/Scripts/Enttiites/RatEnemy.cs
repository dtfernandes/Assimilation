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

    protected override void Start()
    {
        base.Start();

        State idle = new State(null,null, Idle);
        State chase = new State(null, null, Chase);

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
        if (inInvincibility) return;

        
    }

    public void Chase()
    {
        if (inInvincibility) return;

        float xDistance = _player.transform.position.x - transform.position.x;

        transform.position += new Vector3(Mathf.Sign(xDistance) * _speed * Time.deltaTime, 0f, 0f);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _view);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
