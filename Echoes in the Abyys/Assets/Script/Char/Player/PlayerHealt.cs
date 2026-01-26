using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public event Action<int> OnDamaged;
    public event Action OnDeath;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"Player took {amount} damage. HP: {currentHP}/{maxHP}");
        OnDamaged?.Invoke(currentHP);

        if (currentHP == 0)
        {
            Debug.Log("PLAYER DEFEATED");
            OnDeath?.Invoke();
        }
    }
}
