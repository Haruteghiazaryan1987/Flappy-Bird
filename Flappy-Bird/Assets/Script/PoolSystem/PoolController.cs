using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PoolSystem {
	public class PoolController : MonoBehaviour {
		[SerializeField] private List<EditorPoolItem> editorPoolItems;
		private PoolManger poolManger;

		public void InitPool() {
			poolManger = new PoolManger();
			editorPoolItems.ForEach(editorItem => {
				var config = editorItem.GetConfig();
				if (!config.CanUse) {
					return;
				}

				poolManger.CreatePool(config.PoolObject, config.Size, config.AutoScale, config.AutoScaleSize,config.Parent);
			});
		}

		public T Use<T>(T poolObjectPrefab,Vector3 position) where T : PoolObject {
			return poolManger.Use(poolObjectPrefab,position);
		}

		public void UnUseItems<T>(T poolObjectPrefab) where T : PoolObject {
			poolManger.UnUseAllByObject(poolObjectPrefab);
		}
	}
}