using UnityEngine;

public class ThornController : ToolController
{
    [SerializeField] private int _damage = 1;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null && Energy.Value > 0 && col.gameObject.TryGetComponent<BubbleController>(out BubbleController bubble))
        {
            bubble.TakeDamage(_damage);
            Energy.Value--;
        }
    }
}
