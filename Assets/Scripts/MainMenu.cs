using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }

    public void LoadLocale()
    {
        int newLocale = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale) == 0 ? 1 : 0;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[newLocale];
    }

    public void Quit()
    {
        Application.Quit();
    }
}
