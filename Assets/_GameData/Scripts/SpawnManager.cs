using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject friendPrefab;
    [SerializeField] private GameObject enemyPrefab;
    
    [Header("Spwan And Target")]
    [SerializeField] private Transform friendSpawnPoint; 
    [SerializeField] private Transform enemyCastleTarget; 
    
    [SerializeField] private Transform enemySpawnPoint;   
    [SerializeField] private Transform friendCastleTarget;
    
    private LevelDataSO levelData;
    
    [Header("Multiplier Settings")]
    [SerializeField] private int multiplicationFactor = 5;
    [SerializeField] private float spawnSpacing = 1.5f;
    

    public void Initialize(LevelDataSO data)
    {
        levelData = data;
    }

    private void OnEnable()
    {
        EventManager.OnSpawnFriendRequested += SpawnFriend;
        EventManager.OnSpawnEnemyRequested += SpawnEnemy;
        EventManager.OnUnitMultiplyRequested += MultiplyUnit;
    }

    private void OnDisable()
    {
        EventManager.OnSpawnFriendRequested -= SpawnFriend;
        EventManager.OnSpawnEnemyRequested -= SpawnEnemy;
        EventManager.OnUnitMultiplyRequested -= MultiplyUnit;
    }
    
    private void SpawnFriend()
    {
        GameObject friendGO = Instantiate(friendPrefab, friendSpawnPoint.position, Quaternion.identity);
        FriendController friendController = friendGO.GetComponent<FriendController>();
        
        // Askere görevini ver: Hedefi ve tarif kitabından okunan hızı.
        friendController.Initialize(enemyCastleTarget, levelData.friendSpeed , levelData.Damage );
    }

    private void SpawnEnemy()
    {
        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        EnemyController enemyController = enemyGO.GetComponent<EnemyController>();
        
        // Düşmana görevini ver: Hedefi ve tarif kitabından okunan hızı.
        enemyController.Initialize(friendCastleTarget, levelData.enemySpeed,  levelData.Damage);
    }
    private static readonly List<Vector3> relativeSpawnPoints = new List<Vector3>
    {
        // Arka ve yanlar
        new Vector3(1, 0, -1), new Vector3(0, 0, -1), new Vector3(-1, 0, -1),
        new Vector3(1, 0, 0),                           new Vector3(-1, 0, 0),
        // Ön ve yanlar
        new Vector3(1, 0, 1), new Vector3(0, 0, 1), new Vector3(-1, 0, 1),
        // Daha uzak noktalar
        new Vector3(0, 0, -2), new Vector3(2, 0, 0), new Vector3(-2, 0, 0), new Vector3(0, 0, 2)
    };

    private void MultiplyUnit(GameObject originalUnit, MultiplierGate gate)
    {
        if (originalUnit.TryGetComponent<FriendController>(out var originalFriend))
        {
            originalFriend.RegisterGate(gate);
            CreateClonesRandomly(originalUnit, originalFriend, gate);
        }
        else if (originalUnit.TryGetComponent<EnemyController>(out var originalEnemy))
        {
            originalEnemy.RegisterGate(gate);
            CreateClonesRandomly(originalUnit, originalEnemy, gate);
        }
    }

    // --- YENİ VE "GÜVENLİ RASTGELE" KLONLAMA METODU ---
    private void CreateClonesRandomly<T>(GameObject originalUnit, T originalController, MultiplierGate gate) where T : MonoBehaviour
    {
        int clonesToCreate = multiplicationFactor - 1;
        if (clonesToCreate <= 0) return;

        // 1. Güvenli noktalar listemizi rastgele karıştırıyoruz.
        List<Vector3> shuffledPoints = relativeSpawnPoints.OrderBy(p => Random.value).ToList();

        // 2. Yaratacağımız klon sayısı kadar, karıştırılmış listenin başından nokta seçiyoruz.
        for (int i = 0; i < clonesToCreate && i < shuffledPoints.Count; i++)
        {
            // Seçilen göreceli noktayı al.
            Vector3 relativePos = shuffledPoints[i];

            // Bu noktayı, birimler arası boşlukla çarp ve orijinal birimin yönüne göre döndür.
            Vector3 offset = originalUnit.transform.rotation * relativePos * spawnSpacing;

            // Son spawn pozisyonunu hesapla.
            Vector3 spawnPosition = originalUnit.transform.position + offset;

            // Klonu oluştur ve görevini ata.
            GameObject clone = Instantiate(originalUnit, spawnPosition, originalUnit.transform.rotation);
            T cloneController = clone.GetComponent<T>();

            if (cloneController != null)
            {
                if (cloneController is FriendController friendClone && originalController is FriendController fOriginal)
                {
                    friendClone.Initialize(fOriginal.target, fOriginal.speed, fOriginal.damage);

                    friendClone.RegisterGate(gate);
                }
                else if (cloneController is EnemyController enemyClone && originalController is EnemyController eOriginal)
                {
                    enemyClone.Initialize(eOriginal.target, eOriginal.speed, eOriginal.damage);
                    enemyClone.RegisterGate(gate);
                }
            }
        }
    }
}
                        

