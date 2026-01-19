using UnityEngine;

public class HandView : MonoBehaviour
{
    public HandController handController;
    public CardView cardPrefab;
    public Transform handParent;
    public Transform selectParent;

    void Update()
    {
        RefreshHand();
    }

    void RefreshHand()
    {
        if (handParent.childCount == handController.hand.Count)
            return;

        foreach (Transform child in handParent)
            Destroy(child.gameObject);

        foreach (var card in handController.hand)
        {
            CardView view = Instantiate(cardPrefab, handParent);
            view.Bind(card, handController, selectParent);
        }
    }
}
