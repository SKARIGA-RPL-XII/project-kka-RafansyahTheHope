using UnityEngine;

public class HandView : MonoBehaviour
{
    public HandController handController;
    public CardView cardPrefab;

    public Transform handParent;
    public Transform selectParent;

    void Start()
    {
        handController.OnHandChanged += RefreshAll;
        RefreshAll();
    }

    void OnDestroy()
    {
        if (handController != null)
            handController.OnHandChanged -= RefreshAll;
    }

    void RefreshAll()
    {
        RefreshHand();
        RefreshSelected();
    }

    // ======================
    // HAND AREA
    // ======================
    void RefreshHand()
    {
        foreach (Transform child in handParent)
            Destroy(child.gameObject);

        foreach (var card in handController.hand)
        {
            CardView view = Instantiate(cardPrefab, handParent);
            view.Bind(card, handController, selectParent);
        }
    }

    // ======================
    // SELECT AREA
    // ======================
    void RefreshSelected()
    {
        foreach (Transform child in selectParent)
            Destroy(child.gameObject);

        foreach (var card in handController.selected)
        {
            CardView view = Instantiate(cardPrefab, selectParent);
            view.Bind(card, handController, selectParent);
        }
    }
}
