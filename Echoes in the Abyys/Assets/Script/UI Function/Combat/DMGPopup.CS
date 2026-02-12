using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float moveY = 50f;
    public float duration = 1f;

    public void Setup(int value, Color color, bool isHeal = false)
    {
        text.text = value.ToString();
        text.color = color;

        transform.localScale = Vector3.zero;

        if (isHeal)
        {
            transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
        }
        else
        {
            transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        }

        transform.DOMoveY(transform.position.y + moveY, duration)
            .SetEase(Ease.OutCubic);

        text.DOFade(0f, duration)
            .OnComplete(() => Destroy(gameObject));
    }
}
