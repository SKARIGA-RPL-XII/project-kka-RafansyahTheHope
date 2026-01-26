using UnityEngine;

public class SelectView : MonoBehaviour
{
    public HandController handController;

    void OnEnable()
    {
        handController.OnResolveFinish += Clear;
    }

    void OnDisable()
    {
        handController.OnResolveFinish -= Clear;
    }

    void Clear()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
