using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.enemy.command.input;

namespace BallRebound.scripts.cqrs.enemy.command;

/// <summary>
///     波次生成命令类，开始一波敌人生成周期
/// </summary>
/// <param name="input">波次生成命令输入参数</param>
public sealed class SpawnWaveCommand(SpawnWaveInput input)
    : AbstractCommand<SpawnWaveInput>(input)
{
    /// <summary>
    ///     执行波次生成命令
    /// </summary>
    /// <param name="input">波次生成命令输入参数</param>
    protected override void OnExecute(SpawnWaveInput input)
    {
        this.SendEvent(new @event.WaveStartedEvent
        {
            WaveNumber = input.WaveNumber,
            Duration = -1f
        });
    }
}
