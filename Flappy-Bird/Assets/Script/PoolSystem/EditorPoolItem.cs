using System;
using UnityEngine;

namespace Scripts.PoolSystem {
	[Serializable]
	public class EditorPoolItem {
		[SerializeField] private PoolObject poolObject;
		[SerializeField] private int size;
		[SerializeField] private bool canUse = true;
		[SerializeField] private bool autoScale;
		[SerializeField] private int autoScaleSize;
		[SerializeField] private RectTransform parent;

		public PoolConfig GetConfig() {
			return new PoolConfig(poolObject, size, canUse, autoScale, autoScaleSize, parent);
		}
	}
}