using UnityEngine;

public class ChainResolver
{
    public static bool CheckChain(CardInstance first, CardInstance second)
    {
        if (first == null || second == null) return false;

        return first.data.element == second.data.element;
    }
}
