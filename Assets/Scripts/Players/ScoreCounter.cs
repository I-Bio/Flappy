using System;
using Entities;
using UnityEngine;

namespace Players
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private ScoreZone _scoreZone;
        
        private int _score;

        public event Action<int> ScoreChanged;
        
        public void Add()
        {
            _score++;
            _scoreZone.SetPosition();
            ScoreChanged?.Invoke(_score);
        }

        public void Reset()
        {
            _score = 0;
            _scoreZone.Reset();
            ScoreChanged?.Invoke(_score);
        }
    }
}
