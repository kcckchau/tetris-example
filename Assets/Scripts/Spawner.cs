using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    public Shape[] m_shapes;

    void Awake()
    {
        Debug.Log("Awake", this);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable", this);
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start", this);
    }

    // Update is called once per frame
    void Update () {		
	}

    private void OnApplicationQuit()
    {

    }

    void onDisable ()
    {
        Debug.Log("onDisable", this);
    }

    void onDestroy ()
    {
        Debug.Log("onDestroy", this);
    }

    public Shape SpawnShape ()
    {
        int i = Random.Range(0, m_shapes.Length);
        Shape tmp = GameObject.Instantiate(m_shapes[i], transform.position, Quaternion.identity) as Shape;
        return tmp;

    }
}
