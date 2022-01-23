using UnityEngine.UI;
using UnityEngine;
using Game.Player;

namespace Game.View
{
    public class EditWindowView : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _nameText;

        public void EditPlayerStats(PlayerStatsBar statsBar)
        {
            var playerStats = statsBar.PlayerStats;
            var nameText = _nameText.text;
            var scoreText = int.Parse(_scoreText.text);

            playerStats.Name = nameText;
            playerStats.Score = scoreText;
        }
    }
}