namespace BallRebound.scripts.cqrs.enemy.@event;

/// <summary>
///     敌人生成事件，当新敌人被实例化到场景中时触发
/// </summary>
public sealed class EnemySpawnedEvent
{
    /// <summary>
    ///     敌人类型标识（EnemyNormal / EnemyTracker / EnemyPatrol / EnemyBounce）
    /// </summary>
    public required string EnemyType { get; init; }

    /// <summary>
    ///     生成位置（世界坐标）
    /// </summary>
    public required Godot.Vector2 Position { get; init; }
}
