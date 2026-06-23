namespace BallRebound.scripts.cqrs.audio.@event;

/// <summary>
///     音效播放事件，用于触发一次性音效播放
/// </summary>
public sealed class PlaySfxEvent
{
    /// <summary>
    ///     音效文件资源路径（如 "res://assets/audio/sfx/bounce.wav"）
    /// </summary>
    public required string SfxPath { get; init; }
}
