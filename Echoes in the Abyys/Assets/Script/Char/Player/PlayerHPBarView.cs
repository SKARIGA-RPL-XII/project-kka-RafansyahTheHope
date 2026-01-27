using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBarView : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;

    void OnEnable()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHPBarView: PlayerHealth not assigned!");
            return;
        }

        playerHealth.OnHealthChanged += UpdateBar;
        playerHealth.OnDeath += OnDeath;
    }

    void Start()
    {
        // 🔥 Force initial draw
        UpdateBar(playerHealth.currentHP);
    }

    void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateBar;
            playerHealth.OnDeath -= OnDeath;
        }
    }

    void UpdateBar(int currentHP)
    {
        if (playerHealth.maxHP <= 0) return;

        float percent = Mathf.Clamp01((float)currentHP / playerHealth.maxHP);
        fillImage.fillAmount = percent;
        fillImage.color = Color.Lerp(Color.red, Color.green, percent);
    }

    void OnDeath()
    {
        fillImage.fillAmount = 0f;
        gameObject.SetActive(false);
    }
}
