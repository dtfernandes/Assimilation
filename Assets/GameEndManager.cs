using UnityEngine;
using TMPro;
using System.Threading;

public class GameEndManager : MonoBehaviour
{
    private GameState _gameState;

    [SerializeField]
    private TextMeshProUGUI _endingText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    private Timer _time;

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

        GameTimer timer = GameObject.FindFirstObjectByType<GameTimer>();

        timer.Stop();

        _timeText.text = timer.GetElapsedTime() + " Floor " + _gameState.Floor;
    }
}
