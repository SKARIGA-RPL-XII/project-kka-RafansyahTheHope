using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBarView : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;

    int maxHP;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHPBarView: PlayerHealth not assigned!");
            return;
        }

        maxHP = playerHealth.maxHP;

        playerHealth.OnDamaged += UpdateBar;
        playerHealth.OnDeath += OnDeath;

        UpdateBar(playerHealth.currentHP);
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDamaged -= UpdateBar;
            playerHealth.OnDeath -= OnDeath;
        }
    }

    void UpdateBar(int currentHP)
    {
        float percent = (float)currentHP / maxHP;
        fillImage.fillAmount = percent;

        // BONUS: warna dinamis
        fillImage.color = Color.Lerp(Color.red, Color.green, percent);
    }

    void OnDeath()
    {
        Debug.Log("PLAYER DEFEATED - HP bar hidden");
        gameObject.SetActive(false);
    }
}
