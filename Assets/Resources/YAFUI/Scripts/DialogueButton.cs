/*
This Script is used to set the actions for each button in the dialogue window, and activate the dialogue.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class DialogueButton : MonoBehaviour
{

    public string title;
    public string message;
    public Text text;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CreateNewDialogue);
        text = transform.GetComponentInChildren<Text>();
    }

    void CreateNewDialogue()
    {
        Dialogue_YesNoCancle DYNC = Dialogue_YesNoCancle.ShowDialogue(title,message);
        DYNC.YesAction = YesPress;
        DYNC.NoAction = NoPress;
        DYNC.CancelAction = CancelPress;
    }


    //when button1 is pressed
    public void YesPress()
    {
        text.text = "dialogue: Yes";
    }

    //when button2 is pressed
    public void NoPress()
    {
        text.text = "dialogue: No";
    }

    //when button3 is pressed
    public void CancelPress()
    {
        text.text = "dialogue: Cancel";
    }


}
