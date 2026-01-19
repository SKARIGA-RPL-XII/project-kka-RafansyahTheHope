using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image background;

    private CardInstance boundCard;
    private HandController handController;
    private Transform selectArea;

    public void Bind(CardInstance card, HandController controller, Transform selectParent)
    {
        boundCard = card;
        handController = controller;
        selectArea = selectParent;

        background.color = GetColor(card.data.element);
    }

    public void OnClick()
    {
        if (!handController.CanSelect()) return;

        handController.SelectCard(boundCard);
        
        transform.SetParent(selectArea, false);
    }

    Color GetColor(ElementType element)
    {
        return element switch
        {
            ElementType.Fire => Color.red,
            ElementType.Nature => Color.green,
            ElementType.Physical => Color.gray,
            ElementType.Ghost => Color.magenta,
            ElementType.Electro => Color.cyan,
            _ => Color.white
        };
    }
}
