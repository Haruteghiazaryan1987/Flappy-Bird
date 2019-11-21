using System.Collections.Generic;
using Scripts.PoolSystem;
using UnityEngine;

namespace Script {
    public abstract class AbstractManager : MonoBehaviour {
        [SerializeField] private AbstractControl controlPrefab;
        [SerializeField] private PoolController poolController;
        public AbstractControl ControlPrefab => controlPrefab;

        public PoolController PoolController => poolController;

        public void StopPrefab(List<AbstractControl> list) {
            if(list==null)
                return;
            for (int i = 0; i < list.Count; i++) {
                AbstractControl obj = list[i].GetComponent<AbstractControl>();
                obj.Paused();
            }
        }
        public void PlayPrefab(List<AbstractControl> list) {
            if(list==null)
                return;
            for (int i = 0; i < list.Count; i++) {
                AbstractControl obj = list[i].GetComponent<AbstractControl>();
                obj.Play();
            }
        }
        public virtual void Paused() {
        }
        public abstract void ReusePrefab();

        public virtual void Setup() {
        }
        public virtual void Play() {
        }
    }
}