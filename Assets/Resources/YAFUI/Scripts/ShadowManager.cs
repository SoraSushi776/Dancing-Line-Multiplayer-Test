using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowManager : MonoBehaviour
{

    //public List<Image> Shadows = new List<Image>();

    public Sprite Shadow256;
    public Sprite Shadow128;
    public Sprite Shadow064;
    public Sprite Shadow032;
    public Sprite Shadow016;

    public Image img01;
    public Image img02;


    [Range(0, 1)]
    public float opacity = 0.75f;
    private float _opacity;


    //private void updateOpacity()
    //{
    //    foreach (Image i in Shadows)
    //    {
    //        i.color = new Color(i.color.r, i.color.g, i.color.b, _opacity);
    //    }
    //}

    [Range(0, 1)]
    public float size = 1f;
    private float _size;

    // Use this for initialization
    void Start ()
    {
        //Shadows.Clear();

        //for (int i = 0; i < gameObject.transform.childCount; i++)
        //{
        //    Image img = gameObject.transform.GetChild(i).GetComponent<Image>();

        //    if (img != null)
        //    {
        //        Shadows.Add(img);
        //    }
        //}

        //Shadows.Sort
        //(
        //     delegate (Image i1, Image i2)
        //     {
        //         return i1.name.CompareTo(i2.name);
        //     }
        // );

    }


    void FixedUpdate()
    {
        bool requireUpdate = false;

        if (opacity != _opacity)
        {
            _opacity = opacity;
            requireUpdate = true;
        }

        if (size != _size)
        {
            _size = size;
            requireUpdate = true;
        }

        if (requireUpdate)
        {
            updateShadows();
        }
    }


    public void updateShadows()
    {

        if (_size <= 0.25f)
        {
            img01.sprite = Shadow016;
            img01.color = new Color(img01.color.r, img01.color.g, img01.color.b, _size * _opacity);
            img02.color = new Color(img02.color.r, img02.color.g, img02.color.b, 0f);
        }
        else if (_size <= 0.50f)
        {
            float S01 = (_size - 0.25f) / 0.25f;

            img01.sprite = Shadow016;
            img01.color = new Color(img01.color.r, img01.color.g, img01.color.b, (1f-S01) * _opacity);

            img02.sprite = Shadow032;
            img02.color = new Color(img02.color.r, img02.color.g, img02.color.b, S01 * _opacity);
        }
        else if (_size <= 0.75f)
        {
            float S01 = (_size - 0.50f) / 0.25f;

            img01.sprite = Shadow032;
            img01.color = new Color(img01.color.r, img01.color.g, img01.color.b, (1f - S01) * _opacity);

            img02.sprite = Shadow064;
            img02.color = new Color(img02.color.r, img02.color.g, img02.color.b, S01 * _opacity);
        }
        else if (_size <= 1f)
        {
            float S01 = (_size - 0.75f) / 0.25f;

            img01.sprite = Shadow064;
            img01.color = new Color(img01.color.r, img01.color.g, img01.color.b, (1f - S01) * _opacity);

            img02.sprite = Shadow064;
            img02.color = new Color(img02.color.r, img02.color.g, img02.color.b, S01 * _opacity);
        }
        else
        {
            img01.sprite = Shadow064;
            img01.color = new Color(img01.color.r, img01.color.g, img01.color.b, 0f);

            img02.sprite = Shadow064;
            img02.color = new Color(img02.color.r, img02.color.g, img02.color.b, 1f * _opacity);
        }
    }



}
