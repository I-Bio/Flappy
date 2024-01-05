using Players;
using Spawners;
using UnityEngine;

namespace UI
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private EndGameScreen _endGameScreen;

        private void OnEnable()
        {
            _player.GameOver += OnGameOver;
            _startScreen.PlayButtonClicked += OnPlayButtonClicked;
            _endGameScreen.RestartButtonClicked += OnRestartButtonClicked;
        }

        private void OnDisable()
        {
            _player.GameOver -= OnGameOver;
            _startScreen.PlayButtonClicked -= OnPlayButtonClicked;
            _endGameScreen.RestartButtonClicked -= OnRestartButtonClicked;
        }

        private void Start()
        {
            Time.timeScale = 0f;
            _startScreen.Open();
        }

        private void OnGameOver()
        {
            Time.timeScale = 0f;
            _endGameScreen.Open();
        }
        
        private void OnPlayButtonClicked()
        {
            _startScreen.Close();
            StartGame();
        }

        private void OnRestartButtonClicked()
        {
            _endGameScreen.Close();
            StartGame();
        }
        
        private void StartGame()
        {
            Time.timeScale = 1f;
            _enemySpawner.Reset();
            _player.Reset();
        }
    }
}