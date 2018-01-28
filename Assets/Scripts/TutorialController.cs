using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public GameObject slide1;
    public GameObject slide2;

    private int slide;

    private void Start()
    {
        slide = 0;
    }

    public void click_next(string name)
    {
        if(slide == 0)
        {
            slide1.SetActive(false);
            slide2.SetActive(true);
            slide++;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
