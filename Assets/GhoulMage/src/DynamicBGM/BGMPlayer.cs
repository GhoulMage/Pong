using UnityEngine;

namespace GhoulMage.BGM {
	[RequireComponent(typeof(AudioSource))]
	public class BGMPlayer : MonoBehaviour {
		public AudioSource AudioSource;

		private BGM _currentBGM;

		private int _currentSectionIndex, _nextSection;
		private AudioClip _dynamicAudioClip;
		private int _currentSampleTime;

		public string CurrentSection {
			get {
				return _currentBGM[_currentSectionIndex].Name;
			}
		}
		public int CurrentSectionIndex {
			get {
				return _currentSectionIndex;
			}
		}

		private void OnDisable() {
			Destroy(_dynamicAudioClip);
			AudioSource.Stop();
		}

		public void PlayBGM(BGM theme) {
			_currentBGM = theme;
			_currentSectionIndex = 0;
			CreateDynamicAudioClip(theme.Channels, theme.Frequency);

			AudioSource.loop = true;
			AudioSource.clip = _dynamicAudioClip;
			AudioSource.Play();
		}

		private void CreateDynamicAudioClip(int channels, int frequency) {
			if (_dynamicAudioClip != null)
				Destroy(_dynamicAudioClip);

			_dynamicAudioClip = AudioClip.Create("_dynamic", 4096 * channels, channels, frequency, true, OnAudioRead);
		}
		
		private void OnAudioRead(float[] data) {
			int requestedSamples = data.Length;
			BGM.BGMSection currentSection = _currentBGM[_currentSectionIndex];

			for (int i = 0; i < requestedSamples; i++) {
				//Check if we can switch the section.
				if (_currentSampleTime % (_currentBGM.SampleSwitchTolerance * currentSection.SwitchTolerance) == 0 && _nextSection != _currentSectionIndex) {
					_currentSectionIndex = _nextSection;
					currentSection = _currentBGM[_currentSectionIndex];
					_currentSampleTime = 0;
				}

				if (_currentSampleTime >= currentSection.AudioData.Length) {
					if (_nextSection != _currentSectionIndex) {
						_currentSectionIndex = _nextSection;
						currentSection = _currentBGM[_currentSectionIndex];
					}

					_currentSampleTime = 0; //Wrap around
				}

				data[i] = currentSection.AudioData[_currentSampleTime];
				_currentSampleTime++;
			}
		}
		

		public void ScheduleSection(int sectionIndex) {
			if (sectionIndex < 0 || sectionIndex >= _currentBGM.SectionCount) {
#if UNITY_EDITOR
				Debug.LogError($"Section Index out of bounds: {sectionIndex}");
#endif
				return;
			}
			_nextSection = sectionIndex;
		}
		public void ChangeSectionImmediate(int sectionIndex) {
			if (sectionIndex < 0 || sectionIndex >= _currentBGM.SectionCount) {
#if UNITY_EDITOR
				Debug.LogError($"Section Index out of bounds: {sectionIndex}");
#endif
				return;
			}

			_currentSectionIndex = _nextSection = sectionIndex;
		}
		public bool ScheduleSection(string sectionName) {
			if (_currentBGM.Has(sectionName)) {
				_nextSection = _currentBGM.IndexOf(sectionName);
				return true;
			}

			return false;
		}
		public bool ChangeSectionImmediate(string sectionName) {
			if (_currentBGM.Has(sectionName)) {
				_currentSectionIndex = _nextSection = _currentBGM.IndexOf(sectionName);
				return true;
			}

			return false;
		}
	}
}
