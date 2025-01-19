using UnityEngine;

[CreateAssetMenu(fileName = "Bubble", menuName = "ScriptableObjects/Bubble")]
public class Bubble : ItemBase
{
    public int Health;
    public int RewardOnHurt;
    public int RewardOnDeath;
}
