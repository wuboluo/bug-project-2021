using UnityEngine;

public class Unit2D : MonoBehaviour
{
    public CursorDataSO cursorData;
    public UnitType type;
    Texture2D texture;

    private void Start()
    {
        texture = type switch
        {
            UnitType.Npc => cursorData.cursor_Talk,
            UnitType.Enemy => cursorData.cursor_Attack,
            UnitType.Item => cursorData.cursor_PickUp,
            _ => texture
        };
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(texture, cursorData.hotSpot, cursorData.cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorData.cursor_Default, Vector2.zero, cursorData.cursorMode);
    }
}

public enum UnitType
{
    Item,
    Npc,
    Enemy
}