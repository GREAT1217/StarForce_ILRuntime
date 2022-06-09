using GameFramework.Localization;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class SettingForm : HotfixForm
    {
        private Toggle m_MusicMuteToggle = null;
        private Slider m_MusicVolumeSlider = null;
        private Toggle m_SoundMuteToggle = null;
        private Slider m_SoundVolumeSlider = null;
        private Toggle m_UISoundMuteToggle = null;
        private Slider m_UISoundVolumeSlider = null;
        private CanvasGroup m_LanguageTipsCanvasGroup = null;
        private Toggle m_EnglishToggle = null;
        private Toggle m_ChineseSimplifiedToggle = null;
        private Toggle m_ChineseTraditionalToggle = null;
        private Toggle m_KoreanToggle = null;
        private Language m_SelectedLanguage = Language.Unspecified;

        public void OnMusicMuteChanged(bool isOn)
        {
            GameEntry.Sound.Mute("Music", !isOn);
            m_MusicVolumeSlider.gameObject.SetActive(isOn);
        }

        public void OnMusicVolumeChanged(float volume)
        {
            GameEntry.Sound.SetVolume("Music", volume);
        }

        public void OnSoundMuteChanged(bool isOn)
        {
            GameEntry.Sound.Mute("Sound", !isOn);
            m_SoundVolumeSlider.gameObject.SetActive(isOn);
        }

        public void OnSoundVolumeChanged(float volume)
        {
            GameEntry.Sound.SetVolume("Sound", volume);
        }

        public void OnUISoundMuteChanged(bool isOn)
        {
            GameEntry.Sound.Mute("UISound", !isOn);
            m_UISoundVolumeSlider.gameObject.SetActive(isOn);
        }

        public void OnUISoundVolumeChanged(float volume)
        {
            GameEntry.Sound.SetVolume("UISound", volume);
        }

        public void OnEnglishSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.English;
            RefreshLanguageTips();
        }

        public void OnChineseSimplifiedSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.ChineseSimplified;
            RefreshLanguageTips();
        }

        public void OnChineseTraditionalSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.ChineseTraditional;
            RefreshLanguageTips();
        }

        public void OnKoreanSelected(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            m_SelectedLanguage = Language.Korean;
            RefreshLanguageTips();
        }

        public void OnSubmitButtonClick()
        {
            if (m_SelectedLanguage == GameEntry.Localization.Language)
            {
                Close();
                return;
            }

            GameEntry.Setting.SetString(Game.Constant.Setting.Language, m_SelectedLanguage.ToString());
            GameEntry.Setting.Save();

            GameEntry.Sound.StopMusic();
            UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Restart);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            ReferenceCollector collector = ILForm.ReferenceCollector;

            // Music控制
            m_MusicMuteToggle = collector.Get("tog_MusicMute", typeof(Toggle)) as Toggle;
            m_MusicMuteToggle.onValueChanged.AddListener(OnMusicMuteChanged);
            m_MusicVolumeSlider = collector.Get("slider_MusicVolume", typeof(Slider)) as Slider;
            m_MusicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

            // Sound控制
            m_SoundMuteToggle = collector.Get("tog_SoundMute", typeof(Toggle)) as Toggle;
            m_SoundMuteToggle.onValueChanged.AddListener(OnSoundMuteChanged);
            m_SoundVolumeSlider = collector.Get("slider_SoundVolume", typeof(Slider)) as Slider;
            m_SoundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeChanged);

            // UISound控制
            m_UISoundMuteToggle = collector.Get("tog_UISoundMute", typeof(Toggle)) as Toggle;
            m_UISoundMuteToggle.onValueChanged.AddListener(OnUISoundMuteChanged);
            m_UISoundVolumeSlider = collector.Get("slider_UISoundVolume", typeof(Slider)) as Slider;
            m_UISoundVolumeSlider.onValueChanged.AddListener(OnUISoundVolumeChanged);

            // 提示画布组
            m_LanguageTipsCanvasGroup = collector.Get("CanvasGroup_LanguageTips", typeof(CanvasGroup)) as CanvasGroup;

            // 本地化按钮
            m_EnglishToggle = collector.Get("tog_English", typeof(Toggle)) as Toggle;
            m_EnglishToggle.onValueChanged.AddListener(OnEnglishSelected);
            m_ChineseSimplifiedToggle = collector.Get("tog_ChineseSimplified", typeof(Toggle)) as Toggle;
            m_ChineseSimplifiedToggle.onValueChanged.AddListener(OnChineseSimplifiedSelected);
            m_ChineseTraditionalToggle = collector.Get("tog_ChineseTraditional", typeof(Toggle)) as Toggle;
            m_ChineseTraditionalToggle.onValueChanged.AddListener(OnChineseTraditionalSelected);
            m_KoreanToggle = collector.Get("tog_Korean", typeof(Toggle)) as Toggle;
            m_KoreanToggle.onValueChanged.AddListener(OnKoreanSelected);

            // 按钮
            (collector.Get("bt_Confirm", typeof(CommonButton)) as CommonButton).OnClick.AddListener(OnSubmitButtonClick);
            (collector.Get("bt_Cancel", typeof(CommonButton)) as CommonButton).OnClick.AddListener(Close);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_MusicMuteToggle.isOn = !GameEntry.Sound.IsMuted("Music");
            m_MusicVolumeSlider.value = GameEntry.Sound.GetVolume("Music");

            m_SoundMuteToggle.isOn = !GameEntry.Sound.IsMuted("Sound");
            m_SoundVolumeSlider.value = GameEntry.Sound.GetVolume("Sound");

            m_UISoundMuteToggle.isOn = !GameEntry.Sound.IsMuted("UISound");
            m_UISoundVolumeSlider.value = GameEntry.Sound.GetVolume("UISound");

            m_SelectedLanguage = GameEntry.Localization.Language;
            switch (m_SelectedLanguage)
            {
                case Language.English:
                    m_EnglishToggle.isOn = true;
                    break;

                case Language.ChineseSimplified:
                    m_ChineseSimplifiedToggle.isOn = true;
                    break;

                case Language.ChineseTraditional:
                    m_ChineseTraditionalToggle.isOn = true;
                    break;

                case Language.Korean:
                    m_KoreanToggle.isOn = true;
                    break;

                default:
                    break;
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (m_LanguageTipsCanvasGroup.gameObject.activeSelf)
            {
                m_LanguageTipsCanvasGroup.alpha = 0.5f + 0.5f * Mathf.Sin(Mathf.PI * Time.time);
            }
        }

        private void RefreshLanguageTips()
        {
            m_LanguageTipsCanvasGroup.gameObject.SetActive(m_SelectedLanguage != GameEntry.Localization.Language);
        }
    }
}
