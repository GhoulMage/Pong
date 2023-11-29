using System;
using UnityEngine;

namespace GhoulMage.BGM {
	[Serializable]
	public class BGMInfo {
		[SerializeField]
		int _BPM;

		[SerializeField]
		TimeSignature _timeSignature = TimeSignature.FourFour;

		public int BPM { get { return _BPM; } }
		public TimeSignature TimeSignature { get { return _timeSignature; } }
		public double BPMConstant { get { return MusicMath.BPMConstant(BPM); } }
	}
}
