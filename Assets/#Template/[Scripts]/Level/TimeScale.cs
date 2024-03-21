using UnityEngine;

namespace DancingLineFanmade.Level
{
    [DisallowMultipleComponent]
    public class TimeScale : MonoBehaviour
    {
        [SerializeField] private KeyCode key = KeyCode.T;
        [SerializeField] private float enabledValue = 1.25f;
        [SerializeField] private float disabledValue = 1f;

        private new bool enabled = false;

#if UNITY_EDITOR
        private void Update()
        {
            if (LevelManager.GameState == GameStatus.Playing)
            {
                if (!enabled)
                {
                    if (Input.GetKeyDown(key))
                    {
                        Player.Instance.track.pitch = enabledValue;
                        Time.timeScale = enabledValue;
                        enabled = true;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(key))
                    {
                        Player.Instance.track.pitch = disabledValue;
                        Time.timeScale = disabledValue;
                        enabled = false;
                    }
                }
            }
        }
#endif
    }
}