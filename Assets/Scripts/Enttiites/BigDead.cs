using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDead : MonoBehaviour
{
    [SerializeField]
    private GameObject _dumb;

    public void Dead()
    {
        Destroy(_dumb);
    } 
}
