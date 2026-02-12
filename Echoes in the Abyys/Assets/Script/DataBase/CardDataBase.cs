using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;

    [SerializeField] private List<CardData> allCards;

    private Dictionary<string, CardData> lookup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            lookup = allCards.ToDictionary(c => c.cardId, c => c);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public CardData GetCardById(string id)
    {
        if (lookup != null && lookup.ContainsKey(id))
            return lookup[id];

        Debug.LogError("Card ID not found: " + id);
        return null;
    }
}
