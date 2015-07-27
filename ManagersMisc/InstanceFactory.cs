﻿using UnityEngine;
using System.Collections;

public class InstanceFactory : MonoBehaviour 
{

    public static InstanceFactory instance = null;
    
    public EnemyView[]  m_enemyViews;
    public Enemy        m_enemy;
    public ItemView[]   m_itemViews;
 
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    private EnemyView getRandomEnemy()
    {
        return m_enemyViews[Random.Range(0, m_enemyViews.Length)];
    }

    private ItemView getRandomItem()
    {
        return m_itemViews[Random.Range(0, m_itemViews.Length)];
    }

    public Enemy getEnemy(Vector2 position, Quaternion rotQuat)
    {
        Enemy returnVal = ObjectPoolManager.CreatePooled(m_enemy.gameObject, position, rotQuat).GetComponent<Enemy>();
        return returnVal;
    }

    public EnemyView getRandomEnemyView(Vector2 position, Quaternion rotQuat)
    {
        EnemyView randEnemy = getRandomEnemy();
        EnemyView returnVal = ObjectPoolManager.CreatePooled(randEnemy.gameObject, position, rotQuat).GetComponent<EnemyView>();

        returnVal.GetComponent<SpriteRenderer>().sortingOrder       = 1;
        returnVal.GetComponent<SpriteRenderer>().sortingLayerName   = "Default";

        return returnVal;
    }


    public ItemView getRandomItemView(Vector2 position, Quaternion rotQuat)
    {

        ItemView randItemView   = getRandomItem();
        ItemView returnVal      = ObjectPoolManager.CreatePooled(randItemView.gameObject, position, rotQuat).GetComponent<ItemView>();

        returnVal.GetComponent<SpriteRenderer>().sortingOrder       = 1;
        returnVal.GetComponent<SpriteRenderer>().sortingLayerName   = "foreground";

        return returnVal;
    }


    public void freeEnemyView(EnemyView enemyView)
    {
        ObjectPoolManager.DestroyPooled(enemyView.gameObject);
    }

    public void freeEnemy(Enemy enemy)
    {
        ObjectPoolManager.DestroyPooled(enemy.gameObject);
    }

    public void freeItemView(ItemView itemView)
    {
        ObjectPoolManager.Destroy(itemView.gameObject);
    }
}
