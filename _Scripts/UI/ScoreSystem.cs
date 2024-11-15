using _Scripts.Creatures;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _multiplierText;

    private int _score;
    private int _highScore;
    private int _killMiltiplier;
    private float _scoreMultiplierExpiration;
    
    private void Start()
    {
        Zombie.Died += Zombie_Died;
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreText.SetText("High Score: " + _highScore);
    }

    private void Zombie_Died()
    {
        UpdateKillMultiplier();
        
        _score += _killMiltiplier;
        
        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.SetText("High Score: " + _highScore);
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        _scoreText.SetText(_score.ToString());
    }

    private void UpdateKillMultiplier()
    {
        if (Time.time < _scoreMultiplierExpiration)
        {
            _killMiltiplier++;
        }
        else
        {
            _killMiltiplier = 1;
        }

        _scoreMultiplierExpiration = Time.time + 1f;
        
        _multiplierText.SetText("x " + _killMiltiplier);
        
        if (_killMiltiplier < 3)
            _multiplierText.color = Color.blue;
        else if (_killMiltiplier < 10)
            _multiplierText.color = Color.green;
        else if (_killMiltiplier < 20)
            _multiplierText.color = Color.yellow;
        else if (_killMiltiplier < 50)
            _multiplierText.color = Color.red;
    }
}
