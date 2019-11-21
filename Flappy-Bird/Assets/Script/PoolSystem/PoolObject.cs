using System;
using UnityEngine;

namespace Scripts.PoolSystem {
	public abstract class PoolObject : MonoBehaviour {
		public event Action<PoolObject> OnUnUseItem;
		public virtual void EnableItem() {
			gameObject.SetActive(true);
		}

		public void DisableItem() {
			gameObject.SetActive(false);
		}

		public virtual void UnUse() {
			OnUnUseItem?.Invoke(this);
		}

		public void DestroyItem() {
			DisableItem();
			Destroy(gameObject);
		}
	}
}