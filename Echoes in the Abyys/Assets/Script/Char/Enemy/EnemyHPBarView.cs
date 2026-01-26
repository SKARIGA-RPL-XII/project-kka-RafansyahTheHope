using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBarView : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Image fillImage;

    int maxHP;

    void Start()
    {
        maxHP = enemyHealth.maxHP;

        enemyHealth.OnDamaged += UpdateBar;
        enemyHealth.OnDeath += OnDeath;

        UpdateBar(enemyHealth.currentHP);
    }

    void UpdateBar(int currentHP)
    {
        float percent = (float)currentHP / maxHP;
        fillImage.fillAmount = percent;
    }

    void OnDeath()
    {
        Debug.Log("Enemy defeated - HP bar hidden");
        gameObject.SetActive(false);
    }
}
