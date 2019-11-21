using UnityEngine;

namespace Scripts.PoolSystem {
	public class PoolConfig {
		private PoolObject poolObject;
		private int size;
		private bool canUse;
		private bool autoScale;
		private int autoScaleSize;
		private RectTransform parent;

		public RectTransform Parent => parent;


		public PoolObject PoolObject => poolObject;

		public int Size => size;

		public bool CanUse => canUse;

		public bool AutoScale => autoScale;

		public int AutoScaleSize => autoScaleSize;

		public PoolConfig(PoolObject poolObject, int size, bool canUse, bool autoScale, int autoScaleSize,RectTransform parent) {
			this.poolObject = poolObject;
			this.size = size;
			this.canUse = canUse;
			this.autoScale = autoScale;
			this.autoScaleSize = autoScaleSize;
			this.parent = parent;
		}
	}
}