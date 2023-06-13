using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BirbEnemy : Enemy
{
    private bool _goingLeft;
    private float _left, _right;
    private float _height;

    [SerializeField]
    private float _offset;
    [SerializeField]
    private float _viewRange;

    private WaitForSeconds _surveyAreaTime;
    private bool _survey;

    private Vector2 _targetPosition;

    //Change to a statem machine 
    public System.Action state;

    protected override void Start()
    {
        base.Start();
        _surveyAreaTime = new WaitForSeconds(1);
        _left = roomCollider.bounds.min.x;
        _right = roomCollider.bounds.max.x;
        _height = transform.position.y;
        PrepareIdle();
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
        state?.Invoke();
    }

    public void PrepareIdle()
    {
        _survey = true;
        StartCoroutine(Survay());
        state = Idle;
    }

    public void Idle()
    {
        
        int dir = 0;

        float heightToMove = _height - transform.position.y ;

        if(transform.position.x < _left + _offset)
        {
            _goingLeft = false;
        }
        if (transform.position.x > _right - _offset)
        {
            _goingLeft = true;
        }

        if (_goingLeft)
        {
            dir = -1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            dir = 1;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        transform.position += new Vector3(dir * _speed, heightToMove, 0) * Time.deltaTime;

        
    }

    //Change this to a state machine some day
    public void PrepareAttack()
    {
        _survey = false;
        state = Attack;
    }

    public void Attack()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, _targetPosition, 0.06f);
    
        if(Vector2.Distance(transform.position, _targetPosition)< 0.1f)
        {
            PrepareIdle();
        }
    }

    IEnumerator Survay()
    {
        while (_survey)
        {
            yield return _surveyAreaTime;
           RaycastHit2D[] hits =
                Physics2D.CircleCastAll(transform.position, _viewRange, Vector2.zero);

            if (hits.Any(x => x.collider.gameObject.tag == "Player"))
            {
                _targetPosition = 
                    hits.First(x => x.collider.gameObject.tag == "Player")
                    .collider.gameObject.transform.position;
                PrepareAttack();
            }    
        }
    }


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
