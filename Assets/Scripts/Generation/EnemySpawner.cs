using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private List<Enemy> _enemiesPREBAS;

    private Enemy _selectedEnemy;

    public Enemy SelectEnemy(int maxDanger)
    {      
        List<Enemy> possibleEnemies =
            _enemiesPREBAS.Where(x => x.DangerLevel < maxDanger).ToList();

        int rnd = Random.Range(0, possibleEnemies.Count);

        if (possibleEnemies.Count <= 0)
            return null;

        //Select an enemy from the list
        _selectedEnemy = 
            possibleEnemies[rnd];

        return _selectedEnemy;
    }

    public void SpawnEnemy()
    {
        if (_selectedEnemy == null)
            return;

        Instantiate(_selectedEnemy, transform.position,
            _selectedEnemy.transform.rotation, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,0.2f);
    }
}
