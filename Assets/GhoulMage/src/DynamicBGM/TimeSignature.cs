using System;
using UnityEngine;

namespace GhoulMage.BGM {
	[Serializable]
	public struct TimeSignature {
		public byte Top;
		[SerializeField]
		private Note _bottom;

		public byte Bottom { get { return (byte) _bottom; } }
		public float BeatUnit { get { return 4 * (4 / Bottom); } }

		public enum Note : byte {
			HalfNote = 2,
			QuarterNote = 4,
			EighthNote = 8,
			SixteenNote = 16,
		}

		public TimeSignature(byte top, Note bottom) {
			Top = top;
			_bottom = bottom;
		}

		public static TimeSignature FourFour { get { return new TimeSignature(4, Note.QuarterNote); } }
		public static TimeSignature ThreeFour { get { return new TimeSignature(3, Note.QuarterNote); } }
		public static TimeSignature TwoFour { get { return new TimeSignature(2, Note.QuarterNote); } }
		public static TimeSignature SixEight { get { return new TimeSignature(6, Note.EighthNote); } }
	}
}
