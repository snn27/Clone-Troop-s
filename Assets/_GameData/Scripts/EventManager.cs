using System;
using UnityEngine;

public static class EventManager
{
    public static event Action OnSpawnFriendRequested;
    public static event Action OnSpawnEnemyRequested;
    public static event Action OnGameOver;
    public static event Action OnGameWin;
    
    public static event Action OnNextLevelRequested;
    public static event Action OnRestartLevelRequested;
    public static event Action<GameObject, MultiplierGate> OnUnitMultiplyRequested;

    public static void TriggerFriendSpawn() => OnSpawnFriendRequested?.Invoke();
    public static void TriggerEnemySpawn() => OnSpawnEnemyRequested?.Invoke();
    public static void TriggerGameOver() => OnGameOver?.Invoke();
    public static void TriggerGameWin() => OnGameWin?.Invoke();
    public static void TriggerNextLevel() => OnNextLevelRequested?.Invoke();
    public static void TriggerRestartLevel() => OnRestartLevelRequested?.Invoke();
    public static void TriggerUnitMultiply(GameObject originalUnit, MultiplierGate requestingGate) => OnUnitMultiplyRequested?.Invoke(originalUnit, requestingGate);
}
