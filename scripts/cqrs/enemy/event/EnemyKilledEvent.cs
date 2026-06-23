namespace BallRebound.scripts.cqrs.enemy.@event;

/// <summary>
///     敌人死亡事件，当敌人被玩家击杀时触发
/// </summary>
public sealed class EnemyKilledEvent
{
    /// <summary>
    ///     被击杀的敌人类型标识
    /// </summary>
    public required string EnemyType { get; init; }

    /// <summary>
    ///     击杀位置（世界坐标）
    /// </summary>
    public required Godot.Vector2 Position { get; init; }

    /// <summary>
    ///     此次击杀的基础分数
    /// </summary>
    public required int BaseScore { get; init; }
}
