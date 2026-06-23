namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     能量里程碑事件，当能量达到 100/200/300 标记点时触发
/// </summary>
public sealed class EnergyMilestoneEvent
{
    /// <summary>
    ///     里程碑级别（1=100, 2=200, 3=300）
    /// </summary>
    public required int MilestoneLevel { get; init; }
}
