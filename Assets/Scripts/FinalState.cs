using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalState : MonoBehaviour
{
    private const float PanelTime = 5.5f;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject cards;

    public enum EState
    {
        Win,
        Lose
    }

    public void EndGame(EState state)
    {
        panel.SetActive(true);
        cards.SetActive(false);

        switch (state)
        {
            case EState.Win:
                StartCoroutine(Win());
                break;
            case EState.Lose:
                StartCoroutine(Lose());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private IEnumerator Win()
    {
        winText.SetActive(true);

        yield return new WaitForSeconds(PanelTime);

        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }

    private IEnumerator Lose()
    {
        loseText.SetActive(true);

        yield return new WaitForSeconds(PanelTime);

        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }
}