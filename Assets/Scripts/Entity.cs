﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity: MonoBehaviour 
{
    protected bool inInvincibility;

    [SerializeField]
    private float _invincibilityTime;

    private float _invTimeChecker;
    private WaitForSeconds _invTime;

    [SerializeField]
    protected int maxHP;

    protected int hp;
    protected int atk;

    protected Rigidbody2D rigid;


    protected enum Direction
    {
        Left,
        Right
    }

    protected virtual void Awake()
    {
        hp = maxHP;
        rigid = GetComponent<Rigidbody2D>();
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

        DamageReaction();

        StartCoroutine(Invincibility());

        rigid.velocity = new Vector2(0,0);
        rigid.AddForce(force);

        //Do Damage
        hp -= damage;
        if(hp <= 0)
        {
            Death();
        }
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