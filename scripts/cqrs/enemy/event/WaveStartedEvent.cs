namespace BallRebound.scripts.cqrs.enemy.@event;

/// <summary>
///     波次开始事件，当新一波敌人生成周期开始时触发
/// </summary>
public sealed class WaveStartedEvent
{
    /// <summary>
    ///     波次编号（从 1 开始）
    /// </summary>
    public required int WaveNumber { get; init; }

    /// <summary>
    ///     波次持续时间（秒），-1 表示无限
    /// </summary>
    public required float Duration { get; init; }
}
