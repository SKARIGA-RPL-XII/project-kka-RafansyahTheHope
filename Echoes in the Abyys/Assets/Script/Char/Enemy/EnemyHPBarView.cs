using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBarView : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Image fillImage;

    EnemyHealth currentHealth;
    int maxHP;

    void Start()
    {
        if (enemyManager == null)
        {
            Debug.LogError("EnemyHPBarView: EnemyManager not assigned!");
            return;
        }

        enemyManager.OnTargetChanged += SetTarget;

        var first = enemyManager.GetFirstAliveEnemy();
        if (first != null)
            SetTarget(first);
    }

    void OnDestroy()
    {
        if (enemyManager != null)
            enemyManager.OnTargetChanged -= SetTarget;

        Unsubscribe();
    }

    void SetTarget(EnemyController enemy)
    {
        if (enemy == null || enemy.health == null)
            return;

        Unsubscribe();

        currentHealth = enemy.health;
        maxHP = currentHealth.maxHP;

        currentHealth.OnDamaged += UpdateBar;
        currentHealth.OnDeath += OnDeath;

        UpdateBar(currentHealth.currentHP);
    }

    void Unsubscribe()
    {
        if (currentHealth != null)
        {
            currentHealth.OnDamaged -= UpdateBar;
            currentHealth.OnDeath -= OnDeath;
        }
    }

    void UpdateBar(int currentHP)
    {
        if (maxHP <= 0) return;

        float percent = Mathf.Clamp01((float)currentHP / maxHP);
        fillImage.fillAmount = percent;
        fillImage.color = Color.Lerp(Color.red, Color.green, percent);
    }

    void OnDeath()
    {
        fillImage.fillAmount = 0f;
    }
}
