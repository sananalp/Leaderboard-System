using UnityEngine.UI;
using Game.Player;
using UnityEngine;

namespace Game.View
{
    public class AddWindowView : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _nameText;

        public void AddPlayerStats(PlayerStatsBar statsBar)
        {
            var nameText = _nameText.text;
            var scoreText = int.Parse(_scoreText.text);

            statsBar.PlayerStats = new PlayerStats();
            statsBar.PlayerStats.Name = nameText;
            statsBar.PlayerStats.Score = scoreText;
        }
    }
}