using GFramework.Core.command;
using GFramework.Core.extensions;
using BallRebound.scripts.cqrs.enemy.command.input;

namespace BallRebound.scripts.cqrs.enemy.command;

/// <summary>
///     生成敌人命令类，实例化敌人到场景中
/// </summary>
/// <param name="input">生成敌人命令输入参数</param>
public sealed class SpawnEnemyCommand(SpawnEnemyInput input)
    : AbstractCommand<SpawnEnemyInput>(input)
{
    /// <summary>
    ///     执行敌人生成命令
    /// </summary>
    /// <param name="input">生成敌人命令输入参数</param>
    protected override void OnExecute(SpawnEnemyInput input)
    {
        this.SendEvent(new @event.EnemySpawnedEvent
        {
            EnemyType = input.EnemyType,
            Position = input.Position
        });
    }
}
