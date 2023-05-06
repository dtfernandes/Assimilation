using UnityEngine;

/// <summary>
/// Class that manages the upgrad/degrade functionality
/// </summary>
public class SkillSelection: MonoBehaviour
{
    [SerializeField]
    private SkillSlot[] _skillSlots;
    private GameState _gameState;
    private void Start()
    {
        _gameState = GameState.Instance;

        foreach (SkillSlot slot in _skillSlots)
        {
            slot.OnSelection = () =>
            {
                //Get back to the game
                _gameState.IsWorldStopped = false;
                gameObject.SetActive(false);
            };
        }

    }

    public void SetupSelection(UpgradeType type) 
    {
        _gameState.IsWorldStopped = true;
        //Find all 

        //Select some

        //Setup slots

    }
}