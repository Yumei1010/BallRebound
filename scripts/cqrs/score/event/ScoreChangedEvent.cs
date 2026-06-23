namespace BallRebound.scripts.cqrs.score.@event;

/// <summary>
///     分数变化事件，当当前分数增加或重置时触发
/// </summary>
public sealed class ScoreChangedEvent
{
    /// <summary>
    ///     新的总分值
    /// </summary>
    public required int NewScore { get; init; }

    /// <summary>
    ///     此次分数变化量（正数为加分）
    /// </summary>
    public required int Delta { get; init; }

    /// <summary>
    ///     此次加分的连击倍率（≥ 1.0）
    /// </summary>
    public required float ComboMultiplier { get; init; }
}
