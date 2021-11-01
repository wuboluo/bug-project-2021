using UnityEngine;

public class DescriptionBaseSO : SerializableScriptableObject
{
    [TextArea] public string description;
}

public class DescriptionScriptableObject : ScriptableObject
{
    [TextArea] public string description;
}