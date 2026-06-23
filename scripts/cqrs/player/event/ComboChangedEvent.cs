namespace BallRebound.scripts.cqrs.player.@event;

/// <summary>
///     连击变化事件，当连击数增加或减少时触发
/// </summary>
public sealed class ComboChangedEvent
{
    /// <summary>
    ///     新的连击数
    /// </summary>
    public required int NewCombo { get; init; }
}
