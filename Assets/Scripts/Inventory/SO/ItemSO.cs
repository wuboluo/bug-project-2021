using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Bug/Inventory/Item")]
public class ItemSO : SerializableScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private Sprite _previewImage;

    [SerializeField] private GameObject _prefab;
}