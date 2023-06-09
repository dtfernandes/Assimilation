﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Class that manages the upgrad/degrade functionality
/// </summary>
public class SkillSelection: MonoBehaviour
{
    [SerializeField]
    private SkillSlot[] _skillSlots;
    private GameState _gameState;

    [SerializeField]
    private List<Setupable> _updatables;

    private void Awake()
    {
        _gameState = GameState.Instance;
    }

    private void Start()
    {
       
        foreach (SkillSlot slot in _skillSlots)
        {
            slot.OnSelection = () =>
            {
                //Get back to the game
                _gameState.IsWorldStopped = false;
                gameObject.SetActive(false);

                //Update everything asigned
                foreach(Setupable s in _updatables)
                {
                    s.Setup();
                }
            };
        }

    }

    /// <summary>
    /// Setups the screen where the player chooses their upgrade
    /// </summary>
    /// <param name="type">Upgrade or Mutation</param>
    public void SetupSelection(UpgradeType type) 
    {
        _gameState.IsWorldStopped = true;

        List<UpgradeDefinition> definitions = new List<UpgradeDefinition> { };
        definitions.AddRange(_gameState.UpgradeDefinitions);

        //Find all of type
        definitions = definitions.Where(x => x.Type == type).ToList();



        foreach (SkillSlot slot in _skillSlots)
        {
            //Select Rarity
            Rarity selectedRarity = Rarity.Common;
            int probValue = Random.Range(0,101);
            
            if(probValue >= 60)
            {
                selectedRarity = Rarity.Rare;
            }
            else if(probValue >= 90)
            {
                selectedRarity = Rarity.Epic;
            }

            List<UpgradeDefinition> temp = null;

            //Make sure the skill is aplicable
            definitions = definitions.Where(x => x.GameValue.Value + x.Value >= x.GameValue.MinValue).ToList();

            while (true)
            {
                //Get the list of definition with this rarity
                temp = definitions.Where(
                    x => x.Rarity == selectedRarity).ToList();

                if (temp.Count > 0 || selectedRarity == 0)
                    break;
                else
                {
                    //If there0s no skill with rarity
                    //Try lower rarity
                    //There will always be enough Common Skills
                    selectedRarity -= 1;
                }
            }
            UpgradeDefinition selectedDefinition  = null;   
            
            if(temp.Count > 0)
            //Select Random Skill
                selectedDefinition = temp[Random.Range(0, temp.Count)];
                
            //Setup slots
            slot.Setup(selectedDefinition);

            //Remove definition from posssibilities
            definitions.Remove(selectedDefinition);
        }

    }
}
 