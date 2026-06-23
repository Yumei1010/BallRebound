using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.game.command.input;

namespace BallRebound.scripts.cqrs.game.command;

/// <summary>
///     开始游戏命令类，从主菜单进入游戏
/// </summary>
/// <param name="input">开始游戏命令输入参数</param>
public sealed class StartGameCommand(StartGameInput input)
    : AbstractCommand<StartGameInput>(input)
{
    /// <summary>
    ///     执行开始游戏命令
    /// </summary>
    /// <param name="input">开始游戏命令输入参数</param>
    protected override void OnExecute(StartGameInput input)
    {
        this.SendEvent(new @event.GameStartedEvent());
    }
}
