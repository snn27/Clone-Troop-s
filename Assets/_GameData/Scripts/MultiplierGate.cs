using UnityEngine;

public class MultiplierGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        bool isProcessed = false;
        
        // Giren birimin daha önce bu kapıdan geçip geçmediğini kontrol et.
        if (other.TryGetComponent<FriendController>(out var friend))
        {
            isProcessed = friend.HasBeenProcessedBy(this);
        }
        else if (other.TryGetComponent<EnemyController>(out var enemy))
        {
            isProcessed = enemy.HasBeenProcessedBy(this);
        }

        // Eğer birim bu kapıdan daha önce geçmediyse...
        if (!isProcessed && (other.CompareTag("FriendPlayer") || other.CompareTag("EnemyPlayer")))
        {
            // Haber merkezine anons yap: "Bu birimi, bu kapı için çoğalt!"
            EventManager.TriggerUnitMultiply(other.gameObject, this);
        }
    }
}