using UnityEngine;

namespace Game.Listener
{
    public class ToggleListener : MonoBehaviour
    {
        public delegate void ToggleHandler();

        public event ToggleHandler OnToggleSelect;

        public void CallListener()
        {
            OnToggleSelect?.Invoke();
        }
    }
}