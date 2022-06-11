namespace Game.Hotfix
{
    /* 注意不要和 GameMode 的值重复，规定 HotfixGameMode 从 100 开始。*/

    /// <summary>
    /// 游戏模式。
    /// </summary>
    public enum HotfixGameMode : byte
    {
        /// <summary>
        /// Boss 模式。
        /// </summary>
        Boss = 100,
    }
}
