namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     玩家死亡事件，当玩家以低于击杀阈值的速度撞击敌人时触发
/// </summary>
public sealed class PlayerDiedEvent
{
    /// <summary>
    ///     死亡原因描述
    /// </summary>
    public required string DeathCause { get; init; }

    /// <summary>
    ///     死亡时的最终分数
    /// </summary>
    public required int FinalScore { get; init; }
}
