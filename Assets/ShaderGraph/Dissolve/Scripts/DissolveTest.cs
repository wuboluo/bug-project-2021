using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    [SerializeField] private DissolveEffect dissolveEffect;

    [ColorUsageAttribute(true, true)] [SerializeField]
    private Color startDissolveColor;

    [ColorUsageAttribute(true, true)] [SerializeField]
    private Color stopDissolveColor;

    public void Show()
    {
        dissolveEffect.Dissolve(false, 0.7f, startDissolveColor);
    }

    public void Hide()
    {
        dissolveEffect.Dissolve(true, 0.7f, stopDissolveColor);
    }
}