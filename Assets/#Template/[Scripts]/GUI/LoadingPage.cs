using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DancingLineFanmade.UI
{
    [DisallowMultipleComponent]
    public class LoadingPage : MonoBehaviour
    {
        public static LoadingPage Instance { get; private set; }

        [SerializeField] private Text loadingText;
        [SerializeField] private Image background;
        [SerializeField] private Image loadingImage;

        private CanvasGroup canvasGroup;
        private AsyncOperation operation = null;

        private void Awake()
        {
            Instance = this;
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            canvasGroup.alpha = 0f;
        }

        public Tween Fade(float alpha, float duration, Ease ease = Ease.Linear)
        {
            return canvasGroup.DOFade(alpha, duration).SetEase(ease);
        }

        public void Load(string sceneName)
        {
            background.color = Color.white;
            loadingText.color = Color.black;
            loadingImage.color = Color.black;

            Fade(1f, 0.4f).OnComplete(() =>
            {
                operation = SceneManager.LoadSceneAsync(sceneName);

                if (operation == null)
                {
                    Debug.LogError("Scene not found: " + sceneName);
                    return;
                }

                if (operation.isDone)
                {
                    Debug.LogError("Scene already loaded: " + sceneName);
                    return;
                }

                StartCoroutine(LoadScene(sceneName));
            });
        }

        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                    Fade(0f, 0.4f);
                    yield break;
                }

                yield return null;
            }


        }
    }
}