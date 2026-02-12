using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
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
        if (currentHP <= 0)
            return;

        if (amount <= 0)
            return;

        int finalDamage = amount;

        currentHP -= finalDamage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log($"{gameObject.name} took {finalDamage} damage. HP: {currentHP}/{maxHP}");

        DamagePopupManager.Instance?.ShowDamage(
            finalDamage,
            transform.position,
            Color.red
        );

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
