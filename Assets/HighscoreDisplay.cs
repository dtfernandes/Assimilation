using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _score;


    private void Start()
    {   
        GameTimer timer = FindAnyObjectByType<GameTimer>();
        GameState _state = GameState.Instance;
        List<ScoreValues> scoreList = _state.Scores;


        for (int i = 0; i < _score.Count; i++)
        {
            TextMeshProUGUI score = _score[i];

            if(scoreList.Count - 1 >= i)
            {
                if (scoreList == null)
                {
                    scoreList = new List<ScoreValues> { };
                }

                ScoreValues v = scoreList[i];
                score.text = $"{v.Name} - Floor {v.Floor} - {GameTimer.FormatTime(v.Time)}";

            }
            else
            {
                score.text = " ";
            }
        }

        timer.Reset();
    }
}
