namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     玩家击杀敌人事件，当玩家以高于击杀阈值的速度撞击敌人时触发
/// </summary>
public sealed class PlayerKilledEnemyEvent
{
    /// <summary>
    ///     被击杀的敌人类型标识（如 EnemyNormal、EnemyTracker）
    /// </summary>
    public required string EnemyType { get; init; }

    /// <summary>
    ///     撞击时玩家的速度大小
    /// </summary>
    public required float Speed { get; init; }

    /// <summary>
    ///     击杀发生的位置（世界坐标）
    /// </summary>
    public required Godot.Vector2 Position { get; init; }
}
