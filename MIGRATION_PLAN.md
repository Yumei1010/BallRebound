---
name: ball-to-gframework-migration
description: KILL LOOP 从 GDScript 迁移到 GFramework C# 模板的完整移植计划
metadata:
  type: project
  source: D:\By GitHub\Ball (GDScript Godot 4.x)
  target: D:\By GitHub\BallRebound (GFramework C# template)
  game_name: KILL LOOP
  created: 2026-06-23
---

# KILL LOOP → GFramework C# 移植计划

## 0. 概述

**源项目** (Ball) 是一个 GDScript 编写的快节奏物理街机游戏。核心玩法：用鼠标弹弓式发射球体，高速撞击击杀敌人，在波次生存中追逐高分。

**目标项目** (BallRebound) 基于 My-GFramework-Godot-Template，使用 Godot 4.6 + C# (.NET 10) + GFramework (0.0.177) CQRS/ECS 框架。

### 核心设计决策

| 维度 | Ball (现状) | BallRebound (目标) |
|------|------------|-------------------|
| 架构 | 单体场景 + 信号 + 单例 | CQRS 事件/命令 + DI + 状态机 |
| 通信 | `get_node()` 路径 + Godot 信号 | GFramework 事件总线 + 命令发送 |
| 状态 | 场景内临时变量 | Model + System 分层管理 |
| 持久化 | `FileAccess.store_var()` 二进制 | JSON 序列化 + `ISettingsDataRepository` |
| 测试 | 无 | xUnit 覆盖核心逻辑 |
| 场景 | 单个 main.tscn | 场景配置化 + SceneRouter 切换 |

## 1. 项目初始化

### 1.1 模板清理（已完成部分）

BallRebound 已基于模板完成以下清理：
- [x] 移除 Dependabot
- [x] 清理无效资源引用
- [x] 补全测试项目
- [x] 补全缺失目录

### 1.2 重命名（已完成 ✅）

- [x] `.csproj` 重命名为 `BallRebound.csproj`，`<RootNamespace>` → `BallRebound`
- [x] `.sln` 重命名为 `BallRebound.sln`，项目引用更新
- [x] `tests/` 测试项目重命名为 `BallRebound.Tests`
- [x] `project.godot`: `config/name` → `"KILL LOOP"`
- [x] `project.godot`: `project/assembly_name` → `"BallRebound"`
- [x] 全局命名空间 `GFrameworkTemplate` → `BallRebound`（所有 `.cs` + `.md` 文件）
- [x] `GameConstants.Bgm` 值从 `"BGM"` → `"Music"`（匹配 Ball 音频总线命名）

### 1.3 已配置（已完成 ✅）

- [x] `config/features` → `PackedStringArray("4.6", "C#", "Forward Plus")` — **选用 Forward+** 保证 CRT 着色器 (CanvasLayer.gdshader) 兼容
- [x] 分辨率 → 1920×1080（匹配 Ball 原作）
- [x] `physics/common/physics_ticks_per_second` → 120
- [x] 物理层 5 层定义：`player`, `enemy`, `world`, `playerKill`, `EnemyKill`
- [x] 全局组 `[global_group]`：`enemy`, `bouncing_enemy`, `buttons`
- [x] 国际化配置：中/英文翻译路径
- [x] `config/use_custom_user_dir` → `"KILL LOOP"`

### 1.4 输入映射（已合并 ✅）

最终输入动作表（Ball 语义名 + 必要补充）：

| 动作名 | 绑定 | 用途 |
|--------|------|------|
| `ui_cancel` | Escape / Joypad B | 暂停菜单（GlobalInputController 引用） |
| `launch_ball` | 鼠标左键 | 瞄准 + 发射（核心机制） |
| `cancel_aim` | 鼠标右键 | 取消瞄准 |
| `debug_reset` | P 键 | 调试：重置存档数据 |
| `toggle_fullscreen` | 1 键 | 切换全屏 |

已移除模板原有的 `MouseLeft`/`MouseRight`/`MouseWheelUp`/`MouseWheelDown`/`OpenMap`（改用语义化命名或无需该功能）。

### 1.5 框架适配（已完成 ✅）

- [x] `GameArchitecture.cs` 去掉 `sealed`，允许后续扩展
- [x] 新增 `scripts/module/GameModule.cs` 注册游戏特有系统（`GameArchitecture` 中最后安装）
- [x] 音频总线 `resource/bus/project_bus_layout.tres` 中 `"BGM"` → `"Music"`

## 2. 领域分解（CQRS 域设计）

将 Ball 的单体 `main.tscn` 拆分为独立 CQRS 域：

### 2.1 `scripts/cqrs/player/` — 玩家域

**状态** (Model):
```
PlayerStateModel : IModel
  - Energy: float (0-300)
  - Combo: int
  - MaxSpeed: float (4000 + combo * 250)
  - IsAlive: bool
  - IsAiming: bool
  - CurrentVelocity: Vector2
```

**事件** (`event/`):
```
PlayerLaunchedEvent      — 发射（方向、力度、消耗能量）
PlayerBouncedEvent       — 撞墙反弹（反弹位置、速度）
PlayerKilledEnemyEvent   — 击杀敌人（敌人类型、速度、位置）
PlayerDiedEvent          — 死亡（死亡原因、最终分数）
ComboChangedEvent        — 连击变化（新连击数）
ComboLostEvent           — 连击丢失
EnergyChangedEvent       — 能量变化（新值、变化量）
EnergyMilestoneEvent     — 能量达到 100/200/300 标记
AimStartedEvent          — 开始瞄准
AimCancelledEvent        — 取消瞄准
```

**命令** (`command/`):
```
LaunchPlayerCommand      — 发射（方向向量）
CancelAimCommand         — 取消瞄准
ResetPlayerCommand       — 重置玩家状态
```

**系统** (`../../system/player/`):
```
PlayerPhysicsSystem      — 管理 RigidBody2D 物理参数、速度阈值
PlayerEnergySystem       — 能量消耗/获取逻辑
PlayerComboSystem        — 连击计数、衰减逻辑
PlayerAimSystem          — 瞄准状态、慢动作时间缩放
```

### 2.2 `scripts/cqrs/enemy/` — 敌人域

**事件**:
```
EnemySpawnedEvent        — 敌人生成（类型、位置）
EnemyKilledEvent         — 敌人死亡（击杀者、位置、分数）
WaveStartedEvent         — 波次开始（波次编号、持续时间）
WaveEndedEvent           — 波次结束
WaveTimeUpdatedEvent     — 波次时间推进
```

**命令**:
```
SpawnEnemyCommand        — 生成敌人
SpawnWaveCommand         — 开始一波
KillEnemyCommand         — 击杀敌人
```

**系统** (`../../system/enemy/`):
```
EnemySpawnSystem         — 波次管理、生成逻辑
EnemyAISystem            — 敌人行为（追踪、巡逻）
EnemyPhysicsSystem       — 敌人物理属性
```

### 2.3 `scripts/cqrs/score/` — 分数域

**事件**:
```
ScoreChangedEvent        — 分数变化
HighScoreUpdatedEvent    — 最高分更新
StatsUpdatedEvent        — 统计数据更新
```

**命令**:
```
AddScoreCommand          — 加分数
ResetScoreCommand        — 重置当前分数
```

**系统** (`../../system/score/`):
```
ScoreSystem              — 分数计算（基础分 * 连击倍率）
StatsSystem              — 统计数据追踪
```

### 2.4 `scripts/cqrs/audio/` — 音频域

复用模板已有的音频命令结构（`ChangeMasterVolumeCommand`、`ChangeBgmVolumeCommand`、`ChangeSfxVolumeCommand`），总线名已对齐 Ball：

| 总线 | Ball 原名 | BallRebound (已修正) |
|------|----------|---------------------|
| 主输出 | Master | Master |
| 音乐 | **Music** | **Music** (原 BGM) |
| 音效 | SFX | SFX |

**新增事件**:
```
MusicCrossfadeEvent      — 音乐交叉淡入淡出
PlaySfxEvent             — 播放音效
```

### 2.5 `scripts/cqrs/setting/` — 设置域

复用模板已有的 `scripts/cqrs/setting/` 结构。

### 2.6 `scripts/cqrs/game/` — 游戏流程域

**事件**:
```
GameStartedEvent         — 游戏开始
GamePausedEvent          — 游戏暂停
GameResumedEvent         — 游戏恢复
GameOverEvent            — 游戏结束
```

**命令**:
```
StartGameCommand
PauseGameCommand
ResumeGameCommand
ExitGameCommand          — 复用模板已有
```

---

## 3. 组件映射：GDScript → C# partial class

### 3.1 PlayerBall → PlayerBall 组件

Ball 的 `player_ball.gd` (380+ 行) → GFramework 架构：

| Ball 源文件 | BallRebound 目标 | 职责 |
|------------|-----------------|------|
| `player_ball.gd` | `scripts/component/player/PlayerBall.cs` | _Ready + 核心逻辑 |
| — | `PlayerBall.Dependencies.cs` | 节点引用（Sprite2D, CollisionShape2D, Area2D, AudioStreamPlayer 等） |
| — | `PlayerBall.Properties.cs` | @Export 属性、字段 |
| — | `PlayerBall.Events.cs` | 事件订阅 |
| — | `PlayerBall.Signals.cs` | Godot 信号 → CQRS 事件桥接 |

**迁移映射**:
```
player_ball.gd:
  @export launch_multiplier → PlayerBall.Properties.cs
  _input(event) → PlayerAimSystem + LaunchPlayerCommand
  _physics_process(delta) → PlayerPhysicsSystem
  _on_body_entered(body) → PlayerBall.Events.cs → PlayerBouncedEvent/PlayerDiedEvent
  die() → this.SendCommand(new KillPlayerCommand(...))
  add_combo() → this.SendEvent(new ComboChangedEvent(...))
```

### 3.2 敌人 → Enemy 组件

每个敌人类型一个组件类 + 共享的 EnemyModel：

| 源文件 | 目标 | 行为 |
|--------|------|------|
| `enemy_normal.gd` | `scripts/component/enemy/EnemyNormal.cs` | 静止敌人 |
| `enemy_tracker.gd` | `scripts/component/enemy/EnemyTracker.cs` | 追踪玩家 |
| `enemy_patrol.gd` | `scripts/component/enemy/EnemyPatrol.cs` | 沿路径巡逻 |
| `enemy_bounce.gd` | `scripts/component/enemy/EnemyBounce.cs` | 弹性碰撞球 |

每个敌人遵循 partial class 模式 + `[Log] [ContextAware]`。

### 3.3 UI → UI 页面

| 源文件 | 目标（partial class 五文件模式） |
|--------|-------------------------------|
| `game_ui.gd` | `scripts/menu/GameHudPage.cs` + `.Properties.cs` + `.Dependencies.cs` + `.Events.cs` + `.Signals.cs` |
| `main_menu.gd` | `scripts/menu/MainMenuPage.cs` + ... |
| `pause_menu.gd` | `scripts/menu/PauseMenuPage.cs` + ... |
| `settings_menu.gd` | `scripts/menu/SettingsMenuPage.cs` + ... |
| `data_stats.gd` | `scripts/menu/StatsPage.cs` + ... |
| `energy_bar.gd` | 合并到 `GameHudPage` 中（不需要独立页面） |

UI 页面在 `GameEntryPoint.UiPageConfigs` 中注册，由 `UiRouter` 管理。

### 3.4 管理器 → System/Module 注册

| Ball 单例 | BallRebound |
|-----------|------------|
| `DataManager` (autoload) | `SettingsSystem` (已有) + `StatsSystem` (新建) |
| `MusicManager` (autoload) | `MusicSystem` (新建，注册到 SystemModule) |
| `UiAudioManager` (autoload) | 合并到 `UiAudioSystem`，hook UI 生命周期 |
| `PlaceholderArt` (autoload) | 移除——直接使用 PNG 纹理 |
| `BounceCounterManager` | `BounceCounterSystem` (新建) |
| `EnemySpawner` | `EnemySpawnSystem` (新建) |
| `PathManager` | 合并到 `EnemySpawnSystem` 或独立 `PathSystem` |
| MainScene 编排代码 | 拆分为各 System 的 `RegisterEvent` 订阅 |

---

## 4. 状态机设计

### 4.1 游戏状态

```
AppState（模板已有）
  → MainMenuState  — 主菜单，显示 MainMenuPage
  → GameplayState  — 游戏中，启动物理/敌人系统
    → GamePausedState  — 暂停，叠加 PauseMenuPage
  → GameOverState  — 死亡动画，显示分数，返回菜单
```

### 4.2 状态转换

```
MainMenuState → (StartGameCommand) → GameplayState
GameplayState → (PauseGameCommand) → GamePausedState  
GamePausedState → (ResumeGameCommand) → GameplayState
GameplayState → (PlayerDiedEvent) → GameOverState
GameOverState → (RestartCommand) → GameplayState
GameOverState → (ExitToMenuCommand) → MainMenuState
```

---

## 5. 数据持久化

### 5.1 迁移方案

Ball 使用 `FileAccess.store_var()` 二进制格式。BallRebound 使用 JSON 序列化：

```
DataManager.store_var() → UnifiedSettingsDataRepository 已注册
高分/统计 → 新增 SettingDataLocationProvider 的 Key
全屏/音量/语言 → 已有 SettingsModel 处理
```

### 5.2 新增 DataKey

```csharp
// scripts/data/setting/LocalDataLocation.cs 中扩展
new LocalDataLocation { Key = "high_score", Namespace = "stats" }
new LocalDataLocation { Key = "total_kills", Namespace = "stats" }
new LocalDataLocation { Key = "max_combo", Namespace = "stats" }
// ...
```

---

## 6. 物理与输入

### 6.1 物理层（已配置 ✅）

```
project.godot [layer_names]:
2d_physics/layer_1 = "player"
2d_physics/layer_2 = "enemy"
2d_physics/layer_3 = "world"
2d_physics/layer_4 = "playerKill"
2d_physics/layer_5 = "EnemyKill"

[global_group]:
enemy = ""
bouncing_enemy = ""
buttons = ""

physics_ticks_per_second: 120
```

### 6.2 输入映射（已配置 ✅）

```
project.godot [input]:
ui_cancel          → Escape / Joypad B   (暂停菜单)
launch_ball        → 鼠标左键             (瞄准 + 发射)
cancel_aim         → 鼠标右键             (取消瞄准)
debug_reset        → P 键                 (调试重置数据)
toggle_fullscreen  → 1 键                 (切换全屏)
```

### 6.3 输入处理

GFramework 模板已有 `GlobalInputController`（处理 Global 阶段输入）和 `GameInputController` 基类。扩展为：

```
GameplayInputController : GameInputController
  — 处理 Gameplay/Paused 阶段的输入
  — 左键按下 → SendCommand(new StartAimCommand())
  — 左键释放 → SendCommand(new LaunchPlayerCommand(direction))
```

---

## 7. 资产迁移

### 7.1 直接复制（不改名）

```
Ball/assets/ → BallRebound/assets/:
  actor/          → assets/textures/actor/
  audio/          → assets/audio/
  shader/         → assets/shader/ （.gdshader 直接兼容）
  language/       → assets/language/
  ui/             → assets/textures/ui/
  style/          → resource/bus/ （已有 default_bus_layout.tres）
```

### 7.2 需重建的资源

```
Ball 的 .tscn 场景 → 用 Godot 编辑器重建（部分结构变化）
  - main.tscn 拆分为 main.tscn + game_ui.tscn + 子场景
  - 敌人场景保留但添加 C# 脚本
  - UI 场景按页面重建
```

### 7.3 着色器兼容性

Godot `.gdshader` 文件在 C# 项目中完全兼容，直接复制即可。

---

## 8. 分阶段实施计划

### 阶段 1：基础搭建（预计 2-3 小时）

```
1.  重命名项目、命名空间全局替换
2.  配置 project.godot（物理层、输入映射、项目名称）
3.  复制所有 assets/ 到 BallRebound/assets/
4.  定义所有 CQRS 事件类（player/enemy/score/audio/game 域）
5.  定义所有 CQRS 命令类及 Input
6.  扩展 UiKey 枚举（MainMenu, GameHud, PauseMenu, Settings, Stats, GameOver）
7.  创建状态类和状态机注册
```

### 阶段 2：核心玩法（预计 4-6 小时）

```
8.  创建 PlayerBall 组件（partial class 五文件）
9.  实现 PlayerPhysicsSystem, PlayerEnergySystem, PlayerComboSystem
10. 创建 4 种敌人组件
11. 实现 EnemySpawnSystem（JSON 波次数据驱动）
12. 实现 EnemyAISystem（追踪、巡逻）
13. 创建 GameplayState，绑定场景和 UI
14. 实现 GameHudPage（能量条、连击、分数、计时器）
```

### 阶段 3：系统完善（预计 2-3 小时）

```
15. 实现 ScoreSystem、StatsSystem
16. 实现 MusicSystem（交叉淡入淡出）
17. 创建 MainMenuPage、PauseMenuPage、SettingsMenuPage
18. 实现状态转换动画（场景过渡、UI 切换）
19. 连接音频事件（bounce/death/launch/kill SFX）
```

### 阶段 4：打磨（预计 1-2 小时）

```
20. 死亡效果着色器（DeathInversionEffect.gdshader）
21. 瞄准线（Line2D）和速度轨迹（TrailWithLine2D）
22. 屏幕特效（CRT + 慢动作 CanvasLayer.gdshader）
23. 击杀效果（kill_effect 动画）
24. 浮动文字（floating_text 伤害/分数弹出）
25. 相机震动（camera_shaker）
```

### 阶段 5：测试与调优（预计 1-2 小时）

```
26. 核心系统单元测试（ScoreSystem, EnergySystem, ComboSystem）
27. 物理参数调优（速度阈值、弹性系数、发射倍率）
28. 波次平衡（生成间隔、敌人组合）
29. dotnet test 全部通过
30. Godot 编辑器完整运行验证
```

---

## 9. 风险与难点

| 风险 | 等级 | 说明 | 缓解措施 |
|------|------|------|---------|
| **物理精确复现** | 🔴 高 | Godot 物理引擎在 GDScript 和 C# 中行为一致，但 GFramework 的事件驱动可能引入微妙时序差异（慢动作、死亡暂停） | 保持 `_physics_process` 中的核心逻辑与原始 C# 翻译一致；`Engine.time_scale` 操作在 System 层集中管理 |
| **`_death_pause` 机制** | 🔴 高 | Ball 死亡时 `get_tree().paused = true` + 动画 → `reload_current_scene()`。计划中的 `GameOverState` 暗示显示 UI，但原作直接重载 | 明确死亡流程设计：若保持原作风格，`GameOverState.OnEnter()` 应直接播放死亡效果后重载；若添加 GameOver 页面，需实现重试/返回菜单逻辑 |
| **输入延迟** | 🟡 中 | GDScript 的 `_input()` 是同步的，GFramework 的 CQRS 命令发送增加了间接层 | 输入处理保留在 `_Input()` 回调中直接处理，仅将结果通过事件发布（不通过命令） |
| **音频时序** | 🟡 中 | 音效播放对延迟敏感（尤其是 bounce.wav 在快速连续反弹时） | `AudioStreamPlayer` 直接操作保留在组件中；仅背景音乐使用 CQRS 事件 |
| **EnemyBounce 行为理解** | 🟡 中 | 计划描述为"弹性碰撞球"，实际是**友方链式反应触发器**：碰敌人→连锁击杀→自身不消失，不伤害玩家 | 实现时以 GDScript 源码为准，确保连锁击杀逻辑正确 |
| **spawn_waves.json 结构** | 🟡 中 | JSON 波次定义需要可扩展；文件位于 Ball 根目录而非 assets/ | 保持原有结构，用 `System.Text.Json` 反序列化；单独复制不遗漏 |
| **ReplaceArt 占位符** | 🟢 低 | Ball 使用程序化纹理（写死在脚本中），直接用 PNG 替换即可 | 复制原始 PNG 精灵图 |
| **渲染兼容性** | 🟢 ✅已解决 | Forward+ 保证 3 个着色器 (CanvasLayer/DeathInversion/DangerFlash) 兼容 | project.godot 已配置为 Forward+，分辨率 1920×1080 |
| **音频总线命名** | 🟢 ✅已解决 | Ball 用 "Music"，模板用 "BGM" | 总线 .tres + GameConstants 已统一为 "Music" |
| **模块扩展** | 🟢 ✅已解决 | 模板 GameArchitecture 为 sealed | 已去掉 sealed + 新增 GameModule |

---

## 10. 文件结构总览（目标状态）

```
BallRebound/
├── assets/
│   ├── audio/
│   │   ├── music/Neon Ghosts.mp3, Cyber.wav
│   │   └── sfx/ (8 个 .wav)
│   ├── language/ (2 个 .translation + .csv)
│   ├── shader/ (3 个 .gdshader)
│   └── textures/
│       ├── actor/ (player3.png, player3light.png, 32ball.png, emeny/)
│       └── ui/ (ICON.png, text/)
├── global/ (7 个 autoload)
├── resource/bus/project_bus_layout.tres
├── scenes/
│   ├── main.tscn
│   └── component/ (player_ball, enemy_*, effects)
├── scripts/
│   ├── component/ (player/, enemy/, effect/)
│   ├── constants/
│   ├── core/ (状态机、架构)
│   ├── cqrs/
│   │   ├── player/ (event/, command/, command/input/)
│   │   ├── enemy/ (同上)
│   │   ├── score/ (同上)
│   │   ├── audio/ (事件——扩展模板)
│   │   ├── setting/ (模板已有)
│   │   └── game/ (事件 + 命令)
│   ├── data/setting/ (LocalDataLocation, SettingDataLocationProvider)
│   ├── enums/ui/UiKey.cs (扩展)
│   ├── menu/ (6 个 UI 页面 × 5 文件)
│   ├── model/ (PlayerStateModel, EnemyModel, ScoreModel)
│   ├── module/ (5 个模块——模板 4 个 + 游戏模块)
│   └── system/ (player/, enemy/, score/, audio/, game/)
└── tests/ (xUnit)
    └── BallRebound.Tests/
        ├── PlayerEnergySystemTests.cs
        ├── ScoreSystemTests.cs
        └── ComboSystemTests.cs
```

---

## 11. 关键接口/类型速查

### 事件示例
```csharp
namespace BallRebound.scripts.cqrs.player.@event;

public sealed class PlayerLaunchedEvent
{
    public required Vector2 Direction { get; init; }
    public required float Speed { get; init; }
    public required float EnergyCost { get; init; }
}
```

### 命令示例
```csharp
namespace BallRebound.scripts.cqrs.player.command;

public sealed class LaunchPlayerCommand : AbstractCommand<LaunchPlayerInput>
{
    protected override void Execute(LaunchPlayerInput input)
    {
        // 通过 System 处理物理发射
    }
}
```

### 状态示例
```csharp
namespace BallRebound.scripts.core.state.impls;

public class GameplayState : ContextAwareStateBase
{
    public override void OnEnter(IState? from)
    {
        this.GetSystem<IUiRouter>()!.Clear();
        this.GetSystem<IUiRouter>()!.Push(UiKey.GameHud);
        this.GetSystem<ISceneRouter>()!.Replace(SceneKey.Main);
    }
}
```

---

## 12. 不迁移的内容

| 内容 | 原因 |
|------|------|
| `PlaceholderArt` | 程序化纹理生成是权宜之计，直接用 PNG 替代 |
| `breakthrough_effect.gd` | Ball 中未使用 |
| `DangerFlash.gdshader` | Ball 中未使用 |
| `player.png`, `player2.png` | Ball 中已废弃的旧精灵图 |
| `script/other/` 目录 | 单例被 GFramework DI 替代 |
| `script/audio/audio_manager.gd` | Ball 中是空占位 |
| `FileAccess.store_var()` 二进制存档 | 替换为 JSON 序列化 |
