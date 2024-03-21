//Allows the Switch between demo scenes

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour 
{

    //the scene to switch to 
    public int sceneNum;

	// Use this for initialization
	void Start () 
    {
	    int thiSceneNum = SceneManager.GetActiveScene().buildIndex;

        //do not allow to switch to the current scene
        if (sceneNum == thiSceneNum)
        {
            GetComponent<Button>().interactable = false;
        }
	}
	
	// on button press
	public void Press () 
    {

        if (sceneNum < 0)
        {
            Debug.LogError("no negative scenes");
            return;
        }

        if (sceneNum > SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("scene doesn't exist");
            return;
        }


	    SceneManager.LoadScene(sceneNum);
	}
}
