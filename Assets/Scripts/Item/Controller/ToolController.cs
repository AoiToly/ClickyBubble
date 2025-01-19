using R3;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolController : ItemController, IPointerClickHandler, IDragHandler
{
    [SerializeField] protected Tool _config;
    public Tool Config => _config;
    public ReactiveProperty<int> Energy;
    private EnergyView _energyView;

    protected override void Start()
    {
        base.Start();
        Energy = new(0);
        _energyView = UIManager.Instance.SpawnEnergyView();
        _energyView.SetData(this, _config);
        _energyView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    protected virtual void Update()
    {
        if (_energyView != null)
        {
            _energyView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (Energy.Value < _config.Energy)
        {
            Energy.Value++;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (LevelController.Instance.InBorder(Camera.main.ScreenToWorldPoint(eventData.position)))
        {
            _rb.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);

        }
    }
}
