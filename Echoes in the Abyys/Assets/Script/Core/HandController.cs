using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandController : MonoBehaviour
{
    public DeckManager deckManager;
    public TurnManager turnManager;
    public EnemyManager enemyManager;

    public int maxHandSize = 4;
    public int maxSelect = 2;
    public float resolveDelay = 0.5f;
    public bool inputLocked = false;

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
        return !isResolving && !inputLocked && selected.Count < maxSelect;
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

        if (enemyManager == null || turnManager == null)
        {
            Debug.LogError("HandController: EnemyManager or TurnManager not assigned!");
            yield break;
        }

        bool isChain = selected.Count >= 2 &&
                       ChainResolver.CheckChain(selected[0], selected[1]);

        var target = enemyManager.GetTarget();

        if (target == null)
        {
            Debug.Log("No enemies alive!");
        }
        else
        {
            foreach (var card in selected)
            {
                CardEffectResolver.Resolve(card, target.health, isChain);
                deckManager.ReturnToBottom(card);
            }
        }

        selected.Clear();

        OnResolveFinish?.Invoke();

        while (hand.Count < maxHandSize)
            hand.Add(deckManager.DrawCard());

        isResolving = false;

        turnManager.EndPlayerTurn();
    }

    public void SetInputLock(bool value)
    {
        inputLocked = value;
    }
}
