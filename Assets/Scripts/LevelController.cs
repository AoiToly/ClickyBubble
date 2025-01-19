using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoSingleton<LevelController>
{
    public Transform WorldContainer;
    [SerializeField] private List<LevelConfig> _configs = new();
    [SerializeField] private Transform _borderTop;
    [SerializeField] private Transform _borderBottom;
    [SerializeField] private Transform _borderLeft;
    [SerializeField] private Transform _borderRight;
    [SerializeField] private List<Tool> _toolConfigs = new();
    private List<ItemController> _itemControllers = new();
    private List<BubbleController> _bubbleControllers = new();
    private bool _inProgress = false;
    private LevelConfig _currentLevel;
    private int _wholeWeight;

    private void Update()
    {
        if (!_inProgress)
        {
            return;
        }
        DataManager.Instance.LevelTimer.Value -= Time.deltaTime;
        if (DataManager.Instance.LevelScore.Value >= _currentLevel.TargetScore)
        {
            _inProgress = false;
            var toolConfigs = _toolConfigs.Where(tool => DataManager.Instance.Tools.Count(t => t == tool) < tool.Limit).ToList();
            var a = Random.Range(0, toolConfigs.Count);
            var t = toolConfigs[a];
            toolConfigs[a] = toolConfigs[0];
            toolConfigs[0] = t;
            a = Random.Range(1, toolConfigs.Count);
            t = toolConfigs[a];
            toolConfigs[a] = toolConfigs[1];
            toolConfigs[1] = t;
            a = Random.Range(2, toolConfigs.Count);
            t = toolConfigs[a];
            toolConfigs[a] = toolConfigs[2];
            toolConfigs[2] = t;
            UIManager.Instance.ShowWinPanel(toolConfigs[0], toolConfigs[1], toolConfigs[2]);
            return;
        }
        if (DataManager.Instance.LevelTimer.Value < 0)
        {
            _inProgress = false;
            UIManager.Instance.ShowDefeatPanel();
            return;
        }

        if (_bubbleControllers.Count < _currentLevel.ItemCount)
        {
            SpawnBubble();
        }
    }

    public void StartLevel()
    {
        DataManager.Instance.Tools.Clear();
        StartLevel(_configs[0]);
    }

    public void StartNextLevel()
    {
        int index = _configs.IndexOf(_currentLevel);
        index++;
        index %= _configs.Count;
        StartLevel(_configs[index]);
    }

    private void StartLevel(LevelConfig config)
    {
        for (int i = 0; i < WorldContainer.childCount; i++)
        {
            Destroy(WorldContainer.GetChild(i).gameObject);
        }
        _itemControllers.Clear();
        _bubbleControllers.Clear();
        UIManager.Instance.DestroyAllViews();

        _currentLevel = config;

        DataManager.Instance.LevelTimer.Value = _currentLevel.Time;
        DataManager.Instance.CurrentLevel.Value = config;
        DataManager.Instance.LevelScore.Value = 0;

        _wholeWeight = 0;
        foreach (var i in config.Bubbles)
        {
            _wholeWeight += i.Weight;
        }
        for (int i = 0; i < config.ItemCount; i++)
        {
            SpawnBubble();
        }
        foreach (var item in DataManager.Instance.Tools)
        {
            SpawnItem(item);
        }
        _inProgress = true;
    }

    public void TryAgain()
    {
        StartLevel(_currentLevel);
    }

    public void SpawnBubble()
    {
        var weight = Random.Range(0, _wholeWeight);
        int j = 0;
        Bubble item = null;
        while (j < _currentLevel.Bubbles.Count)
        {
            weight -= _currentLevel.Bubbles[j].Weight;
            item = _currentLevel.Bubbles[j].Config;
            j++;
            if (weight < 0)
            {
                break;
            }
        }
        SpawnBubble(item);
    }

    public void SpawnBubble(Bubble bubble)
    {
        var obj = Instantiate(bubble.Prefab, WorldContainer).GetComponent<BubbleController>();
        obj.transform.position = new Vector2(Random.Range(_borderLeft.position.x + 0.5f, _borderRight.position.x - 0.5f),
            Random.Range(_borderBottom.position.y + 0.5f, _borderTop.position.y - 0.5f));
        _bubbleControllers.Add(obj);
        obj.AddComponent<ItemAppear>();
    }

    public void SpawnItem(ItemBase item)
    {
        var obj = Instantiate(item.Prefab, WorldContainer).GetComponent<ItemController>();
        obj.transform.position = new Vector2(Random.Range(_borderLeft.position.x + 0.5f, _borderRight.position.x - 0.5f),
            Random.Range(_borderBottom.position.y + 0.5f, _borderTop.position.y - 0.5f));
        _itemControllers.Add(obj);
        obj.AddComponent<ItemAppear>();
    }

    public void DestroyBubble(BubbleController bubble)
    {
        _bubbleControllers.Remove(bubble);
        Destroy(bubble.gameObject);
    }

    public bool InBorder(Vector2 position)
    {
        if (position.x < _borderLeft.position.x + 1f || position.x > _borderRight.position.x - 1f
            || position.y < _borderBottom.position.y + 1f || position.y > _borderTop.position.y - 1f)
        {
            return false;
        }
        return true;
    }
}
