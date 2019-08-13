using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHi : MonoBehaviour
{
	[Tooltip("Другой моб")]
	public GameObject OtherMob;

	[Tooltip("Дистанция до другого моба")]
	public float Dist = 15f;

	[Tooltip("Пауза между событиями")]
	public float TimerActive = 10.0f;

	//Требуется скрипт MobPathTraking
	private MobPathTraking mobPathTraking;
	private float timerActive = 10.0f;

	private void Awake()
	{
		timerActive = TimerActive;
		mobPathTraking	= this.GetComponent<MobPathTraking>();
	}

	private void Update()
    {
		timerActive = timerActive - Time.fixedDeltaTime;

		if(timerActive < 0)
		{
			//Проверяем растояние до другого моба и время между событиями
			if(Vector3.Distance(this.transform.position, OtherMob.transform.position) < Dist)
			{
				if(mobPathTraking.GetStatus() == "GoToPoint")
				{
					//Повернемся к цели
					this.transform.LookAt(OtherMob.transform.position);

					//Запустим анимацию сальто
					mobPathTraking.OnHi();

					timerActive = TimerActive;
				}
			}
		}
    }
}
