//Allows the Switch between demo scenes

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DancingLineFanmade.UI;

public class SwitchScenes : MonoBehaviour 
{
    public string sceneName;

    void Start()
    {
        string thiSceneName = SceneManager.GetActiveScene().name;

        if (sceneName == thiSceneName)
        {
            GetComponent<Button>().interactable = false;
        }
	}

    public void Press()
    {
        // 获取场景列表
        int sceneNum = SceneManager.sceneCountInBuildSettings;

        // 遍历场景列表
        for (int i = 0; i < sceneNum; i++)
        {
            // 获取场景路径
            string path = SceneUtility.GetScenePathByBuildIndex(i);

            // 获取场景名
            string name = path.Substring(path.LastIndexOf('/') + 1, path.LastIndexOf('.') - path.LastIndexOf('/') - 1);

            // 如果场景名等于指定场景名
            if (name == sceneName)
            {
                // 加载场景
                LoadingPage.Instance.Load(sceneName);
                return;
            }
        }

        Debug.LogError("Scene not found: " + sceneName);
    }
}
