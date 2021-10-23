using UnityEngine;

[CreateAssetMenu(menuName = "Bug/Cursor/CursorData")]
public class CursorDataSO : ScriptableObject
{
    public Texture2D cursor_Default;
    public Texture2D cursor_Attack;
    public Texture2D cursor_PickUp;
    public Texture2D cursor_Talk;
    
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
}