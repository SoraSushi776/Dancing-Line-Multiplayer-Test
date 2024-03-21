using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nav : MonoBehaviour
{
    public GameObject[] gameobjects;

    public int index0;
    public int index1;


    void Awake()
    {
        updateView();
    }

    public void prev()
    {
        index0--;
        index1 = index0 % gameobjects.Length;
        
        updateView();
    }
    
    public void next()
    {
        index0++;
        index1 = index0 % gameobjects.Length;

        updateView();
    }


    void updateView()
    {
        for (int i = 0; i < gameobjects.Length; i++)
        {
            if (index1 == i)
            {
                gameobjects[i].SetActive(true);
            }
            else
            {
                gameobjects[i].SetActive(false);
            }
        }
    }


}
