using GFramework.Core.Abstractions.command;

namespace BallRebound.scripts.cqrs.enemy.command.input;

/// <summary>
///     生成敌人命令输入类，指定敌人类型和位置
/// </summary>
public sealed class SpawnEnemyInput : ICommandInput
{
    /// <summary>
    ///     敌人类型标识（EnemyNormal / EnemyTracker / EnemyPatrol / EnemyBounce）
    /// </summary>
    public string EnemyType { get; set; } = "EnemyNormal";

    /// <summary>
    ///     生成位置（世界坐标）
    /// </summary>
    public Godot.Vector2 Position { get; set; }
}
