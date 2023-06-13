using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity: MonoBehaviour 
{
    protected GameValues gameValues; 
    protected GameState gameState;

    protected bool inInvincibility;
    protected bool inKnobackProtection;

    [SerializeField]
    private float _invincibilityTime;

    private float _invTimeChecker;
    private WaitForSeconds _invTime;

    protected int hp;
    protected int atk;

    protected bool stoppedAux;
    protected RigidbodyConstraints2D initialConstraints;

    protected Rigidbody2D rigid;
    [SerializeField]
    protected Animator anim;


    protected enum Direction
    {
        Left,
        Right
    }

    protected virtual void Awake()
    {       
        gameState = GameState.Instance;
        gameValues = gameState.GameValues;
        rigid = GetComponent<Rigidbody2D>();

        initialConstraints = rigid.constraints;
    }

    public void RecieveDamage(int damage, GameObject attacker)
    {

        //Calculate Position
        Direction dir = 0;

        //Check if attack was from the left/right
        if (attacker.transform.position.x >
            transform.position.x)
            dir = Direction.Right;
        else
            dir = Direction.Left;

        //React
        float xForce = dir == Direction.Right ? -80 : 80;
        Vector2 force = new Vector2(xForce, 100);

        inInvincibility = true;
        if (_invTimeChecker != _invincibilityTime || _invTime == null)
        {
            _invTimeChecker = _invincibilityTime;
            _invTime = new WaitForSeconds(_invincibilityTime);
        }

        StartCoroutine(Invincibility());

        rigid.velocity = new Vector2(0, 0);
        rigid.AddForce(force);
        
        //Do Damage
        hp -= damage;
        if(hp <= 0)
        {
            Death();
        }

        DamageReaction();

    }

    IEnumerator Invincibility()
    {
        yield return _invTime;
        inInvincibility = false;
        EndInvincibility();
    }

    protected virtual void EndInvincibility() { }
    protected virtual void DamageReaction() { }
    protected abstract void Death();
}