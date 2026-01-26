using UnityEngine;

public class EnemyClickTarget : MonoBehaviour
{
    public EnemyController controller;
    public EnemyManager manager;

    void Awake()
    {
        if (controller == null)
            controller = GetComponent<EnemyController>();
    }

    void OnMouseDown()
    {
        if (manager != null)
            manager.SelectTarget(controller);
    }
}
