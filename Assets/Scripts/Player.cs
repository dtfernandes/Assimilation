using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    [SerializeField]
    private float _speed;
    private float _dumbtimer;

    [SerializeField]
    private int _maxJumps;
    private int _jumps;

    [field:SerializeField]
    public ScriptableInt Exp { get; private set; }
    private Rigidbody2D _rb;
 

    protected override void Awake()
    {
        base.Awake();
        _jumps = _maxJumps;
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (inInvincibility) return;

        //Reset horizontal velocity
        _rb.velocity = new Vector2(0, _rb.velocity.y);

        float xMove = 0;

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //Simple Rotation 
            if (Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            xMove = Input.GetAxisRaw("Horizontal");
        }

      

        float xProper = xMove * Time.deltaTime * _speed * 1000;
        _rb.AddForce(new Vector2(xProper, 0));        
    }

    public void GainExp(int v)
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        if (inInvincibility) return;

        if (_dumbtimer > 0)
            _dumbtimer -= 0.01f;
        else { _dumbtimer = 0; }

        //Create ray        
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, new Vector3(0, -1f, 0), 0.6f, (LayerMask.GetMask("GRound")), 0);

        if (hit.collider != null && _dumbtimer == 0)
        {
            _jumps = _maxJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_jumps > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _dumbtimer = 0.1f;
                _rb.AddForce(new Vector2(0, 300));
                _jumps -= 1;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector3(0, -0.6f, 0));
    }

    protected override void DamageReaction()
    {
        Camera.main.GetComponent<ScreenShake>().Shake();
    }

    protected override void Death()
    {
        
    }
}
