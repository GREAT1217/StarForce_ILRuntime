using GameFramework;

namespace Game
{
    /// <summary>
    /// 自定义 UI 数据。
    /// </summary>
    public sealed class ILFormUserData
    {
        /// <summary>
        /// 热更逻辑类型名。
        /// </summary>
        public string HotLogicTypeFullName
        {
            get;
        }

        /// <summary>
        /// 用户自定义数据。
        /// </summary>
        public object UserData
        {
            get;
        }

        /// <summary>
        /// 非热更层的界面的实例。
        /// </summary>
        public ILForm ILForm
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义 UI 数据。
        /// </summary>
        /// <param name="hotLogicTypeName">热更逻辑类型名。</param>
        /// <param name="userData">用户自定义数据。</param>
        public ILFormUserData(string hotLogicTypeName, object userData)
        {
            HotLogicTypeFullName = Utility.Text.Format("{0}.{1}", "Game.Hotfix", hotLogicTypeName);
            UserData = userData;
        }
    }
}
