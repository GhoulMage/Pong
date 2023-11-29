using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GhoulMage.BGM {
	[CreateAssetMenu(menuName = "Sound/BGM Asset")]
	public class BGMAsset : ScriptableObject {
		public AudioClip _audioClip;

		[SerializeField]
		BGMInfo _bgmInfo;

		[SerializeField]
		BGMSectionData[] _sections;

		public AudioClip AudioClip { get { return _audioClip; } }
		public BGMInfo Info { get { return _bgmInfo; } }
		public BGMSectionData[] Sections { get { return _sections; } }

		public BGM ToBGM() {
			return BGM.From(this);
		}
	}
}
