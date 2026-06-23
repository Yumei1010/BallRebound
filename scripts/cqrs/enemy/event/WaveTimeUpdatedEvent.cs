namespace BallRebound.scripts.cqrs.enemy.@event;

/// <summary>
///     波次时间更新事件，每帧推进当前波次的计时
/// </summary>
public sealed class WaveTimeUpdatedEvent
{
    /// <summary>
    ///     当前波次已流逝的时间（秒）
    /// </summary>
    public required float ElapsedTime { get; init; }
}
