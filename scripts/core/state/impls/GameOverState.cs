using GFramework.Core.Abstractions.state;
using GFramework.Core.extensions;
using GFramework.Core.state;
using GFramework.Game.Abstractions.scene;
using GFramework.Game.Abstractions.ui;
using GFramework.Godot.coroutine;
using BallRebound.scripts.enums.scene;
using BallRebound.scripts.enums.ui;

namespace BallRebound.scripts.core.state.impls;

/// <summary>
///     游戏结束状态，显示死亡效果后重载场景或返回菜单
/// </summary>
public class GameOverState : ContextAwareStateBase
{
    public override void OnEnter(IState? from)
    {
        var uiRouter = this.GetSystem<IUiRouter>()!;
        uiRouter.Clear();
        this.GetSystem<ISceneRouter>()!.Unload();

        // 保持与原作一致：直接显示 GameOver 页，玩家可选择重试或返回菜单
        uiRouter.Push(nameof(UiKey.GameOver));
    }

    public override bool CanTransitionTo(IState target) => true;
}
