using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1f;

    private static int sortingOrder;
    public DamagePoolSO damagePool;

    [SerializeField] private Color textColor;
    [SerializeField] private float initScale;
    private float disappearTimer;
    private Vector3 moveVector;

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * (8f * Time.deltaTime);

        const float scaleAmount = 0.1f;
        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
            transform.localScale += Vector3.one * (scaleAmount * Time.deltaTime);
        else
            transform.localScale -= Vector3.one * (scaleAmount * Time.deltaTime);

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            const float disappearSpeed = 3f;
            var tmProColor = textMesh.color;
            tmProColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = tmProColor;

            if (tmProColor.a < 0) DestroySelf();
        }
    }

    public void SetUp(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());

        textMesh.transform.localScale = Vector3.one * initScale;
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.7f, 1) * 6f;
    }

    private void DestroySelf()
    {
        damagePool?.Return(this);
    }
}