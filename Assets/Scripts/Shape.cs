using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void MoveUp(int scale)
    {
        Move(new Vector3(0, 1 * scale, 0));
    }

    public void MoveDown(int scale)
    {
        Move( new Vector3(0, -1 * scale, 0) );
    }

    public void MoveLeft(int scale)
    {
        Move(new Vector3(-1 * scale, 0, 0));
    }

    public void MoveRight(int scale)
    {
        Move(new Vector3(1 * scale, 0, 0));
    }

    public void Rotate(int direction)    
    {
        transform.Rotate(new Vector3(0, 0, direction * 90));
    }
}
