using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Scripts.PoolSystem {
	public class PoolManger {
		private Dictionary<int, PoolHolder> poolDictionary;

		public PoolManger() {
			poolDictionary = new Dictionary<int, PoolHolder>();
		}

		public void CreatePool<T>(T poolObjectPrefab, int size, bool autoScale, int autoScaleSize, RectTransform parent)
			where T : PoolObject {
			var code = poolObjectPrefab.GetHashCode();
			if (!poolDictionary.ContainsKey(code)) {
				poolDictionary.Add(code, new PoolHolder(autoScale, autoScaleSize, parent));
			}

			poolDictionary[code].CreateItems(poolObjectPrefab, size);
		}

		public void DeletePool<T>(T poolObjectType) where T : PoolObject {
			var code = poolObjectType.GetHashCode();
			if (!poolDictionary.ContainsKey(code)) {
				return;
			}

			poolDictionary[code].DeleteItems();
		}

		public T Use<T>(T poolObjectPrefab, Vector3 position) where T : PoolObject {
			var code = poolObjectPrefab.GetHashCode();
			if (!poolDictionary.ContainsKey(code)) {
				return null;
			}

			var item = poolDictionary[code].UseItem<T>();
			if (item == null) {

				var result = poolDictionary[code].ResizePool(poolObjectPrefab);
				return !result ? null : Use(poolObjectPrefab, position);
			}

			item.transform.localPosition = position;
			return item;
		}

		public void UnUseAllByObject<T>(T poolObjectPrefab) where T : PoolObject {
			var code = poolObjectPrefab.GetHashCode();
			if (!poolDictionary.ContainsKey(code)) {
				return;
			}

			poolDictionary[code].UnUseItems();
		}
	}
}