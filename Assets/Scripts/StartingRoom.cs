using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartingRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPREFAB;
    [SerializeField]
    private GameObject _playerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_playerPREFAB, _playerSpawn.transform.position, Quaternion.identity);
        FindObjectOfType<CameraMovement>().ChangeTargetTo(this.gameObject, true);
    
        
    }
    
    

 
}
