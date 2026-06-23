using GFramework.Core.Abstractions.command;

namespace BallRebound.scripts.cqrs.enemy.command.input;

/// <summary>
///     波次生成命令输入类，指定波次编号
/// </summary>
public sealed class SpawnWaveInput : ICommandInput
{
    /// <summary>
    ///     要开始的波次编号（从 1 开始）
    /// </summary>
    public int WaveNumber { get; set; } = 1;
}
