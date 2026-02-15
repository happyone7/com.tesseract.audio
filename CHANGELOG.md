# Changelog

## [1.1.1] - 2026-02-15
### Fixed
- 모든 파일/폴더에 .meta 파일 추가 (Unity immutable folder 경고 해결)

## [1.1.0] - 2026-02-14
### Fixed
- FadingAudioSource: FadeOut()이 즉시 음소거되던 버그 수정 (startVolume 기반 보간으로 변경)
- AudioManager.PlayBgm(): null clip 전달 시 NullReferenceException 수정
- AudioCategory: 앱 종료 시 Singleton 자동 생성 방지 (HasInstance 가드 추가)

### Changed
- AudioManager: 중복 Init() 호출 시 AudioSource 누적 방지
- AudioManager.MuteAll()/UnmuteAll(): 뮤트 전 볼륨 저장/복원
- m_PlayingAudios → _playingAudios 네이밍 컨벤션 통일

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
