using UnityEngine;
using System.Collections.Generic;
public class FriendController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;
    
    private Rigidbody rb;
    private List<MultiplierGate> processedGates = new List<MultiplierGate>();
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Initialize(Transform newTarget, float newSpeed , int newDamage)
    {
        target = newTarget;
        speed = newSpeed;
        damage = newDamage;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyPlayer"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("EnemyCastle"))
        {
            CastleController castle = other.GetComponent<CastleController>();
            if (castle != null)
            {
                castle.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
    
    public bool HasBeenProcessedBy(MultiplierGate gate)
    {
        return processedGates.Contains(gate);
    }

    // Bir kapı, bu askeri (veya kopyalarını) işledikten sonra "sana damgamı vuruyorum" diyecek.
    public void RegisterGate(MultiplierGate gate)
    {
        if (!processedGates.Contains(gate))
        {
            processedGates.Add(gate);
        }
    }
}