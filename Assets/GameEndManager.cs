using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    private GameState _gameState;

    [SerializeField]
    private TextMeshProUGUI _endingText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    private GameTimer _timer;
    [SerializeField]
    private TextMeshProUGUI _nameField;

    // Start is called before the first frame update
    void Awake()
    {
        _gameState = GameState.Instance;
    
        if(_gameState.GameResult == GameResult.Win)
        {
            _endingText.text = "You Win. Thanks for Playing.";
        }
        else if (_gameState.GameResult == GameResult.Lose)
        {
            _endingText.text = "You Lost. Better luck next time.";
        }

        _timer = GameObject.FindFirstObjectByType<GameTimer>();

        _timer.Stop();

        _timeText.text = _timer.GetElapsedTime() + " Floor " + _gameState.Floor;
    }

    public void SaveScore()
    {
        ScoreValues newScore = new ScoreValues(_gameState.Floor, _nameField.text, _timer.GetElapsedSeconds());

        _gameState.Scores.Add(newScore);


        // Sort the list by "Floor" int value and "Sec" float value
        _gameState.Scores.Sort((score1, score2) =>
        {
            // First, compare by "Floor" int value
            int floorComparison = score1.Floor.CompareTo(score2.Floor);

            if (floorComparison != 0)
            {
                // If the "Floor" values are different, return the result of the comparison
                return floorComparison;
            }
            else
            {
                // If the "Floor" values are the same, compare by "Time" float value
                return score1.Time.CompareTo(score2.Time);
            }
        });
    }


}
