using GFramework.Core.Abstractions.command;

namespace BallRebound.scripts.cqrs.score.command.input;

/// <summary>
///     加分命令输入类，指定分数增量和连击倍率
/// </summary>
public sealed class AddScoreInput : ICommandInput
{
    /// <summary>
    ///     要增加的分数值（基础分）
    /// </summary>
    public int BaseScore { get; set; }

    /// <summary>
    ///     当前连击数（用于计算倍率）
    /// </summary>
    public int Combo { get; set; }
}
