using GFramework.Core.extensions;
using GFramework.Game.Abstractions.ui;
using BallRebound.global;
using Godot;

namespace BallRebound.scripts.menu;

public partial class TemplatePage
{
    private IUiRouter _uiRouter = null!;

    /// <summary>
    ///     异步等待架构就绪，获取 UI 路由器依赖
    /// </summary>
    private async Task ReadyAsync()
    {
        await GameEntryPoint.Architecture.WaitUntilReadyAsync().ConfigureAwait(false);
        _uiRouter = this.GetSystem<IUiRouter>()!;
        _log.Debug("TemplatePage 初始化完成");
    }
}
