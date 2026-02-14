# Changelog

## [1.0.0] - 2026-02-14
### Added
- AudioManager: Singleton audio manager with BGM/SFX/UI/Extra channels
- AudioMixerConfig: ScriptableObject for AudioMixer configuration
- AudioCategory: Category-based concurrent playback limiting
- AudibleClipList: Fixed-size tracking of currently playing clips
- FadingAudioSource: Audio fade in/out with pause/resume support
- WeightedAudioClip / WeightedAudioList: Weighted random audio selection
- GlobalBgmAudioSource / GlobalOneShotAudioSource: Component-based audio triggers
- ESoundType: Audio channel type enum
- PlayData: Serializable audio clip + volume pair
- Logarithmic dB volume curve for natural volume sliders
