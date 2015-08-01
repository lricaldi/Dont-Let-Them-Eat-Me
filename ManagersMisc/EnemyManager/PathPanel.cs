using UnityEngine;
using System.Collections;

public class pathNode
{
    public Transform    m_nodeTransform;
    //public bool         m_inUse;
    public pathNode     m_nextNode;
    public bool         m_fastMove;

    public int          m_col;
    public int          m_row;

    public Enemy        m_refEnemy = null;

    public pathNode(Transform node, pathNode nextNode, bool fastMove, int col, int row)
    {
        m_fastMove      = fastMove;
        m_nextNode      = nextNode;
        m_nodeTransform = node;
        //m_inUse         = false;
        m_col           = col;
        m_row           = row;
        m_refEnemy      = null;

    }
}

public class pathColumn
{
    public pathNode[] m_nodes;

    public pathColumn(pathNode[] nodes)
    {
        m_nodes = nodes;
    }
}

public class PathPanel : MonoBehaviour 
{
    private const int NUM_COLS      = 5;
    private const int NUM_ROWS      = 6;
    private const int FAST_MOVE_ROW = 4;

    private pathColumn[]    m_columns;
    private int[]           m_freeNodes;

	void Start () 
    {
        buildPathPanel();
        m_freeNodes = new int[NUM_COLS];
	}


    private void buildPathPanel()
    {
        m_columns = new pathColumn[NUM_COLS];
        for (int colIndex = 0; colIndex < NUM_COLS; colIndex++)
        {
            pathNode[] nodes    = new pathNode[NUM_ROWS];
            for (int rowIndex = 0; rowIndex < NUM_ROWS; rowIndex++)
            {
                Transform nodeTrans = GetComponent<Transform>().Find("pathCol" + colIndex + "Row" + rowIndex);
                pathNode nextNode   = (rowIndex !=0)?nodes[rowIndex-1]: null;
                nodes[rowIndex]     = new pathNode(nodeTrans, nextNode, rowIndex >= FAST_MOVE_ROW, colIndex, rowIndex);
            }

            m_columns[colIndex] = new pathColumn(nodes);
        }
    }

    public pathNode getNode(int col, int row)
    {
        if (col < 0 || col >= NUM_COLS) return null;
        if (row < 0 || row >= NUM_ROWS) return null;

        return m_columns[col].m_nodes[row];
    }

    public pathNode getFreeStartNode()
    {
        int curFree     = 0;
        int firstRow    = NUM_ROWS - 1;

        for (int i = 0; i < NUM_COLS; i++)
        {
            //if (!m_columns[i].m_nodes[firstRow].m_inUse)
            if (m_columns[i].m_nodes[firstRow].m_refEnemy == null)
            {
                m_freeNodes[curFree++] = i;
            }
        }

        if (curFree < 1)
        {
            
            return null;
        }

        int nodeSelected = m_freeNodes[Random.Range(0, curFree)];
        return m_columns[nodeSelected].m_nodes[firstRow];
    }
}
