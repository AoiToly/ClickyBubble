using UnityEngine;

public class GloomShroomController : ToolController
{
    [SerializeField] private float _radius;
    [SerializeField] private SpriteRenderer _range;

    [SerializeField] private float _interval = 3;

    private ContactFilter2D _filter;
    private float _timer = 0;

    protected override void Start()
    {
        base.Start();
        _range.transform.localScale = Vector3.one * _radius;
        _filter = new ContactFilter2D();
        _filter.NoFilter();
    }

    protected override void Update()
    {
        base.Update();
        if (Energy.Value <= 0)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            _timer = 0;
            Energy.Value--;
            Collider2D[] res = new Collider2D[8];
            int count = Physics2D.OverlapCircle(transform.position, _radius, _filter, res);
            for (int i = 0; i < count; i++)
            {
                if (res[i].TryGetComponent(out BubbleController bubble))
                {
                    bubble.TakeDamage(1);
                }
            }
        }


    }
}
