using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    private bool _unlockHorizontal;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _unlockHorizontal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       

        if (_target == null) return;
        if(Vector2.Distance(transform.position, _target.transform.position) > 0.1f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _target.transform.position + new Vector3(0, 0, -10), 0.5f) ;
            //
        }
        else
        {
            transform.position = _target.transform.position + new Vector3(0, 0, -10);
        }

        if (_unlockHorizontal)
        {
            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player");
            }
            else 
            {
                transform.position = 
                    new Vector3(_player.transform.position.x, transform.position.y, -10);
            }
        }
    }

    public void ChangeTargetTo(GameObject target, bool force = false)
    {
        _target = target;

        if (force)
        {
            transform.position = target.transform.position;
        }
    }
}
