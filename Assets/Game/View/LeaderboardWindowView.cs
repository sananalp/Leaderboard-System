using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Game.Player;
using Game.Listener;

namespace Game.View
{
    public class LeaderboardWindowView : MonoBehaviour
    {
        [SerializeField] private AddWindowView _addWindow;
        [SerializeField] private EditWindowView _editWindow;
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _statsPrefab;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _editButton;
        private List<PlayerStatsBar> _statsBarList = new List<PlayerStatsBar>();
        private delegate void SortPlayerStatsHandler();
        private event SortPlayerStatsHandler OnStatsCreate;
        private delegate void StatsBarHandler(PlayerStatsBar playerStatsBar);
        private event StatsBarHandler OnStatsBarSpawn;
        private event StatsBarHandler OnEditStatsBar;

        private void OnEnable()
        {
            OnStatsBarSpawn += _addWindow.AddPlayerStats;
            OnEditStatsBar += _editWindow.EditPlayerStats;
            OnStatsCreate += SortPlayerStats;
            OnStatsCreate += InitStatsBar;
            _toggleGroup.GetComponent<ToggleListener>().OnToggleSelect += ActiveTogglesChecker;
        }
        private void OnDisable()
        {
            OnStatsBarSpawn -= _addWindow.AddPlayerStats;
            OnEditStatsBar -= _editWindow.EditPlayerStats;
            OnStatsCreate -= SortPlayerStats;
            OnStatsCreate -= InitStatsBar;
            _toggleGroup.GetComponent<ToggleListener>().OnToggleSelect -= ActiveTogglesChecker;
        }
        public void ActiveTogglesChecker()
        {
            if (_toggleGroup.ActiveToggles().Any())
            {
                _addButton.gameObject.SetActive(false);
                _editButton.gameObject.SetActive(true);
            }
            else
            {
                _addButton.gameObject.SetActive(true);
                _editButton.gameObject.SetActive(false);
            }
        }
        public void SpawnStatsBar()
        {
            var statsObject = Instantiate(_statsPrefab, _content);
            var statsBar = statsObject.GetComponent<PlayerStatsBar>();
            var toggle = statsObject.GetComponent<Toggle>();
            toggle.group = _toggleGroup;
            _statsBarList.Add(statsBar);

            OnStatsBarSpawn?.Invoke(statsBar);
            OnStatsCreate?.Invoke();
        }
        private void SortPlayerStats()
        {
            _statsBarList.Sort();
            _statsBarList.Reverse();

            for (int i = 0; i < _statsBarList.Count; i++)
            {
                _statsBarList[i].PlayerStats.Position = i + 1;
                _statsBarList[i].PositionText.text = _statsBarList[i].PlayerStats.Position.ToString();
                _statsBarList[i].transform.SetSiblingIndex(i);
            }
        }
        private void InitStatsBar()
        {
            for (int i = 0; i < _statsBarList.Count; i++)
            {
                _statsBarList[i].NameText.text = _statsBarList[i].PlayerStats.Name;
                _statsBarList[i].ScoreText.text = _statsBarList[i].PlayerStats.Score.ToString();
            }
        }
        public void TryRemoveStatsBar()
        {
            if (_toggleGroup.ActiveToggles().Any())
            {
                var toggle = _toggleGroup.ActiveToggles().FirstOrDefault();
                var statsBar = toggle.GetComponent<PlayerStatsBar>();
                _statsBarList.Remove(statsBar);
                Destroy(statsBar.gameObject);
                SortPlayerStats();
            }
            else
            {
                Debug.Log("Нет активных выборов!");
            }
        }
        public void EditStatsBar()
        {
            var toggle = _toggleGroup.ActiveToggles().FirstOrDefault();
            var statsBar = toggle.GetComponent<PlayerStatsBar>();
            OnEditStatsBar?.Invoke(statsBar);
            OnStatsCreate?.Invoke();
        }
        public void TryCancelChoose()
        {
            if (_toggleGroup.AnyTogglesOn())
            {
                _toggleGroup.SetAllTogglesOff();
            }
        }
    }
}