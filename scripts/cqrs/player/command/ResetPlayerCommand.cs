using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.player.command.input;

namespace BallRebound.scripts.cqrs.player.command;

/// <summary>
///     重置玩家命令类，将玩家状态恢复为初始值
/// </summary>
/// <param name="input">重置玩家命令输入参数</param>
public sealed class ResetPlayerCommand(ResetPlayerInput input)
    : AbstractCommand<ResetPlayerInput>(input)
{
    /// <summary>
    ///     执行重置玩家命令
    /// </summary>
    /// <param name="input">重置玩家命令输入参数</param>
    protected override void OnExecute(ResetPlayerInput input)
    {
        // 系统会在阶段 2 中订阅此命令并执行实际重置
    }
}
