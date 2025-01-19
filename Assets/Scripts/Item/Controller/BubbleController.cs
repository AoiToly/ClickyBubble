using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleController : ItemController, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Bubble _config;
    public Bubble Config => _config;
    protected ClickCountView _clickCountView;
    public ReactiveProperty<int> ClickCount;
    protected bool _pointerInside = false;
    protected bool _onDrag = false;
    protected float _dragTimer = 0;
    protected bool _dragTimerOn = false;
    protected bool _isDraging = false;
    protected Vector3 _lastMousePos;
    protected float _dragDistance = 0;

    private Tween _fadeTween;
    private Tween _clickTween;

    protected override void Start()
    {
        base.Start();
        ClickCount = new(_config.Health);
        _clickCountView = UIManager.Instance.SpawnClickCountText();
        _clickCountView.SetData(this);
        _clickCountView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        _rb = GetComponent<Rigidbody2D>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;

        _fadeTween = spriteRenderer.DOFade(0.3f, Random.Range(0.5f, 1.2f)).OnComplete(() =>
        {
            _fadeTween = spriteRenderer.DOFade(1, 1.2f).SetLoops(200, LoopType.Yoyo);
        });
    }

    protected virtual void Update()
    {
        if (_clickCountView != null)
        {
            _clickCountView.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }

        if (Input.GetMouseButtonDown(0) && _pointerInside)
        {
            if (!DataManager.Instance.Dragger.Value)
            {
                OnClick();
            }
            _dragTimer = 0;
            _dragTimerOn = true;
            _lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _dragTimerOn = false;
            _dragTimer = 0;
            _isDraging = false;
        }
        if (_dragTimerOn)
        {
            _dragTimer += Time.deltaTime;
            if (_dragTimer > 0.01f)
            {
                _isDraging = true;
                _lastMousePos = Input.mousePosition;
                _dragDistance = 0;
                _dragTimerOn = false;
            }
        }
        if (_isDraging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (DataManager.Instance.Dragger.Value)
            {
                _dragDistance += Time.deltaTime;
                _lastMousePos = Input.mousePosition;
                if (_dragDistance > 0.5f)
                {
                    _dragDistance = 0;
                    OnClick();
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        damage = Mathf.Min(damage, ClickCount.Value);
        ClickCount.Value -= damage;
        DataManager.Instance.LevelScore.Value += _config.RewardOnHurt * damage;
        if (ClickCount.Value == 0)
        {
            DataManager.Instance.LevelScore.Value += _config.RewardOnDeath;
            OnDeath();
        }
    }

    protected virtual void OnClick()
    {
        _clickTween?.Kill();
        _clickTween = transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 8, 0.3f);
        TakeDamage(1);
        if (DataManager.Instance.DoubleClick.Value)
        {
            TakeDamage(1);
        }
    }

    protected virtual void OnDeath()
    {
        if (_clickCountView != null)
        {
            Destroy(_clickCountView.gameObject);
        }
        LevelController.Instance.DestroyBubble(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerInside = false;
    }

    private void OnDestroy()
    {
        _fadeTween?.Kill();
        _clickTween?.Kill();
    }
}

