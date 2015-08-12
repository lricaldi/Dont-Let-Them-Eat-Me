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
    private bool            m_makeEnemies;

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
        SceneManager.instance.getUIScript().getEnemyCounter().setCounter(m_enemiesLeft);
        m_activeEnemies = 0;
        m_makeEnemies = false;
   
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!m_makeEnemies  || SceneManager.instance.isGameFinished()) return;
        
        m_timer += Time.deltaTime;

        if (m_activeEnemies <= 0 && m_enemiesLeft <= 0)
        {
            SceneManager.instance.targetWon();
            m_makeEnemies = false;
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

    public void startMakingEnemies(bool doMake)
    {
        m_makeEnemies = doMake;
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

    public Enemy getNextEnemyToKill(BeltItem.EffectTypeEnum effectType)
    {
        /*
         * if the effect is normal or heavy find the column with the most enemies and drop it there.
         * otherwise we try to find an enemy with same effect if not found use the top most enemy.
         */
        pathNode bestMatch = null;
        
        if (effectType == BeltItem.EffectTypeEnum.ETE_NORMAL || effectType == BeltItem.EffectTypeEnum.ETE_HEAVY)
        {
            bestMatch = m_pathPanel.getTopMostNode(true);
        }
        else
        {
            int[] colTopRows = m_pathPanel.getTopMostNodes();

            for (int colIndex = 0; colIndex < PathPanel.NUM_COLS; colIndex++)
            {
                if(colTopRows[colIndex] < 0) continue;
                pathNode curNode = m_pathPanel.getNode(colIndex, colTopRows[colIndex]);
                
                if (bestMatch == null)
                {
                    bestMatch = curNode;
                }
                else 
                {
                    if (effectType == curNode.m_refEnemy.EffectType)
                    {
                        if (bestMatch.m_refEnemy.EffectType == effectType)
                        {
                            float randNumb = Random.Range(0, 10);
                            bestMatch = (randNumb > 5) ? curNode : bestMatch;
                        }
                        else
                        {
                            bestMatch = curNode;
                        }
                    }
                    else if (bestMatch.m_refEnemy.EffectType != effectType && curNode.m_row <= bestMatch.m_row)
                    {
                        bestMatch = curNode;
                    }
                }
            }
        }
        if (bestMatch != null)
        {
            return bestMatch.m_refEnemy;
        }
        return null;
        
    }
}
