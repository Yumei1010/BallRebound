namespace BallRebound.scripts.cqrs.audio.@event;

/// <summary>
///     音乐交叉淡入淡出事件，用于触发背景音乐轨道切换
/// </summary>
public sealed class MusicCrossfadeEvent
{
    /// <summary>
    ///     目标音乐流资源路径（如 "res://assets/audio/music/Neon Ghosts.mp3"）
    /// </summary>
    public required string StreamPath { get; init; }

    /// <summary>
    ///     淡入淡出持续时间（秒）
    /// </summary>
    public required float Duration { get; init; }
}
