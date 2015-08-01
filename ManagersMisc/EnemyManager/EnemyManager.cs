using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour 
{
    private const float CREATE_INTERVAL = 0.1f;
    private const int   ENEMIES_TO_KILL = 40;

    private PathPanel       m_pathPanel;
    private float           m_timer;
    private int             m_enemiesLeft;
    private int             m_activeEnemies;
    private bool            m_done;
    Dictionary<int, Enemy> m_enemiesOnPath;
    
    public void DebugEnemiesInPath()
    {
        foreach (KeyValuePair<int, Enemy> curEnemy in m_enemiesOnPath)
        {
            Debug.Log(" IN PATH " + curEnemy.Value.gameObject.GetInstanceID() + " POS " + curEnemy.Value.GetComponent<Transform>().position);
        }
    }

    public pathNode getNode(int col, int row)
    {
        return m_pathPanel.getNode(col, row);
    }
	void Start () 
    {
        m_pathPanel     = GameObject.Find("PathPanel").gameObject.GetComponent<PathPanel>();
        m_enemiesOnPath = new Dictionary<int, Enemy>();
        m_enemiesLeft   = ENEMIES_TO_KILL;
        m_activeEnemies = 0;
        m_done          = false;
   
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_done) return;
        
        m_timer += Time.deltaTime;

        if (m_activeEnemies <= 0 && m_enemiesLeft <= 0)
        {
            SceneManager.instance.targetWon();
            m_done = true;
        }
        if (m_timer > CREATE_INTERVAL && m_enemiesLeft >0)
        {
            m_timer = 0;
            pathNode node = m_pathPanel.getFreeStartNode();
            if (node != null)
            {
                m_activeEnemies++;
                m_enemiesLeft--;

                Enemy newEnemy          = InstanceFactory.instance.getEnemy(node.m_nodeTransform.position, Quaternion.identity);
                EnemyView newEnemyView  = InstanceFactory.instance.getRandomEnemyView(Vector2.zero, Quaternion.identity);

                newEnemyView.gameObject.GetComponent<Transform>().SetParent(newEnemy.GetComponent<Transform>(), false);
                newEnemy.reset();
                newEnemy.setNodeAndView(node, newEnemyView);
               
                newEnemy.setupStates();
                newEnemy.setReady(true);
                
                if (!m_enemiesOnPath.ContainsKey(newEnemy.gameObject.GetInstanceID()))
                {
                    m_enemiesOnPath.Add(newEnemy.gameObject.GetInstanceID(), newEnemy);
                }

            }
        }
	
	}

    public void enemyKilled()
    {
        m_activeEnemies--;
    }

    public void removeFromPath(Enemy enemyToRemoveFromPath)
    {
        if (m_enemiesOnPath.ContainsKey(enemyToRemoveFromPath.gameObject.GetInstanceID()))
        {
            m_enemiesOnPath.Remove(enemyToRemoveFromPath.gameObject.GetInstanceID());
        }
    }

    public Enemy getNextEnemyToKill()
    {
        Enemy bestMatch = null;
        foreach (KeyValuePair<int, Enemy> curEnemy in m_enemiesOnPath)
        {
            if (bestMatch == null)
            {
                bestMatch = curEnemy.Value;
            }
            else
            {
                // get the top most of the column
                if (bestMatch.GetComponent<Transform>().position.y < curEnemy.Value.GetComponent<Transform>().position.y)
                {
                    bestMatch = curEnemy.Value;
                }
            }
        }
        return bestMatch;
    }
}
