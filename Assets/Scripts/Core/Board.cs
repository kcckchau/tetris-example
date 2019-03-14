using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    // Use this for initialization
    public Transform p_emptyCell;
    public int m_width = 10;
    public int m_height = 30;
    public int m_header = 8;

    Transform[,] m_grids;

    void Awake()
    {
        m_grids = new Transform[m_width, m_height];
        for(int i = 0; i < m_width; ++i)
        {
            for (int j = 0; j < m_width; ++j)
            {
                m_grids[i, j] = null;
            }
        }
    }

    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void DrawEmptyCells()
    {
        for (int i = 0; i < m_width; ++i)
        {
            for (int j = 0; j < m_height - m_header; ++j)
            {
                Transform clone = Instantiate(p_emptyCell, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity) as Transform;
                clone.name = "BoradSpace " + i + "," + j;
                clone.parent = transform;
            }
        }
    }

    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector3 childPos = child.position;
            if (m_grids[(int)childPos.x, (int)childPos.y])
            {
                return false;
            }

        }
        return true;
    }

    public bool IsWithinBoard(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector3 childPos = child.position;
            //Vector3 board = child.position;
            //Debug.Log("Child pos " + childPos);
            //Debug.Log("Board pos " + board);
            if (!(childPos.x > 0 && childPos.x < m_width && childPos.y > 0))
            {
                Debug.Log("Wrong Child pos " + childPos);
                return false;
            }

        }
        return true;
    }

    public bool IsAboveTop(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector3 childPos = child.position;
            if (childPos.y > m_height - m_header - 1)
            {
                Debug.Log("Above top " + childPos);
                return true;
            }

        }
        return false;
    }

    public void StoreShape(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector3 childPos = child.position;
            m_grids[(int)childPos.x, (int)childPos.y] = child;
        }
    }

    private void ClearRow(int row)
    {
        for (int i = row; i < m_width; ++i)
        {
            GameObject.Destroy(m_grids[i, row].gameObject);
            m_grids[i, row] = null;
        }
    }

    private void ShiftRow(int row)
    {
        for (int i = row; i < m_height - m_header - 1; ++i)
        {
            for (int j = 0; j < m_width; ++j)
            {
                m_grids[j, i] = null;
                if (m_grids[j, i + 1])
                {
                    //m_grids[j, i + 1].position += (new Vector3(0, -1, 0));
                    m_grids[j, i + 1].Translate(new Vector3(0, -1, 0), Space.World);
                    m_grids[j, i] = m_grids[j, i + 1];
                }
            }
        }

    }

    public int CheckRow()
    {
        int rowsCleared = 0;
        for (int i = 0; i < m_height - m_header; ++i)
        {
            bool isRowClear = true;
            //for (int i = 0; i < 1; ++i)
            for (int j = 0; j < m_width; ++j)
            {
                if (m_grids[j, i] == null)
                {
                    //Debug.Log(j + "," + i + " null ");
                    isRowClear = false;
                }
            }

            if (isRowClear)
            {
                Debug.Log("Row " + i + " Clear ");
                ClearRow(i);
                ShiftRow(i);
                i--;
                rowsCleared++;
            }
        }
        return rowsCleared;
    }

    public int ghostShapeDistance(Shape shape)
    {
        int distance = m_height;
        foreach (Transform tmp in shape.transform)
        {
            int tmp2 = Mathf.FloorToInt(tmp.position.y);
            bool found = false;
            for (int i = m_height - m_header - 1; i >= 0;  --i)
            {
                if (m_grids[(int)tmp.position.x,i] != null)
                {
                    tmp2 = (int)tmp.position.y - i - 1;
                    found = true;
                    if (tmp2 < distance)
                    {
                        distance = tmp2;                        
                    }
                    break;
                }
            }
            if (found == false)
            {
                if (tmp2 < distance)
                {
                    distance = tmp2;
                }
            }
        }
        return distance;
    }
}