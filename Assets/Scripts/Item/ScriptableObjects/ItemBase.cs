using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public string Description;
    public Sprite Icon;
    public Texture2D CursorTexture;
    public Vector2 CursorHotspot;
}
