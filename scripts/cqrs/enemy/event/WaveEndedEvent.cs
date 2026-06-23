namespace BallRebound.scripts.cqrs.enemy.@event;

/// <summary>
///     波次结束事件，当当前波次持续时间到期时触发
/// </summary>
public sealed class WaveEndedEvent
{
    /// <summary>
    ///     刚结束的波次编号
    /// </summary>
    public required int WaveNumber { get; init; }
}
