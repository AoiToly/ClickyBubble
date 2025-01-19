using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _damage = 1;

    public void SetData(int damage)
    {
        _damage = damage;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null)
        {
            if (col.gameObject.TryGetComponent(out ToolController tool) && tool.Config.name == "Tank")
            {
                return;
            }
            if (col.gameObject.TryGetComponent<BubbleController>(out BubbleController bubble))
            {
                bubble.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
