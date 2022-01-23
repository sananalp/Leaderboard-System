using UnityEngine;
using UnityEngine.UI;
using System;
using Game.Listener;

namespace Game.Player
{
    public class PlayerStatsBar : MonoBehaviour, IComparable
    {
        private PlayerStats _playerStats;
        [SerializeField] private Text _positionText;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Image _barImage1;
        [SerializeField] private Image _barImage2;

        public PlayerStats PlayerStats { get { return _playerStats; } set { _playerStats = value; } }
        public Text PositionText { get { return _positionText; } set { _positionText = value; } }
        public Text NameText { get { return _nameText; } set { _nameText = value; } }
        public Text ScoreText { get { return _scoreText; } set { _scoreText = value; } }

        public void OnClick()
        {
            var toggle = GetComponent<Toggle>();
            var toggleListener = GetComponentInParent<ToggleListener>();

            if (toggle.isOn)
            {
                _barImage1.color = Color.white * 0.9f;
                _barImage2.color = Color.white * 0.9f;
            }
            else
            {
                _barImage1.color = Color.white * 1.0f;
                _barImage2.color = Color.white * 1.0f;
            }

            toggleListener.CallListener();
        }

        public int CompareTo(object obj)
        {
            if (obj is PlayerStatsBar statsBar) return _playerStats.Score.CompareTo(statsBar._playerStats.Score);
            else throw new Exception();
        }
    }
}