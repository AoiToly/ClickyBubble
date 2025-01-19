using UnityEngine;

public class DoubleClick : ToolController
{
    [SerializeField] private float _duration;

    float _timer;

    protected override void Update()
    {
        base.Update();

        if (Energy.Value <= 0)
        {
            if (DataManager.Instance.DoubleClick.Value)
            {
                DataManager.Instance.DoubleClick.Value = false;
            }
            return;
        }

        if (!DataManager.Instance.DoubleClick.Value)
        {
            DataManager.Instance.DoubleClick.Value = true;
        }

        _timer += Time.deltaTime;

        if (_timer > _duration)
        {
            Energy.Value--;
            _timer = 0;
        }
    }

    private void OnDestroy()
    {
        DataManager.Instance.DoubleClick.Value = false;
    }
}
