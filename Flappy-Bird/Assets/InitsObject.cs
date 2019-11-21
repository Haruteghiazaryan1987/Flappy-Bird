using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InitsObject : MonoBehaviour
{
    public event Action<object> GoOnInits;
    [SerializeField] private GameObject prefab;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos=new Vector3(Random.Range(-30,30),Random.Range(-50,50));
            var go = Instantiate(prefab,pos,Quaternion.identity);
            GoOnInits?.Invoke(go);
        }
    }
}
