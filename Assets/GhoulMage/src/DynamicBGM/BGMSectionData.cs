using System;
using UnityEngine;

namespace GhoulMage.BGM {
	[Serializable]
	public class BGMSectionData {
		public string Name;
		[Min(1)]
		public int StartBeat = 1;
		[Min(1)]
		public int EndBeat = 2;

		[Range(1, 8)]
		public int ChangeSensitivity = 2;
	}
}
