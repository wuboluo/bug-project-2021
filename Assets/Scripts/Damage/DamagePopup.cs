using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public DamagePoolSO damagePool;

    public Color textColor;
    private TextMeshProUGUI textMesh;
    float disappearTimer;
    private const float maxDisapperTime = 0.5f;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void SetUp(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textMesh.color = textColor;

        textMesh.fontSize = 50;
        transform.localScale = Vector3.one;
        
        disappearTimer = maxDisapperTime;
        moveVector = new Vector3(0.7f, 1) * 60f;
    }

    private void Update()
    {
        transform.localPosition += moveVector * Time.deltaTime;
        moveVector -= moveVector * (8 * Time.deltaTime);

        if (disappearTimer > maxDisapperTime * 0.5f)
            transform.localScale += Vector3.one * (1 * Time.deltaTime);
        else
            transform.localScale -= Vector3.one * (2 * Time.deltaTime);

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            const float disappearSpeed = 5f;
            var tmProColor = textMesh.color;
            tmProColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = tmProColor;

            if (tmProColor.a < 0) DestroySelf();
        }
    }

    private void DestroySelf()
    {
        damagePool?.Return(this);
    }
}