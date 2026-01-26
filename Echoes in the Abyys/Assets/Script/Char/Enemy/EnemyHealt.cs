using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 30;
    public int currentHP;

    public event Action OnDeath;
    public event Action<int> OnDamaged;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHP}/{maxHP}");
        OnDamaged?.Invoke(currentHP);

        if (currentHP == 0)
        {
            Debug.Log($"{gameObject.name} defeated!");
            OnDeath?.Invoke();
        }
    }
}
