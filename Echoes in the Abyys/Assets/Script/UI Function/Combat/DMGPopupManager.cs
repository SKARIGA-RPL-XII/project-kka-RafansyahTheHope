using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager Instance;

    public GameObject damagePopupPrefab;
    public Canvas canvas;

    void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(int amount, Vector3 worldPosition, Color color)
    {
        ShowPopup(amount, worldPosition, color, false);
    }

    public void ShowHeal(int amount, Vector3 worldPosition)
    {
        ShowPopup(amount, worldPosition, Color.green, true);
    }

    void ShowPopup(int amount, Vector3 worldPosition, Color color, bool isHeal)
    {
        if (damagePopupPrefab == null || canvas == null)
            return;

        GameObject obj = Instantiate(damagePopupPrefab, canvas.transform);

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        obj.transform.position = screenPos;

        obj.GetComponent<DamagePopup>().Setup(amount, color, isHeal);
    }
}
