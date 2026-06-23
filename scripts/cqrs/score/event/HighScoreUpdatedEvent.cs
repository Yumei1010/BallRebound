namespace BallRebound.scripts.cqrs.score.@event;

/// <summary>
///     最高分更新事件，当当前分数超过历史最高分时触发
/// </summary>
public sealed class HighScoreUpdatedEvent
{
    /// <summary>
    ///     新的历史最高分
    /// </summary>
    public required int NewHighScore { get; init; }
}
