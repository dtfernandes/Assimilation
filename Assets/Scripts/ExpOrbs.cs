using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbs : MonoBehaviour
{
    private GameObject _player;

    [SerializeField]
    private float _followSpeed;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            _player.transform.position, _followSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.GetComponent<Player>().Exp.Value += 5;
            Destroy(gameObject);
        }
    }
}
