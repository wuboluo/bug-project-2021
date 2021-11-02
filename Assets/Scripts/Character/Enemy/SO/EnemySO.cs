using UnityEngine;

[CreateAssetMenu(menuName = "Bug/Enemy/NewEnemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int maxHp;
    public int defaultAtk;
    public int defaultDef;
    public int defaultSpeed;
}