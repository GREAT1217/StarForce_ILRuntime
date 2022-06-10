using GameFramework;

namespace Game
{
    /// <summary>
    /// 自定义的底层与热更层传递的数据。
    /// </summary>
    public sealed class ILUserData
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
        /// 底层逻辑实例。
        /// </summary>
        public object ILLogic
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义的底层与热更层传递的数据。
        /// </summary>
        /// <param name="hotLogicTypeName">热更逻辑类型名。</param>
        /// <param name="userData">用户自定义数据。</param>
        public ILUserData(string hotLogicTypeName, object userData)
        {
            HotLogicTypeFullName = Utility.Text.Format("{0}.{1}", "Game.Hotfix", hotLogicTypeName);
            UserData = userData;
        }
    }
}
