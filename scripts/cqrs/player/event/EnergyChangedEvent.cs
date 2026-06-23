namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     能量变化事件，当玩家能量值发生变化时触发
/// </summary>
public sealed class EnergyChangedEvent
{
    /// <summary>
    ///     新的能量值（0 - 300）
    /// </summary>
    public required float NewValue { get; init; }

    /// <summary>
    ///     能量变化量（正数为增加，负数为消耗）
    /// </summary>
    public required float Delta { get; init; }
}
