using GFramework.Core.Abstractions.state;
using GFramework.Core.extensions;
using GFramework.Core.state;
using GFramework.Game.Abstractions.scene;
using GFramework.Game.Abstractions.ui;
using BallRebound.scripts.enums.scene;
using BallRebound.scripts.enums.ui;

namespace BallRebound.scripts.core.state.impls;

/// <summary>
///     游戏状态，进入核心玩法——加载主场景、显示 HUD
/// </summary>
public class GameplayState : ContextAwareStateBase
{
    public override void OnEnter(IState? from)
    {
        var uiRouter = this.GetSystem<IUiRouter>()!;
        uiRouter.Clear();
        this.GetSystem<ISceneRouter>()!.Replace(nameof(SceneKey.Main));
        uiRouter.Push(nameof(UiKey.GameHud));
    }

    public override bool CanTransitionTo(IState target) => true;
}
