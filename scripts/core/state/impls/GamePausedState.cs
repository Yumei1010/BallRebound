using GFramework.Core.Abstractions.state;
using GFramework.Core.extensions;
using GFramework.Core.state;
using GFramework.Game.Abstractions.scene;
using GFramework.Game.Abstractions.ui;
using BallRebound.scripts.enums.scene;
using BallRebound.scripts.enums.ui;

namespace BallRebound.scripts.core.state.impls;

/// <summary>
///     游戏暂停状态，在 GameplayState 之上叠加暂停菜单
/// </summary>
public class GamePausedState : ContextAwareStateBase
{
    public override void OnEnter(IState? from)
    {
        // 不 Clear，保留底层 HUD 和场景
        this.GetSystem<IUiRouter>()!.Push(nameof(UiKey.PauseMenu));
    }

    public override bool CanTransitionTo(IState target) => target is GameplayState;
}
