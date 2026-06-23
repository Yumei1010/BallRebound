using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.score.command.input;

namespace BallRebound.scripts.cqrs.score.command;

/// <summary>
///     重置分数命令类，将当前分数归零
/// </summary>
/// <param name="input">重置分数命令输入参数</param>
public sealed class ResetScoreCommand(ResetScoreInput input)
    : AbstractCommand<ResetScoreInput>(input)
{
    /// <summary>
    ///     执行重置分数命令
    /// </summary>
    /// <param name="input">重置分数命令输入参数</param>
    protected override void OnExecute(ResetScoreInput input)
    {
        this.SendEvent(new @event.ScoreChangedEvent
        {
            NewScore = 0,
            Delta = 0,
            ComboMultiplier = 1.0f
        });
    }
}
