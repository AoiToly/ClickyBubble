using R3;
using System.Collections.Generic;

public class DataManager : MonoSingleton<DataManager>
{
    public ReactiveProperty<float> LevelTimer = new();
    public ReactiveProperty<int> LevelScore = new();
    public ReactiveProperty<LevelConfig> CurrentLevel = new();
    public List<Tool> Tools = new();
    public ReactiveProperty<bool> DoubleClick = new(false);
    public ReactiveProperty<bool> Dragger = new(false);
}
