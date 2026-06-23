using GFramework.Game.Abstractions.data;
using GFramework.Game.Abstractions.enums;

namespace BallRebound.scripts.data.setting;

/// <summary>
///     设置与统计数据位置提供者，按类型名和命名空间生成数据存储位置
/// </summary>
public class SettingDataLocationProvider : IDataLocationProvider
{
    public IDataLocation GetLocation(Type type)
    {
        return new LocalDataLocation
        {
            Key = type.Name,
            Namespace = GetNamespace(type)
        };
    }

    private static string GetNamespace(Type type)
    {
        // 设置类放入 "settings" 命名空间
        if (type.Name.Contains("Settings", StringComparison.OrdinalIgnoreCase))
            return "settings";

        // 统计数据类放入 "stats" 命名空间
        if (type.Name.Contains("Stats", StringComparison.OrdinalIgnoreCase))
            return "stats";

        return "settings";
    }

    /// <summary>
    ///     统计数据 Key 注册表（供 StatsSystem 运行时使用）
    /// </summary>
    public static readonly IReadOnlyList<IDataLocation> StatsDataLocations = new List<IDataLocation>
    {
        new LocalDataLocation { Key = "high_score", Namespace = "stats" },
        new LocalDataLocation { Key = "total_kills", Namespace = "stats" },
        new LocalDataLocation { Key = "max_combo", Namespace = "stats" },
        new LocalDataLocation { Key = "max_kills_single_game", Namespace = "stats" },
        new LocalDataLocation { Key = "total_play_time", Namespace = "stats" },
        new LocalDataLocation { Key = "longest_survival_time", Namespace = "stats" },
    };
}
