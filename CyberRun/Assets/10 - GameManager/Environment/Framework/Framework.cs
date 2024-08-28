using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framework 
{
    private GameObject model = null;

    public Framework Blueprint(GameObject frameworkPF)
    {
        model = Object.Instantiate<GameObject>(frameworkPF);

        return (this);
    } 

    public Framework Assemble(GameObject[] prefabList, string anchorName, float turn = 0.0f)
    {
        int selection = Random.Range(0, prefabList.Length);

        return (Assemble(prefabList[selection], anchorName, turn));
    }

    public Framework Assemble(GameObject prefab, string anchorName, float turn = 0.0f)
    {
        Transform anchors = model.transform.Find("Anchors");
        Transform anchor = anchors.Find(anchorName);
        
        GameObject go = Object.Instantiate(prefab, anchor);
        go.transform.rotation = Quaternion.Euler(new Vector3(0.0f, turn, 0.0f));
           
        return (this);
    }

    public Framework Apply(GameObject go, string anchorName, float turn = 0.0f)
    {
        Transform anchors = model.transform.Find("Anchors");
        Transform anchor = anchors.Find(anchorName);

        go.transform.position = anchor.transform.position;
        go.transform.rotation = Quaternion.Euler(new Vector3(0.0f, turn, 0.0f));
        go.transform.parent = anchor;

        return (this);
    }

    public Framework Decorate(int n, GameObject[] prefab, float size)
    {
        for (int i = 0; i < n; i++ ) {
            int choice = Random.Range(0, prefab.Length);
            Decorate(1, prefab[choice], size);
        }

        return (this);
    }

    public Framework Decorate(int n, GameObject prefab, float size)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject go = Object.Instantiate(prefab, model.transform);
            Vector2 position = Random.insideUnitCircle * size;
            go.transform.localPosition = new Vector3(position.x, 0.0f, position.y);
            float turn = Random.Range(0.0f, 360.0f);
            go.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, turn, 0.0f));
        }

        return (this);
    }

    public Framework Position(Vector3 position)
    {
        model.transform.position = position;

        return (this);
    }

    public GameObject Build(Transform parent = null)
    {
        if (parent != null)
        {
            model.transform.SetParent(parent.transform);
        }

        return (model);
    }
}
