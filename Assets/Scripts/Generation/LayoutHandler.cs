using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutHandler : MonoBehaviour
{
    [SerializeField]
    private List<EnemySpawner> _spawners;

    [SerializeField]
    private SpriteRenderer[] _platforms;

    [SerializeField]
    private Sprite[] _platformVariation;

    public void Setup(Biome biome)
    {
        if(_platformVariation.Length <= 0) return;
        switch(biome)
        {
            case Biome.Forest:
                UpdateDisplay(_platformVariation[0]);
                break;
            case Biome.Cavern:
                UpdateDisplay(_platformVariation[1]);
                break;
        }
    }

    private void UpdateDisplay(Sprite selectedSprite)
    {
        foreach(SpriteRenderer sR in _platforms)
        {
            sR.sprite = selectedSprite;
        }
    }

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
