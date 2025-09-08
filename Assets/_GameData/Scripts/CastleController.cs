using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int currentHealth;
    
    // Sınıfın kendi "başlangıç canı" değişkeni.
    private int startingHealth; 

    [SerializeField] private bool isEnemyCastle;

    public void Initialize(int newStartingHealth)
    {
        startingHealth = newStartingHealth;
        currentHealth = newStartingHealth;
        
        // Başlangıçta can barını %100 dolu olarak güncelle.
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    
    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            float healthPercentage = (float)currentHealth / startingHealth;
            
            // Slider'ın değerini bu yüzdeye eşitle.
            healthSlider.value = healthPercentage;
        }
    }

    private void Die()
    {
        // Kale yıkıldığında olacaklar...
        if (isEnemyCastle)
        {
            Debug.Log("oyunu kazandınız");
            EventManager.TriggerGameWin();
        }
        else
        {
            Debug.Log("oyunu kaybettiniz");
            EventManager.TriggerGameOver();
        }
        
        gameObject.SetActive(false);
        
    }
}