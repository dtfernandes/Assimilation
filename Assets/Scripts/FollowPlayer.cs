using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private CameraMovement _cameraM;
    private GameObject _player;

    [SerializeField]
    private List<GameObject> _rooms;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {          
            _rooms.Add(collision.gameObject);
            _cameraM.ChangeTargetTo(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 )
        {
            _rooms.Remove(collision.gameObject);
            if(_rooms.Count == 1)
            {
                _cameraM.ChangeTargetTo(_rooms[0]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
            transform.position = _player.transform.position;
        else
            _player = GameObject.FindGameObjectWithTag("Player");

    }
}
