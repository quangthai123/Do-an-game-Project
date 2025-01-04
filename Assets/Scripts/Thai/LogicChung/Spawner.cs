using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> objPrefabs;
    [SerializeField] protected List<Transform> poolObjs;
    protected Transform holder;
    protected void Reset()
    {
        LoadPrefabsAndHolder();
    }
    protected virtual void Start()
    {
        LoadPrefabsAndHolder();
    }
    protected void LoadPrefabsAndHolder()
    {
        holder = transform.Find("Holder");
        objPrefabs.Clear();
        Transform prefab = transform.Find("Prefab");
        foreach (Transform tran in prefab)
        {
            objPrefabs.Add(tran);
        }
    }
    protected Transform GetObjByName(string name)
    {
        foreach(Transform tran in poolObjs)
        {
            if(tran.name == name+"(Clone)") 
                return tran;
        }
        return null;
    }
    public Transform Spawn(string objName, Vector3 pos, Quaternion rot)
    {
        Transform obj = GetObjByName(objName);
        if(obj == null)
        {
            foreach (Transform tran in objPrefabs)
            {
                if (tran.name == objName)
                    obj = Instantiate(tran, pos, rot, holder);
            }
        } else
        {
            obj.SetPositionAndRotation(pos, rot);
            obj.parent = holder;
        }
        obj.gameObject.SetActive(true);
        return obj;
    }
    protected void Despawn(Transform go)
    {
        go.gameObject.SetActive(false);
        if(!poolObjs.Contains(go))
            poolObjs.Add(go);
    }
}
