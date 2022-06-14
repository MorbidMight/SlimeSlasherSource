using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fountain : MonoBehaviour
{
    bool secondState = false;
    SpriteRenderer srenderer;
    [SerializeField] Sprite toChangeTo;
    [SerializeField] Image flashImg;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject mainCanvas;
    void Start()
    {
        srenderer = GetComponent<SpriteRenderer>();
    }

    public void changeState()
    {
        srenderer.sprite = toChangeTo;
        mainCanvas.SetActive(false);
        if (!secondState)
        {
            StartCoroutine("StartEndSequence");
        }
        secondState = true;


    }

    IEnumerator StartEndSequence()
    {
        Time.timeScale = 0;
       bool colorCount = false;
       foreach(char c in "FINISH")
        {
            text.text += c;
            if(colorCount)
            {
                flashImg.color = new Color(0, 0, 0, 0);
                text.color = new Color(1, 1, 1, 1);


                colorCount = !colorCount;
            }else
            {
                flashImg.color = new Color(0.102f, 0.102f, 0.102f, 0.316f);
                text.color = new Color(0, 0, 0, 1);
                colorCount = !colorCount;
            }
 
            yield return new WaitForSecondsRealtime(0.13f);
        }
       for(int x = 0; x < 10; x++)
        {
            if (colorCount)
            {
                flashImg.color = new Color(0, 0, 0, 0);
                text.color = new Color(1, 1, 1, 1);

                colorCount = !colorCount;
            }
            else
            {
                flashImg.color = new Color(0.05f, 0.05f, 0.05f, 0.316f);
                text.color = new Color(0, 0, 0, 1);
                colorCount = !colorCount;
            }

            yield return new WaitForSecondsRealtime(0.13f);
        }
        Time.timeScale = 1f;
        SceneManagement.LoadNextScene();
    }
}
