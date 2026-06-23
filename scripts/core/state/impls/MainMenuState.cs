using GFramework.Core.Abstractions.state;
using GFramework.Core.extensions;
using GFramework.Core.state;
using GFramework.Game.Abstractions.scene;
using GFramework.Game.Abstractions.ui;
using BallRebound.scripts.enums.scene;
using BallRebound.scripts.enums.ui;

namespace BallRebound.scripts.core.state.impls;

/// <summary>
///     主菜单状态，显示主菜单页面和主菜单场景
/// </summary>
public class MainMenuState : ContextAwareStateBase
{
    public override void OnEnter(IState? from)
    {
        var uiRouter = this.GetSystem<IUiRouter>()!;
        uiRouter.Clear();
        this.GetSystem<ISceneRouter>()!.Replace(nameof(SceneKey.MainMenu));
        uiRouter.Push(nameof(UiKey.MainMenu));
    }

    public override bool CanTransitionTo(IState target) => true;
}
