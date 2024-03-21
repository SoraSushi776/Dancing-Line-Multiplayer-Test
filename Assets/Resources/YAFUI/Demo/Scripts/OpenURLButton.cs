/*
OpenURLButton.cs
Allows for a URL to be opened 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURLButton : MonoBehaviour {

    public string URL;

    public void OnPress()
    {
        Application.OpenURL(URL);
    }
}
