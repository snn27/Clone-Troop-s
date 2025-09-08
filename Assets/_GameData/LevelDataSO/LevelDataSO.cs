using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_0", menuName = "ScriptableObjects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    [Header("Castle life")]
    public int friendCastleHealth;
    public int enemyCastleHealth;
    
    [Header("Level Layout")]
    public GameObject levelDesignPrefab;
    
    [Header("Setting")]
    public float enemySpeed;
    public float friendSpeed;
    public int Damage;

}
