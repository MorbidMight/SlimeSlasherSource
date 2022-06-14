using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    private Player playerComp;
    private CanvasRenderer canvasRenderer;
    private Animator animator;
    private Sprite HP4, HP3, HP2, HP1, HP0;
    void Start()
    {
        playerComp = FindObjectOfType<Player>();
        canvasRenderer = gameObject.GetComponent<CanvasRenderer>();
        animator = GetComponent<Animator>();
        HP4 = Resources.Load<Sprite>("Sprites/4 HP");      
        HP3 = Resources.Load<Sprite>("Sprites/3 HP");    
        HP2 = Resources.Load<Sprite>("Sprites/2HP");  
        HP1 = Resources.Load<Sprite>("Sprites/1 HP");
        HP0 = Resources.Load<Sprite>("Sprites/0 HP");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "SwordIcon")
        {
            if (playerComp.getThrownSword())
            {
                canvasRenderer.SetColor(Color.gray);

            }
            if (playerComp.getThrownSword() != true)
            {
                canvasRenderer.SetColor(Color.red);
            }
        }

        if(gameObject.tag == "HealthIcon")
        {
            if(playerComp.getHealth() >= 4)
            {
                gameObject.GetComponent<Image>().sprite = HP4;
            }
            if(playerComp.getHealth() == 3)
            {
                gameObject.GetComponent<Image>().sprite = HP3;
            }
            if (playerComp.getHealth() == 2)
            {
                gameObject.GetComponent<Image>().sprite = HP2;
            }
            if (playerComp.getHealth() == 1)
            {
                gameObject.GetComponent<Image>().sprite = HP1;
            }
            if (playerComp.getHealth() <= 0)
            {
                gameObject.GetComponent<Image>().sprite = HP0;
            }
        }
    }
}
