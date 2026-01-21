using UnityEngine;

public enum CardType { Attack, Buff, Debuff, Heal }
public enum ElementType { Physical, Nature, Fire, Ghost, Electro }

[CreateAssetMenu(menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Info")]
    public string cardName;
    public CardType cardType;
    public ElementType element;

    [Header("Combat Stats")]
    public int baseDamage = 0;          
    public int healAmount = 0;        

    [Header("Chain")]
    public float chainMultiplier = 1.5f;

    [Header("Status (Optional / Next Step)")]
    public int dotAmount = 0;
    public int dotTurns = 0;
}
