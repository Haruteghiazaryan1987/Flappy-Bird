using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Script {
    public enum ManagerType {
        Background,
        Pipe,
        Earth
    }

    [Serializable]
    public class Aggregator {
        [SerializeField] private ManagerType managerType;
        [SerializeField] private AbstractManager manager;

        public ManagerType ManagerType => managerType;

        public AbstractManager Manager => manager;

        public void Setup() {
            manager.Setup();
        }

        public void Play() {
            manager.Play();
        }
        public void Paused() {
            manager.Paused();
        }
    }

    public class ManagerAggregation : MonoBehaviour {
        [SerializeField] private List<Aggregator> aggregators;

        public void SetupManagers() {
            aggregators.ForEach(aggregator => aggregator.Setup());
        }

        public void PlayManagers() {
            aggregators.ForEach(aggregator => aggregator.Play());
        }

        public void PausedManager() {
            aggregators.ForEach(aggregator => aggregator.Paused());
        }

        public T GetManager<T>(ManagerType managerType) where T : AbstractManager {
            var aggregator = aggregators.Find(a => a.ManagerType == managerType);
            if (aggregator == null) {
                return null;
            }

            return aggregator.Manager as T;
        }
    }
}