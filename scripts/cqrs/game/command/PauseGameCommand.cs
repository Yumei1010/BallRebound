using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.game.command.input;

namespace BallRebound.scripts.cqrs.game.command;

/// <summary>
///     暂停游戏命令类，冻结游戏逻辑
/// </summary>
/// <param name="input">暂停游戏命令输入参数</param>
public sealed class PauseGameCommand(PauseGameInput input)
    : AbstractCommand<PauseGameInput>(input)
{
    /// <summary>
    ///     执行暂停游戏命令
    /// </summary>
    /// <param name="input">暂停游戏命令输入参数</param>
    protected override void OnExecute(PauseGameInput input)
    {
        this.SendEvent(new @event.GamePausedEvent());
    }
}
