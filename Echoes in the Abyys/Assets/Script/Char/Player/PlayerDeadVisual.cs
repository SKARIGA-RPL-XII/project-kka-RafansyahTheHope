using UnityEngine;

public class PlayerDeathVisual : MonoBehaviour
{
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Hide()
    {
        if (sr != null)
            sr.enabled = false;
    }
}
