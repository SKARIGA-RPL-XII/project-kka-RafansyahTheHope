using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> initialDeck; // 8 kartu

    private Queue<CardInstance> deckQueue = new();

    void Awake()
    {
        BuildDeck();
    }

    void BuildDeck()
    {
        List<CardInstance> temp = new();

        foreach (var data in initialDeck)
            temp.Add(new CardInstance(data));

        Shuffle(temp);

        deckQueue.Clear();
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
        return deckQueue.Dequeue();
    }

    public void ReturnToBottom(CardInstance card)
    {
        deckQueue.Enqueue(card);
    }
}
