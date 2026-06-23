using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.player.command.input;

namespace BallRebound.scripts.cqrs.player.command;

/// <summary>
///     发射玩家命令类，处理玩家球体发射逻辑
/// </summary>
/// <param name="input">发射命令输入参数</param>
public sealed class LaunchPlayerCommand(LaunchPlayerInput input)
    : AbstractCommand<LaunchPlayerInput>(input)
{
    /// <summary>
    ///     执行发射命令，将发射事件发布到事件总线
    /// </summary>
    /// <param name="input">发射命令输入参数</param>
    protected override void OnExecute(LaunchPlayerInput input)
    {
        this.SendEvent(new @event.PlayerLaunchedEvent
        {
            Direction = input.Direction,
            Speed = input.Speed,
            EnergyCost = 100.0f
        });
    }
}
