using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    Board m_board;
    Spawner m_spawner;
    ScoreManager m_scoreManager;
    SoundManager m_soundManager;
    GhostController m_ghostController;

    Shape m_activeShape;
    Shape m_ghostShape;

    float m_timeToDrop = 0f;
    [Range(0f, 5f)]
    public float m_dropInterval = 1.0f;
    [Range(0f, 5f)]
    public float m_keyDownInterval = 0.5f;
    [Range(0f, 5f)]
    public float m_keyLeftInterval = 0.5f;
    [Range(0f, 5f)]
    public float m_keyRightInterval = 0.5f;
    [Range(0f, 5f)]
    public float m_keyRotateInterval = 0.5f;

    public float m_ghostShapeInterval = 0.25f;


    float m_timeToNextKeyDown = 0f;
    float m_timeToNextKeyLeft = 0f;    
    float m_timeToNextKeyRight = 0f;
    float m_timeToNextKeyRotate = 0f;
    float m_timeToUpdateGhostShape = 0f;

    bool m_endGame = false;



    // Use this for initialization
    void Start ()
    {
        m_board = FindObjectOfType<Board>();
        m_spawner = FindObjectOfType<Spawner>();
        m_scoreManager = FindObjectOfType<ScoreManager>();
        m_soundManager = FindObjectOfType<SoundManager>();
        m_ghostController = FindObjectOfType<GhostController>();

        if (!m_board)
        {
            Debug.LogError("Board not found");
        }
        if (!m_spawner)
        {
            Debug.LogError("Spawner not found");
        }
        if (!m_scoreManager)
        {
            Debug.LogError("ScoreManager not found");
        }
        if (!m_soundManager)
        {
            Debug.LogError("SoundManager not found");
        }

        m_activeShape = m_spawner.SpawnShape() as Shape;
        m_ghostController.Create(m_activeShape);

        if (!m_activeShape)
            Debug.LogError("Failed to Spawn ActiveShape");
        
        //a.x = 0;
        //Debug.LogError("a: " + a.x);
        //Application.targetFrameRate = 120;
        //QualitySettings.vSyncCount = 0;
        Debug.Log("TargetFrameRate: " + Application.targetFrameRate);

    }

    void LandShape()
    {
        m_board.StoreShape(m_activeShape);

        int rowsClears = 0;
        rowsClears = m_board.CheckRow();
        if(rowsClears > 0)
        {
            m_soundManager.PlayRowClearVocal();
        }

        m_scoreManager.UpdateScore(rowsClears);

        if (m_board.IsAboveTop(m_activeShape))
        {
            m_soundManager.PlayGameOverSound();
            m_endGame = true;
        }
        m_activeShape = m_spawner.SpawnShape();
        m_ghostController.Reset();
        m_ghostController.Create(m_activeShape);
        m_soundManager.PlayLandShapeSound();

    }
    // Update is called once per frame
    void Update()
    {
        if (m_endGame)
            return;
        //Debug.Log("time " + Time.time + " haha " + m_dropInterval + " haha " + m_timeToDrop);


        if (m_activeShape && Time.time > m_timeToDrop || Input.GetButtonDown("MoveDown") || (Input.GetButton("MoveDown") &&  Time.time > m_timeToNextKeyDown))
        {
            m_activeShape.MoveDown(1);
            if( !m_board.IsWithinBoard(m_activeShape) || !m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveUp(1);
                LandShape();
            }
            m_timeToDrop = Time.time + m_dropInterval;
            m_timeToNextKeyDown = Time.time + m_keyDownInterval;
        }

        if (Input.GetButton("MoveLeft") && Time.time > m_timeToNextKeyLeft)
        {
            m_activeShape.MoveLeft(1);
            if (!m_board.IsWithinBoard(m_activeShape) || !m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight(1);
                m_soundManager.PlayErrorSound();
            }
            m_timeToNextKeyLeft = Time.time + m_keyLeftInterval;
        }
        else if (Input.GetButton("MoveRight") && Time.time > m_timeToNextKeyRight)
        {
            m_activeShape.MoveRight(1);
            if (!m_board.IsWithinBoard(m_activeShape) || !m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft(1);
            }
            m_timeToNextKeyRight = Time.time + m_keyRightInterval;
        }
        else if (Input.GetButton("Rotate") && Time.time > m_timeToNextKeyRotate)
        {
            m_activeShape.Rotate(1);
            m_soundManager.PlayMoveSound();
            if (!m_board.IsWithinBoard(m_activeShape) || !m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.Rotate(-1);
            }            
            m_timeToNextKeyRotate = Time.time + m_keyRotateInterval;

        }

    }

    void LateUpdate()
    {
        // Update GhostShape        
        int tmp = m_board.ghostShapeDistance(m_activeShape);
        Vector3 position = m_activeShape.transform.position - (new Vector3(0, tmp, 0));
        Quaternion rotation = m_activeShape.transform.rotation;
        m_ghostController.UpdateTransform(position, rotation);
    }


}
