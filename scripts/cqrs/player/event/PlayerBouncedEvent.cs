namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     玩家反弹事件，当玩家球体撞击墙壁等非敌人物体时触发
/// </summary>
public sealed class PlayerBouncedEvent
{
    /// <summary>
    ///     反弹位置（世界坐标）
    /// </summary>
    public required Godot.Vector2 ImpactPosition { get; init; }

    /// <summary>
    ///     反弹前的速度向量
    /// </summary>
    public required Godot.Vector2 Velocity { get; init; }

    /// <summary>
    ///     当前反弹计数（自上次击杀后的墙壁反弹次数）
    /// </summary>
    public required int BounceCount { get; init; }

    /// <summary>
    ///     此次反弹是否导致连击丢失（反弹次数达上限且未击杀）
    /// </summary>
    public required bool IsComboLost { get; init; }
}
