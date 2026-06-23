namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     玩家发射事件，当玩家从瞄准状态发射球体时触发
/// </summary>
public sealed class PlayerLaunchedEvent
{
    /// <summary>
    ///     发射方向（世界坐标系中的单位向量）
    /// </summary>
    public required Godot.Vector2 Direction { get; init; }

    /// <summary>
    ///     发射速度大小
    /// </summary>
    public required float Speed { get; init; }

    /// <summary>
    ///     此次发射消耗的能量
    /// </summary>
    public required float EnergyCost { get; init; }
}
