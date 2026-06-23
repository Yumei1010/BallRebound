using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.score.command.input;

namespace BallRebound.scripts.cqrs.score.command;

/// <summary>
///     加分命令类，根据基础分和连击倍率计算并更新当前分数
/// </summary>
/// <param name="input">加分命令输入参数</param>
public sealed class AddScoreCommand(AddScoreInput input)
    : AbstractCommand<AddScoreInput>(input)
{
    /// <summary>
    ///     执行加分命令，计算最终分数并发布事件
    /// </summary>
    /// <param name="input">加分命令输入参数</param>
    protected override void OnExecute(AddScoreInput input)
    {
        var multiplier = 1.0f + Math.Min(0.05f * input.Combo, 1.0f);
        var delta = (int)MathF.Round(input.BaseScore * multiplier);
        this.SendEvent(new @event.ScoreChangedEvent
        {
            NewScore = 0, // ScoreSystem 将计算实际值
            Delta = delta,
            ComboMultiplier = multiplier
        });
    }
}
