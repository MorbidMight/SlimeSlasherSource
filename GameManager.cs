using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    void Start()
    {

    }

    void Update()
    {
        DontDestroyOnLoad(gameObject);
        string currScene = SceneManager.GetActiveScene().name;
        if (!currScene.Equals("Lose Screen") && !currScene.Equals("Main Menu"))
        {
            level = SceneManager.GetActiveScene().buildIndex;
        }

        var managers = FindObjectsOfType<GameManager>();
        if(FindObjectsOfType<GameManager>().Length > 1)
        {
            foreach(GameManager x in managers)
            {
                if(!x.gameObject.Equals(gameObject))
                {
                    Destroy(x.gameObject);
                }
            }
            
        }
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(level);
    }


    
}
