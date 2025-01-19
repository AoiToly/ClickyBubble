using UnityEngine;
using UnityEngine.EventSystems;

public class TankController : ToolController
{
    [SerializeField] private BulletController _bulletTemplate;
    [SerializeField] private float _shootInterval = 10;
    [SerializeField] private float _bulletSpeed = 1;
    private float _timer = 0f;

    protected override void Update()
    {
        base.Update();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(_bulletTemplate, transform.position, Quaternion.identity, LevelController.Instance.WorldContainer).GetComponent<Rigidbody2D>().linearVelocity = Vector2.one * _bulletSpeed;
        Instantiate(_bulletTemplate, transform.position, Quaternion.identity, LevelController.Instance.WorldContainer).GetComponent<Rigidbody2D>().linearVelocity = -Vector2.one * _bulletSpeed;
        Instantiate(_bulletTemplate, transform.position, Quaternion.identity, LevelController.Instance.WorldContainer).GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, -1) * _bulletSpeed;
        Instantiate(_bulletTemplate, transform.position, Quaternion.identity, LevelController.Instance.WorldContainer).GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1, 1) * _bulletSpeed;
    }
}
