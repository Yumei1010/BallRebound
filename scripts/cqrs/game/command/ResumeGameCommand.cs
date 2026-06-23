using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.game.command.input;

namespace BallRebound.scripts.cqrs.game.command;

/// <summary>
///     恢复游戏命令类，从暂停状态恢复游戏
/// </summary>
/// <param name="input">恢复游戏命令输入参数</param>
public sealed class ResumeGameCommand(ResumeGameInput input)
    : AbstractCommand<ResumeGameInput>(input)
{
    /// <summary>
    ///     执行恢复游戏命令
    /// </summary>
    /// <param name="input">恢复游戏命令输入参数</param>
    protected override void OnExecute(ResumeGameInput input)
    {
        this.SendEvent(new @event.GameResumedEvent());
    }
}
