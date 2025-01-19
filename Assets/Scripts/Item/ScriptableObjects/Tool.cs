using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "ScriptableObjects/Tool")]
public class Tool : ItemBase
{
    public int Energy;
    public int Limit = 999;
}
