using GFramework.Core.Abstractions.architecture;
using GFramework.Game.architecture;
using GFramework.Game.setting;
using BallRebound.scripts.core.scene;
using BallRebound.scripts.core.ui;

namespace BallRebound.scripts.module;

/// <summary>
///     系统模块类，负责安装和注册框架所需的各种系统组件
/// </summary>
public class SystemModule : AbstractModule
{
    public override void Install(IArchitecture architecture)
    {
        architecture.RegisterSystem(new UiRouter());
        architecture.RegisterSystem(new SceneRouter());
        architecture.RegisterSystem(new SettingsSystem());
    }
}
