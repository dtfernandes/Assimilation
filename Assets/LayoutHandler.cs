using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutHandler : MonoBehaviour
{
    [SerializeField]
    private List<EnemySpawner> _spawners;

    public void SpawnEnemies(int dangerLevel)
    {
        int currentDanger = dangerLevel;
        
        foreach(EnemySpawner eS in _spawners)
        {
            if (currentDanger <= 0)
                break;

            currentDanger -= 
                eS.SelectEnemy(currentDanger)?.DangerLevel ?? 0;
        }


        foreach (EnemySpawner eS in _spawners)
        {
            eS.SpawnEnemy();
        }
    }
}
