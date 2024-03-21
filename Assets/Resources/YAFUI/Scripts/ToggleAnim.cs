using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleAnim : MonoBehaviour
{

    private Toggle thisToggle;
    /// <summary>
    /// The animator.
    /// </summary>
    private Animator Anim;
    
    // Use this for initialization
    void Start ()
    {
        thisToggle = GetComponent<Toggle>();
        Anim = GetComponent<Animator>();
        thisToggle.onValueChanged.AddListener(OnPress);
        OnPress(thisToggle.isOn);
        Anim.SetBool("Disabled", !thisToggle.IsInteractable());
        IsInteractable = thisToggle.IsInteractable();
    }

    void OnEnable()
    {
        thisToggle = GetComponent<Toggle>();
        Anim = GetComponent<Animator>();
        thisToggle.onValueChanged.AddListener(OnPress);
        OnPress(thisToggle.isOn);
        Anim.SetBool("Disabled", !thisToggle.IsInteractable());
        IsInteractable = thisToggle.IsInteractable();
    }

    private bool IsInteractable;
    void FixedUpdate()
    {
        if (IsInteractable != thisToggle.IsInteractable())
        {
            IsInteractable = thisToggle.IsInteractable();

            Anim.SetBool("Disabled", !IsInteractable);
        }
    }

    public void OnPress(bool value)
    {
        if (value)
        {
            Anim.SetBool("Value", true);
        }
        else
        {
            Anim.SetBool("Value", false);
        }

    }
}
