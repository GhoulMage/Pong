namespace GhoulMage.BGM {
	public static class MusicMath {
		/// <summary>
		/// Returns the length of a bar in seconds for the specified beats per minute
		/// </summary>
		public static double BPMConstant(int BPM) {
			return 1 / (BPM / 60.0);
		}

		/// <summary>
		/// Returns seconds elapsed between beats 1 and the specified beat in a 4/4 time signature
		/// </summary>
		public static double BeatInSeconds(int beat, int bpm) {
			return ((beat - 1) * 4) * BPMConstant(bpm);
		}

		/// <summary>
		/// Returns seconds elapsed between beats 1 and the specified beat in the specified time signature
		/// </summary>
		public static double BeatInSeconds(int beat, int bpm, TimeSignature timeSignature) {
			return ((beat - 1) * timeSignature.BeatUnit) * BPMConstant(bpm);
		}

		/// <summary>
		/// Returns seconds elapsed between beats 1 and the specified beat in a 4/4 time signature
		/// </summary>
		public static double BeatInSeconds(int beat, double bpmConstant) {
			return ((beat - 1) * 4) * bpmConstant;
		}

		/// <summary>
		/// Returns seconds elapsed between beats 1 and the specified beat in a 4/4 time signature
		/// </summary>
		public static double BeatInSeconds(int beat, double bpmConstant, TimeSignature timeSignature) {
			return ((beat - 1) * timeSignature.BeatUnit) * bpmConstant;
		}
	}
}
