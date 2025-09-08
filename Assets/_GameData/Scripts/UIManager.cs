using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    
    
    private void OnEnable()
    {
        EventManager.OnGameWin += ShowWinPanel;
        EventManager.OnGameOver += ShowLosePanel;
    }
    
    private void OnDisable()
    {
        EventManager.OnGameWin -= ShowWinPanel;
        EventManager.OnGameOver -= ShowLosePanel;
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
        //NextLevelStart //EventManger
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
        //reStartLevel //EventManger
    }
    
    public void OnNextLevelButtonClick()
    {
        // GameManager'a "sonraki seviyeye geç" anonsu yap.
        EventManager.TriggerNextLevel();
    }

    public void OnRestartButtonClick()
    {
        // GameManager'a "seviyeyi yeniden başlat" anonsu yap.
        EventManager.TriggerRestartLevel();
    }
}
