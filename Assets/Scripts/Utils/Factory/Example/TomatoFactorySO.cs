using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Bug/Factory/TomatoFactory")]
public class TomatoFactorySO : FactorySO<Tomato>
{
    public Tomato tomato;

    public override Tomato Create()
    {
        return Instantiate(tomato);
    }
}