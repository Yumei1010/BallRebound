using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.player.command.input;

namespace BallRebound.scripts.cqrs.player.command;

/// <summary>
///     取消瞄准命令类，处理取消瞄准状态并恢复时间缩放
/// </summary>
/// <param name="input">取消瞄准命令输入参数</param>
public sealed class CancelAimCommand(CancelAimInput input)
    : AbstractCommand<CancelAimInput>(input)
{
    /// <summary>
    ///     执行取消瞄准命令
    /// </summary>
    /// <param name="input">取消瞄准命令输入参数</param>
    protected override void OnExecute(CancelAimInput input)
    {
        this.SendEvent(new @event.AimCancelledEvent());
    }
}
