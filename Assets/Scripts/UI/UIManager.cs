using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void StartButton()
    {
        Time.timeScale = 1;
    }

    public void OpenWinScreen()
    {
        _winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenLoseScreen()
    {
        _loseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(0);
    }
}
