using UnityEngine;

public enum CardType { Attack, Buff, Debuff, Heal }
public enum ElementType { Physical, Nature, Fire, Ghost, Electro }

[CreateAssetMenu(menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public ElementType element;
    public int value;
}
