using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySlime : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    private Collider2D _collider;
    private Rigidbody2D _rigid;
   
    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private float _invincibilityTime;
    private bool _inInvincibility;

    private float _invTimeChecker;
    private WaitForSeconds _invTime;

    private enum Direction
    {
        Left,
        Right
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_inInvincibility) return;

        if(collision.gameObject.tag == "Attack")
        {
            Direction dir = 0;
            //Check if attack was from the left/right
            if (collision.gameObject.transform.position.x >
                transform.position.x)
                dir = Direction.Right;
            else
                dir = Direction.Left;

            ReceiveAttack(dir);
        }
    }

    private void ReceiveAttack(Direction dir)
    {
        float xForce = dir == Direction.Right ? -80 : 80;
        Vector2 force = new Vector2(xForce, 100);

        _inInvincibility = true;
        if(_invTimeChecker != _invincibilityTime || _invTime == null)
        {
            _invTimeChecker = _invincibilityTime;
            _invTime = new WaitForSeconds(_invincibilityTime);
        }
        _collider.isTrigger = true;

        //Play animation
        _anim.SetTrigger("Damage");

        health -= 1;
        QuerryDeath();


        StartCoroutine(Invincibility());

        _rigid.AddForce(force);
    }

    private void QuerryDeath()
    {
        if(health <= 0)
        {
            _anim.Play("Death");
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    IEnumerator Invincibility()
    {
        yield return _invTime;
        _collider.isTrigger = false;
        _inInvincibility = false;
    }

    void Awake()
    {  
        health = maxHealth;
        _collider = GetComponent<Collider2D>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
