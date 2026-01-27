using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 200;
    public int currentHP;

    public event Action<int> OnHealthChanged;
    public event Action OnDeath;

    void Awake()
    {
        currentHP = maxHP;
    }

    void Start()
    {
        OnHealthChanged?.Invoke(currentHP);
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log($"Player took {amount} damage. HP: {currentHP}/{maxHP}");

        OnHealthChanged?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log($"Player healed {amount}. HP: {currentHP}/{maxHP}");

        OnHealthChanged?.Invoke(currentHP);
    }

    void Die()
    {
        if (currentHP > 0) return;

        Debug.Log("PLAYER DIED!");

        GetComponent<PlayerDeathVisual>()?.Hide();
        OnDeath?.Invoke();
    }
}
