using GFramework.Core.Abstractions.command;

namespace BallRebound.scripts.cqrs.enemy.command.input;

/// <summary>
///     击杀敌人命令输入类，指定击杀相关参数
/// </summary>
public sealed class KillEnemyInput : ICommandInput
{
    /// <summary>
    ///     被击杀的敌人类型标识
    /// </summary>
    public string EnemyType { get; set; } = string.Empty;

    /// <summary>
    ///     击杀位置（世界坐标）
    /// </summary>
    public Godot.Vector2 Position { get; set; }

    /// <summary>
    ///     此次击杀的基础分数
    /// </summary>
    public int BaseScore { get; set; }
}
