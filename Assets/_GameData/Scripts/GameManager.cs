// GameManager.cs

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float timer ;
    
    [Header("Scene References")]
    [SerializeField] private CastleController friendCastle;
    [SerializeField] private CastleController enemyCastle;
    [SerializeField] private SpawnManager spawnManager;

    [Header("Level Data")]
    [SerializeField] private LevelDataSO[] allLevels;
    private static int currentLevelIndex = 0;
    
    [SerializeField] private Transform levelDesignSpawnPoint;
    private GameObject currentLevelDesignInstance;
    
    
    
    private void OnEnable()
    {
        // UIManager'dan gelen seviye geçiş isteklerini dinle.
        EventManager.OnNextLevelRequested += LoadNextLevel;
        EventManager.OnRestartLevelRequested += RestartLevel;
    }

    private void OnDisable()
    {
        EventManager.OnNextLevelRequested -= LoadNextLevel;
        EventManager.OnRestartLevelRequested -= RestartLevel;
    }

    private void Start()
    {
        Time.timeScale = 1;
        InitializeLevel();
    }

    void Update()
    {
        StartGame();
    }

    private void StartGame()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            EventManager.TriggerFriendSpawn();
            EventManager.TriggerEnemySpawn();
        }
    }
    
    private void InitializeLevel()
    {
        LevelDataSO currentLevelData = allLevels[currentLevelIndex]; 
        
        if (currentLevelDesignInstance != null)
        {
            Destroy(currentLevelDesignInstance);
        }

        // 2. Tarif kitabında bir level tasarımı belirtilmişse, onu yükle (Instantiate).
        if (currentLevelData.levelDesignPrefab != null)
        {
            currentLevelDesignInstance = Instantiate(currentLevelData.levelDesignPrefab, levelDesignSpawnPoint.position, levelDesignSpawnPoint.rotation);
        }
        
        friendCastle.Initialize(currentLevelData.friendCastleHealth);
        enemyCastle.Initialize(currentLevelData.enemyCastleHealth);
        
        spawnManager.Initialize(currentLevelData);
    }
    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= allLevels.Length)
        {
            currentLevelIndex = 0;
            Debug.Log("Tebrikler tüm levelleri tamamladınız. En başa dönüyoruz.");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}