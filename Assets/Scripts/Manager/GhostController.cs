using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : Shape {

    Shape m_ghostShape;

    public void Reset()
    {
        Destroy(m_ghostShape.gameObject);
    }

    public void Create(Shape shape)
    {
        m_ghostShape = Instantiate(shape, shape.transform.position, shape.transform.rotation) as Shape;
        m_ghostShape.name = "Ghost Shape";
        SpriteRenderer[] spriteRenderers = m_ghostShape.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer r in spriteRenderers)
        {
            r.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTransform(Vector3 position, Quaternion rotation)
    {
        m_ghostShape.transform.position = position;
        m_ghostShape.transform.rotation = rotation;
    }
}
