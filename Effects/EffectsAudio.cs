using UnityEngine;
using System.Collections;

public class EffectsAudio : MonoBehaviour {

	public enum EffectSound { ES_EnemyAttach, ES_Explosion02, BGS_Length };


	public AudioClip m_EnemyAttach;
    public AudioClip m_Expl02;

	public EffectsAudio()
	{
		loadClips();
	}
	

	public  void loadClips()
	{

		m_EnemyAttach = Resources.Load<AudioClip>("Audio/Effects/Enemies/common/AttachSound01");
        m_Expl02 = Resources.Load<AudioClip>("Audio/Effects/Misc/Explosion02");
	}


	public void playClip(int soundEnum)
	{
		switch ((EffectSound)soundEnum)
		{
			case EffectSound.ES_EnemyAttach:
				//SoundManager.instance.PlaySingleChTwo(m_EnemyAttach, false);
				break;
            case EffectSound.ES_Explosion02:
                //SoundManager.instance.PlaySingleChTwo(m_Expl02, false);
                break;
		}

	}
}
