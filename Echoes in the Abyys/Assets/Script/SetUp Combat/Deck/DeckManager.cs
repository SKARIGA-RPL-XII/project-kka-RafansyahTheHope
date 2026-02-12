using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private Queue<CardInstance> deckQueue = new();

    void Start()
    {
        BuildDeckFromDatabase();
    }

    void BuildDeckFromDatabase()
    {
        deckQueue.Clear();

        if (DatabaseManager.Instance == null)
        {
            Debug.LogError("DatabaseManager not found!");
            return;
        }

        if (CardDatabase.Instance == null)
        {
            Debug.LogError("CardDatabase not found!");
            return;
        }

        var deckEntries = DatabaseManager.Instance.GetDeck();

        if (deckEntries.Count == 0)
        {
            Debug.LogWarning("Deck kosong di database!");
            return;
        }

        List<CardInstance> temp = new();

        foreach (var entry in deckEntries)
        {
            CardData data = CardDatabase.Instance.GetCardById(entry.CardId);

            if (data != null)
                temp.Add(new CardInstance(data));
        }

        Shuffle(temp);

        foreach (var card in temp)
            deckQueue.Enqueue(card);
    }

    void Shuffle(List<CardInstance> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }

    public CardInstance DrawCard()
    {
        if (deckQueue.Count == 0)
        {
            Debug.LogWarning("Deck empty!");
            return null;
        }

        return deckQueue.Dequeue();
    }

    public void ReturnToBottom(CardInstance card)
    {
        deckQueue.Enqueue(card);
    }
}
