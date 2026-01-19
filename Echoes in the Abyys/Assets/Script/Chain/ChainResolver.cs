using UnityEngine;

public class ChainResolver
{
    public static void CheckChain(CardInstance first, CardInstance second)
    {
        if (first.data.element != second.data.element) return;

        Debug.Log($"CHAIN TRIGGERED: {first.data.element}");
        // nanti di sini efek chain
    }
}
