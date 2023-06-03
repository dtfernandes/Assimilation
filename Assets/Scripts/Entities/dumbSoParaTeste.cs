using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class dumbSoParaTeste : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        int dir = 0;

        if (Input.GetKey(KeyCode.W))
        {
            dir = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir = 2;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetFloat("Dir", dir);
            _anim.SetTrigger("Attack");      
        }
    }
}
