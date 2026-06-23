using GFramework.Core.Abstractions.command;

namespace BallRebound.scripts.cqrs.player.command.input;

/// <summary>
///     发射玩家命令输入类，包含发射方向参数
/// </summary>
public sealed class LaunchPlayerInput : ICommandInput
{
    /// <summary>
    ///     发射方向（世界坐标系中的单位向量）
    /// </summary>
    public Godot.Vector2 Direction { get; set; }

    /// <summary>
    ///     发射速度大小
    /// </summary>
    public float Speed { get; set; }
}
