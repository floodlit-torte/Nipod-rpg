using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    private int gameScene = 1;
    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void ExitGame()
    {
        Debug.Log("Exit!!!");
        Application.Quit();
    }
}
