namespace BallRebound.scripts.cqrs.score.@event;

/// <summary>
///     统计数据更新事件，当某项持久化统计数据发生变化时触发
/// </summary>
public sealed class StatsUpdatedEvent
{
    /// <summary>
    ///     统计项键名（如 total_kills、max_combo、play_time）
    /// </summary>
    public required string StatKey { get; init; }

    /// <summary>
    ///     更新后的数值
    /// </summary>
    public required float NewValue { get; init; }
}
