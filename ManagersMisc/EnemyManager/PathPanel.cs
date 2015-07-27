﻿using UnityEngine;
using System.Collections;

public class pathNode
{
    public Transform    m_nodeTransform;
    public bool         m_inUse;
    public pathNode     m_nextNode;
    public bool         m_fastMove;

    public pathNode(Transform node, pathNode nextNode, bool fastMove)
    {
        m_fastMove      = fastMove;
        m_nextNode      = nextNode;
        m_nodeTransform = node;
        m_inUse         = false;

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
                nodes[rowIndex]     = new pathNode(nodeTrans, nextNode, rowIndex >= FAST_MOVE_ROW);
            }

            m_columns[colIndex] = new pathColumn(nodes);
        }
    }

    public pathNode getFreeStartNode()
    {
        int curFree     = 0;
        int firstRow    = NUM_ROWS - 1;

        for (int i = 0; i < NUM_COLS; i++)
        {
            if (!m_columns[i].m_nodes[firstRow].m_inUse)
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