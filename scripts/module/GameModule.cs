using GFramework.Core.Abstractions.architecture;
using GFramework.Game.architecture;

namespace BallRebound.scripts.module;

/// <summary>
///     游戏模块类，负责注册 KILL LOOP 游戏特有的系统
///     包括玩家物理、敌人 AI、波次生成、音乐、连击、分数等系统
/// </summary>
public class GameModule : AbstractModule
{
    public override void Install(IArchitecture architecture)
    {
        // 游戏系统将在各实施阶段中逐一注册：
        // architecture.RegisterSystem(new PlayerPhysicsSystem());
        // architecture.RegisterSystem(new PlayerEnergySystem());
        // architecture.RegisterSystem(new PlayerComboSystem());
        // architecture.RegisterSystem(new EnemySpawnSystem());
        // architecture.RegisterSystem(new EnemyAISystem());
        // architecture.RegisterSystem(new ScoreSystem());
        // architecture.RegisterSystem(new StatsSystem());
        // architecture.RegisterSystem(new MusicSystem());
    }
}
