using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PoolSystem {
    public class PoolHolder {
        private Queue<PoolObject> poolItems;
        private List<PoolObject> activeItems;
        private Transform parent;
        private bool autoScale;
        private int autoScaleSize;
        private RectTransform parentRectTransform;

        public PoolHolder(bool autoScale, int autoScaleSize, RectTransform parent) {
            this.autoScaleSize = autoScaleSize;
            this.autoScale = autoScale;
            parentRectTransform = parent;
            poolItems = new Queue<PoolObject>();
            activeItems = new List<PoolObject>();
        }

        private Transform CreateParent<T>() {
            if (parent != null) {
                return parent;
            }

            parent = new GameObject($"{typeof(T).Name}-{typeof(T).GetHashCode()}").transform;
            parent.parent = parentRectTransform;
            var parentPosition = parent.position;
            parentPosition.z = 0;

            return parent;
        }

        private void CreateItem<T>(T poolObjectPrefab) where T : PoolObject {
            var item = Object.Instantiate(poolObjectPrefab, CreateParent<T>());
            item.OnUnUseItem += OnUnUseItem;
            item.DisableItem();
            poolItems.Enqueue(item);
        }

        private void OnUnUseItem(PoolObject poolObject) {
            UnUseItem(poolObject);
        }

        public void CreateItems<T>(T poolObjectPrefab, int size) where T : PoolObject {
            for (int i = 0; i < size; i++) {
                CreateItem(poolObjectPrefab);
            }
        }

        public bool ResizePool<T>(T poolObjectPrefab) where T : PoolObject {
            if (!autoScale) {
                return false;
            }

            CreateItems(poolObjectPrefab, autoScaleSize);
            return true;
        }

        public void DeleteItems() {
            UnUseItems();
            while (poolItems.Count > 0) {
                var item = poolItems.Dequeue();
                DestroyItem(item);
            }

            Object.Destroy(parent.gameObject);
        }

        public void UnUseItems() {
            while (activeItems.Count > 0) {
                var item = activeItems[0];
                UnUseItem(item);
            }
        }

        private bool UnUseItem(PoolObject poolObject) {
            if (activeItems.Count == 0) {
                return false;
            }

            var itemIndex = activeItems.FindIndex(activeItem => activeItem == poolObject);
            if (itemIndex == -1) {
                return false;
            }

            poolObject.DisableItem();
            activeItems.RemoveAt(itemIndex);
            poolItems.Enqueue(poolObject);
            //Debug.LogError(poolItems.Count);
            return true;
        }

        private void DestroyItem(PoolObject poolObject) {
            poolObject.DestroyItem();
        }

        public T UseItem<T>() where T : PoolObject {
            if (poolItems.Count == 0) {
                return null;
            }

            var item = poolItems.Dequeue();
            activeItems.Add(item);
            item.EnableItem();
            return item as T;
        }
    }
}