/*
This is the base script for all other dialogue scripts.
*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Dialogue_YesNoCancle : MonoBehaviour 
{


//variables 
#region variables
    private Animator anim;
    public Button YesButton;
    public Button NoButton;
    public Button CancleButton; 
#endregion

    //delegates for each button in the dialogue
    //more delegates will need to be created if your dialogue window needs more options, feel free to contact me if you need help.
#region Delegates

    public delegate void YesDelegate();
    public YesDelegate YesAction;

    public delegate void NoDelegate();
    public NoDelegate NoAction;

    public delegate void CancelDelegate();
    public CancelDelegate CancelAction;

#endregion

#region Methods

    //on start set the Animator, and hide the children
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Show");

        YesButton = gameObject.transform.Find("Panel/Buttons/YesButton").GetComponent<Button>();
        YesButton.onClick.AddListener(PressYes);
        NoButton = gameObject.transform.Find("Panel/Buttons/NoButton").GetComponent<Button>();
        NoButton.onClick.AddListener(PressNo);
        CancleButton = gameObject.transform.Find("Panel/Buttons/CancleButton").GetComponent<Button>();
        CancleButton.onClick.AddListener(PressCancel);
    }
    

    //this will show the dialogue
    //this method might need to be overrode depending how many text boxes and other components need to be set.
    public static Dialogue_YesNoCancle ShowDialogue(string title, string message)
    {
        Dialogue_YesNoCancle newDialogue = Instantiate(Resources.Load("Dialogue_YesNoCancle", typeof(Dialogue_YesNoCancle))) as Dialogue_YesNoCancle;
        Text thisTitle = newDialogue.transform.Find("Panel/Title").GetComponent<Text>();
        Text thisMessage = newDialogue.transform.Find("Panel/Message").GetComponent<Text>();

        //set the text for the text components
        thisTitle.text = title;
        thisMessage.text = message;
        
        return newDialogue;
    }

    public void RemoveDialogue()
    {
        Destroy(gameObject);
    }

    ////base ShowDialogue method
    //public void ShowDialogue()
    //{
    //    anim.SetBool("Active", true);
    //    anim.SetBool("On", true);
    //}

    //this method will be called when the yes button is pressed in the dialogue window
    public void PressYes()
    {
        if (YesAction != null)
        {
            YesAction(); //this is a reference to the YesAction/method that was set before the dialogue was called
        }

        anim.SetTrigger("Remove"); //close the dialogue Window
    }

    public void PressNo()
    {
        if (NoAction != null)
        {
            NoAction(); //this is a reference to the NoAction/method that was set before the dialogue was called
        }

        anim.SetTrigger("Remove"); //close the dialogue Window
    }

    public void PressCancel()
    {
        if (CancelAction != null)
        {
            CancelAction(); //this is a reference to the CancelAction/method that was set before the dialogue was called
        }

        anim.SetTrigger("Remove"); //close the dialogue Window
    }
#endregion

}
