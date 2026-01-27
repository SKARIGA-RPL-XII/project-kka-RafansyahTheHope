using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 30;
    public int currentHP;

    public event Action<int> OnDamaged;
    public event Action OnDeath;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHP}/{maxHP}");

        OnDamaged?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        OnDeath?.Invoke();

        gameObject.SetActive(false);
    }
}
