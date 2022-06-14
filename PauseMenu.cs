using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool menuActivated = false;
    [SerializeField] GameObject menu;

    public void ActivateMenu()
    {
        menu.SetActive(true);
        menuActivated = true;
        Debug.Log(menuActivated);
        Time.timeScale = 0f;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1f;
        menuActivated = false;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !menuActivated)
        {
            ActivateMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menuActivated)
        {
            DeactivateMenu();
        }
    }


}
