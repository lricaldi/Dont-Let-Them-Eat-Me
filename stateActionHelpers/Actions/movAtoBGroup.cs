using UnityEngine;
using System.Collections;


public struct movAtoBItem
{
	public Vector2 startPos;
	public Vector2 endPos;
	public float speed;
	public GameObject obj;
	public Vector2 direction;
	public float startTime;
	public bool done;


	public movAtoBItem(GameObject obj, Vector2 startPos, Vector2 endPos, float speed, float startTime)
	{
		this.startPos		= startPos;
		this.endPos			= endPos;
		this.speed			= speed;
		this.obj			= obj;
		this.direction		= (endPos - startPos).normalized;
		this.startTime		= startTime;
		this.done			= false;
	}
}

public class movAtoBGroup : StateActionBase
{

	private movAtoBItem[] movItems;
	
	private float m_timer;

	public override void reset()
	{
		base.reset();
		m_timer = 0;
		for (int i = 0; i<movItems.Length; i++)
		{
			movItems[i].done = false;
		}
	}

	public movAtoBGroup(movAtoBItem[] movItems)
	{
		this.movItems = movItems;

		reset();
	}

	public override void update()
	{
		if (m_timer == 0)
		{
			for (int i = 0; i < movItems.Length; i++)
			{
				movItems[i].obj.GetComponent<Transform>().position = movItems[i].startPos;
				movItems[i].obj.SetActive(true);
			}
		}
		m_timer += Time.deltaTime;
		bool allDone = true;
		if (!m_done)
		{
			for (int i = 0; i < movItems.Length; i++)
			{
				if (movItems[i].done) continue;
				allDone = false;

				if (m_timer >  movItems[i].startTime)
				{
					Vector2 curPos = movItems[i].obj.GetComponent<Transform>().position;
					curPos += movItems[i].direction * movItems[i].speed * Time.deltaTime;

					movItems[i].obj.GetComponent<Transform>().position = curPos;

					if ((movItems[i].endPos - movItems[i].startPos).sqrMagnitude < (curPos - movItems[i].startPos).sqrMagnitude)
					{
						movItems[i].done = true;
					}
				}
			}
		}

		m_done = allDone;
	}

	
}
