using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public abstract class HotfixForm
    {
        /// <summary>
        /// 非热更层的界面的实例。
        /// </summary>
        protected ILForm ILForm
        {
            get;
            private set;
        }

        public void Close()
        {
            ILForm.Close(false);
        }

        public void Close(bool ignoreFade)
        {
            ILForm.Close(ignoreFade);
        }

        /// <summary>
        /// 界面初始化。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnInit(object userData)
        {
            ILFormUserData ilFormUserData = (ILFormUserData)userData;
            if (ilFormUserData == null)
            {
                Log.Error("HotfixForm Init failed.");
                return;
            }

            ILForm = ilFormUserData.ILForm;
        }

        /// <summary>
        /// 界面回收。
        /// </summary>
        protected virtual void OnRecycle()
        {
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnOpen(object userData)
        {
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnClose(bool isShutdown, object userData)
        {
        }

        /// <summary>
        /// 界面暂停。
        /// </summary>
        protected virtual void OnPause()
        {
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        protected virtual void OnResume()
        {
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        protected virtual void OnCover()
        {
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        protected virtual void OnReveal()
        {
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        protected virtual void OnRefocus(object userData)
        {
        }

        /// <summary>
        /// 界面轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        protected virtual void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
        {
        }
    }
}
