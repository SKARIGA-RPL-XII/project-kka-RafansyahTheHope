using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HandController : MonoBehaviour
{
    public DeckManager deckManager;
    public EnemyHealth enemyHealth;

    public int maxHandSize = 4;
    public int maxSelect = 2;
    public float resolveDelay = 0.5f;

    public List<CardInstance> hand = new();
    public List<CardInstance> selected = new();

    public event Action OnResolveStart;  
    public event Action OnResolveFinish; 

    bool isResolving = false;

    void Start()
    {
        InitHand();
    }

    void InitHand()
    {
        hand.Clear();
        for (int i = 0; i < maxHandSize; i++)
            hand.Add(deckManager.DrawCard());
    }

    public bool CanSelect()
    {
        return !isResolving && selected.Count < maxSelect;
    }

    // === CLICK CARD ===
    public void SelectCard(CardInstance card)
    {
        if (!CanSelect()) return;
        if (!hand.Contains(card)) return;

        hand.Remove(card);
        selected.Add(card);

        if (selected.Count == maxSelect)
        {
            StartCoroutine(ResolveWithDelay());
        }
    }

    IEnumerator ResolveWithDelay()
    {
        isResolving = true;

        OnResolveStart?.Invoke();

        yield return new WaitForSeconds(resolveDelay);

        bool isChain = ChainResolver.CheckChain(selected[0], selected[1]);

        foreach (var card in selected)
        {
            CardEffectResolver.Resolve(card, enemyHealth, isChain);

            deckManager.ReturnToBottom(card);
        }

        selected.Clear();

        OnResolveFinish?.Invoke();

        while (hand.Count < maxHandSize)
            hand.Add(deckManager.DrawCard());

        isResolving = false;
    }

}
