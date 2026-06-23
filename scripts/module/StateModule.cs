using GFramework.Core.Abstractions.architecture;
using GFramework.Core.functional.pipe;
using GFramework.Game.architecture;
using GFramework.Game.state;
using BallRebound.scripts.core.state.impls;

namespace BallRebound.scripts.module;

/// <summary>
///     状态模块类，负责安装和注册应用状态机及状态
/// </summary>
public class StateModule : AbstractModule
{
    public override void Install(IArchitecture architecture)
    {
        architecture.RegisterSystem(new GameStateMachineSystem().Also(it =>
        {
            it.Register(new AppState());
        }));
    }
}
