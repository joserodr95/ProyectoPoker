using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
    public static void GoToLevel(string levelName)
    {
        SceneManager.LoadScene($"PokerLevel{levelName}", LoadSceneMode.Single);
    }
}
