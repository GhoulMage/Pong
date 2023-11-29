using UnityEngine;

namespace GhoulMage.BGM {
	public class BGM {
		public BGMAsset BGMAsset { get; private set; }
		public int SampleSwitchTolerance { get { return _sampleSwitchTolerance; } }
		public float LengthSeconds { get { return _durationInSeconds; } }
		public BGMSection[] Sections { get { return _splicedSections; } }
		public int SectionCount { get { return _splicedSections.Length; } }
		public int Channels { get; private set; }
		public int Frequency { get; private set; }

		public BGMSection this[int index] {
			get {
				return _splicedSections[index];
			}
		}
		public bool Has(string name) {
			foreach (var section in _splicedSections) {
				if (section.Name == name)
					return true;
			}

			return false;
		}
		public int IndexOf(string name) {
			for(int i =0; i < _splicedSections.Length; i++) {
				if (_splicedSections[i].Name == name)
					return i;
			}

			return -1;
		}

		public struct BGMSection {
			private string _name;
			private float[] _data;
			private int _changeSensitivity;

			public string Name { get { return _name; } }
			public float[] AudioData { get { return _data; } }
			public int SwitchTolerance { get { return _changeSensitivity; } }

			public BGMSection(string name, int dataLength, int changeSensitivity) {
				_name = name;
				_data = new float[dataLength];
				_changeSensitivity = changeSensitivity;
			}
		}

		private AudioClip _audioClip;
		private double _bpmConstant;
		private float _durationInSeconds;
		private int _durationInSamples;
		private int _sampleSwitchTolerance;
		private TimeSignature _timeSignature;
		private BGMSection[] _splicedSections;

		private BGM(BGMAsset bgmAsset) {
			BGMAsset = bgmAsset;
			_audioClip = BGMAsset.AudioClip;

			Channels = _audioClip.channels;
			Frequency = _audioClip.frequency;
			_durationInSamples = _audioClip.samples;
			_durationInSeconds = _audioClip.length;
			_bpmConstant = BGMAsset.Info.BPMConstant;
			_timeSignature = BGMAsset.Info.TimeSignature;
		}

		public static BGM From(BGMAsset bgmAsset) {
			BGM result = new BGM(bgmAsset);

			result.GetBGMConstants();
			result.SpliceBGM();

			return result;
		}

		private void GetBGMConstants() {
			_audioClip.LoadAudioData();

			double fullBar = MusicMath.BeatInSeconds(_timeSignature.Top+1, _bpmConstant, _timeSignature);
			_sampleSwitchTolerance = Mathf.FloorToInt((float) (fullBar * Mathf.FloorToInt(_durationInSamples / _durationInSeconds))) * Channels;

			_audioClip.UnloadAudioData();
		}

		private void SpliceBGM() {
			int oneSecondOfSamples = Mathf.FloorToInt(_durationInSamples / _durationInSeconds);
			_splicedSections = new BGMSection[BGMAsset.Sections.Length];

			for (int i = 0; i < _splicedSections.Length; i++) {
				double startBeat = MusicMath.BeatInSeconds(BGMAsset.Sections[i].StartBeat, _bpmConstant, _timeSignature);
				double endBeat = MusicMath.BeatInSeconds(BGMAsset.Sections[i].EndBeat, _bpmConstant, _timeSignature);

				int sectionSamplesStart  = Mathf.FloorToInt((float)(startBeat * oneSecondOfSamples));
				int sectionSamplesLength = Mathf.FloorToInt((float)(endBeat * oneSecondOfSamples)) - sectionSamplesStart;

				_splicedSections[i] = new BGMSection(BGMAsset.Sections[i].Name, sectionSamplesLength * Channels, BGMAsset.Sections[i].ChangeSensitivity);
#if UNITY_EDITOR
				Debug.Log($"Splicing section {_splicedSections[i].Name} at index {i}...");
				if (_audioClip.GetData(_splicedSections[i].AudioData, sectionSamplesStart))
					Debug.Log($"Succesful splice of {_splicedSections[i].Name} at index {i}.");
				else {
					Debug.Log($"Splice error info:\n\tStart Beat: {startBeat} seconds.\n\tEnd Beat: {endBeat} seconds.\n\tSample Start: {sectionSamplesStart}\n\tSample Length: {sectionSamplesLength}");
				}
#else
			if(!_audioClip.GetData(_splicedSections[i].AudioData, sectionSamplesStart))
				Debug.LogWarning("Error loading BGM Data!");
#endif
			}
		}
	}
}
