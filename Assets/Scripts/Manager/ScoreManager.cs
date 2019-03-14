using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text level;
    public Text lines;
    public Text score;

    int m_level = 1;
    int m_lines = 0;
    int m_score = 0;

	// Use this for initialization
	void Start () {
        level.text = m_level.ToString();
        lines.text = m_lines.ToString();
        score.text = m_score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelUp()
    {

    }

    public void UpdateScore(int rowsCleared)
    {        
        m_score += rowsCleared;
        score.text = m_score.ToString();
    }

}
