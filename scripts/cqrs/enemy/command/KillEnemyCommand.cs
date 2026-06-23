using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.enemy.command.input;

namespace BallRebound.scripts.cqrs.enemy.command;

/// <summary>
///     击杀敌人命令类，处理敌人死亡逻辑
/// </summary>
/// <param name="input">击杀敌人命令输入参数</param>
public sealed class KillEnemyCommand(KillEnemyInput input)
    : AbstractCommand<KillEnemyInput>(input)
{
    /// <summary>
    ///     执行击杀敌人命令
    /// </summary>
    /// <param name="input">击杀敌人命令输入参数</param>
    protected override void OnExecute(KillEnemyInput input)
    {
        this.SendEvent(new @event.EnemyKilledEvent
        {
            EnemyType = input.EnemyType,
            Position = input.Position,
            BaseScore = input.BaseScore
        });
    }
}
