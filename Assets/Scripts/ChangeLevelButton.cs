using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeLevelButton : MonoBehaviour 
{

    public void ChangeLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ResetGame()
    {
        GameManager.Instance.Lives = 3;
        GameManager.Instance.Score = 0;
        GameManager.Instance.NumSeeds = 0;
        SceneManager.LoadScene("MainMenu");
    }
    
}
