using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandController : MonoBehaviour
{
    [Header("References")]
    public DeckManager deckManager;
    public TurnManager turnManager;
    public EnemyManager enemyManager;
    public ArtifactManager artifactManager;   // 🔥 Tambahan

    [Header("Hand Rules")]
    public int maxHandSize = 4;
    public int baseMaxSelect = 2;              // 🔥 Base rule
    public float resolveDelay = 0.5f;

    [Header("Input")]
    public bool inputLocked = false;

    [Header("Runtime")]
    public List<CardInstance> hand = new();
    public List<CardInstance> selected = new();

    public event Action OnHandChanged;
    public event Action OnResolveStart;
    public event Action OnResolveFinish;

    bool isResolving = false;

    void Start()
    {
        InitHand();
        OnHandChanged?.Invoke();
    }

    void InitHand()
    {
        hand.Clear();

        for (int i = 0; i < maxHandSize; i++)
        {
            var card = deckManager.DrawCard();
            if (card != null)
                hand.Add(card);
        }
    }
    int GetMaxSelect()
    {
        if (artifactManager == null)
            return baseMaxSelect;

        return artifactManager.ModifyMaxCardsPerTurn(baseMaxSelect);
    }

    public bool CanSelect()
    {
        if (turnManager.CurrentState != CombatState.PlayerTurn)
            return false;

        return !isResolving &&
               !inputLocked &&
               selected.Count < GetMaxSelect();
    }

    public void SelectCard(CardInstance card)
    {
        if (!CanSelect() && !selected.Contains(card))
            return;

        if (selected.Contains(card))
        {
            selected.Remove(card);
            hand.Add(card);
            OnHandChanged?.Invoke();
            return;
        }

        if (!hand.Contains(card))
            return;

        hand.Remove(card);
        selected.Add(card);

        OnHandChanged?.Invoke();

        if (selected.Count >= GetMaxSelect())
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
            Debug.LogError("HandController: Missing reference!");
            isResolving = false;
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
                CardEffectResolver.Resolve(
                    card,
                    target.health,
                    isChain,
                    artifactManager,
                    turnManager.player
                );

                artifactManager?.OnCardPlayed(card);

                deckManager.ReturnToBottom(card);
            }
        }

        selected.Clear();

        OnResolveFinish?.Invoke();

        while (hand.Count < maxHandSize)
        {
            var draw = deckManager.DrawCard();
            if (draw != null)
                hand.Add(draw);
        }

        OnHandChanged?.Invoke();

        isResolving = false;

        turnManager.EndPlayerTurn();
    }

    public void SetInputLock(bool value)
    {
        inputLocked = value;
    }

    public void DrawOneCard()
    {
        if (hand.Count >= maxHandSize)
            return;

        var draw = deckManager.DrawCard();
        if (draw != null)
        {
            hand.Add(draw);
            OnHandChanged?.Invoke();
        }
    }
}
