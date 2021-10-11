using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    private static readonly int DissolveColor = Shader.PropertyToID("_DissolveColor");
    private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");
    public Material material;

    private float dissolveAmount;
    private float dissolveSpeed;
    private bool isDissolving;

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            material.SetFloat(DissolveAmount, dissolveAmount);
        }
        else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            material.SetFloat(DissolveAmount, dissolveAmount);
        }
    }

    public void Dissolve(bool isStart, float _speed, Color _dissolveColor)
    {
        isDissolving = isStart;
        material.SetColor(DissolveColor, _dissolveColor);
        dissolveSpeed = _speed;
    }
}