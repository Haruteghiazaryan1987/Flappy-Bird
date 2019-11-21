using System;
using Scripts.PoolSystem;
using UnityEngine;

namespace Script {
    public abstract class AbstractControl : PoolObject {
        
        public event Action PipeOffScreen;

        public abstract void Paused();

        public virtual void Play() {
            
        }
        protected void PrefabOffScreen() {
            PipeOffScreen?.Invoke();
        }
    }
}
