using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : Setupable
{
    [SerializeField]
    private GameObject _heartPREFAB, _heartEmptyPREFAB;

    [SerializeField]
    private ScriptableInt _health;
    [SerializeField]
    private ScriptableInt _maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        Setup();

        _health.OnChange += Setup;
    }

    public override void Setup()
    {
        if(_maxHealth.Value < _health.Value)
        {
            _health.Value = _maxHealth.Value;
        }

        //Clear every heart
        foreach(Transform t in transform)
        {
            Destroy(t?.gameObject);
        }

        for (int i = 0; i < _health.Value; i++)
        {
            Instantiate(_heartPREFAB,
                transform.position, Quaternion.identity, transform);
        }

        for (int i = 0; i < _maxHealth.Value - _health.Value; i++)
        {
            Instantiate(_heartEmptyPREFAB,
                transform.position, Quaternion.identity, transform);
        }

     
    }
}
