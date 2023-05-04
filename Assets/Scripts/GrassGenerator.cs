using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GrassGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _grass;

    private Collider2D _collider;

    private Vector2 _left, _right;
    private Vector2 _currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _left = _collider.bounds.min;
        _right = _collider.bounds.max;
        _currentPosition = _left;
        Populate();
    }

    public void Populate()
    {
        int rnd = Random.Range(0, _grass.Length);
        GameObject g = Instantiate(_grass[rnd], _currentPosition, Quaternion.identity);
        g.transform.SetParent(transform);

        Collider2D c = g.GetComponent<Collider2D>();
        g.transform.position = new Vector2(g.transform.position.x, _collider.bounds.min.y 
            + c.bounds.extents.y);


        float value = Random.Range(0.1f,1.5f);
        _currentPosition += new Vector2(value,0);

        if(_currentPosition.x < _right.x)
        {
            Populate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
