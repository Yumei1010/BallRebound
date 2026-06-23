namespace BallRebound.scripts.cqrs.game.@event;

/// <summary>
///     游戏结束事件，当玩家死亡时触发
/// </summary>
public sealed class GameOverEvent
{
    /// <summary>
    ///     最终得分
    /// </summary>
    public required int FinalScore { get; init; }
}
