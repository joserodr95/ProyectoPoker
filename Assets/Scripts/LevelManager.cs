using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene($"PokerLevel{levelName}", LoadSceneMode.Single);
    }
}
