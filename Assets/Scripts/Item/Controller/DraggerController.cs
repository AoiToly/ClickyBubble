using UnityEngine;

public class DraggerController : ToolController
{
    [SerializeField] private float _duration;

    float _timer;

    protected override void Update()
    {
        base.Update();

        if (Energy.Value <= 0)
        {
            if (DataManager.Instance.Dragger.Value)
            {
                DataManager.Instance.Dragger.Value = false;
            }
            return;
        }

        if (!DataManager.Instance.Dragger.Value)
        {
            DataManager.Instance.Dragger.Value = true;
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
        DataManager.Instance.Dragger.Value = false;
    }
}
