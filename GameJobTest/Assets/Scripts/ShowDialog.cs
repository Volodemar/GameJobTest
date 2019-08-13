using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialog : MonoBehaviour
{
	[Tooltip("Первый мобик")]
	public GameObject Mob1;

	[Tooltip("Второй мобик")]
	public GameObject Mob2;

	[Tooltip("Растояние между мобами для показа диалога")]
	public float Dist = 20;

	[Tooltip("Частота проверки события")]
	public float Timer = 1;

	[Tooltip("Панель диалога")]
	public GameObject DialogPanel;

	private float timer = 0;
	private MobPathTraking mobPT;

	private void Awake()
	{
		mobPT = Mob1.GetComponent<MobPathTraking>();
	}

	private void Start()
	{
		timer = Timer;
	}

	private void Update()
    {
		if(timer < 0)
		{
			//Проверим растояние
			if(Vector3.Distance(Mob1.transform.position, Mob2.transform.position) < Dist)
			{
				if(mobPT.GetStatus() == "WaitAnim")
				{
					//Покажем диалог
					ShowDialogPanel(true);
				}
			}
			else
			{
				//Скроем диалог
				ShowDialogPanel(false);
				timer = Timer;
			}
		}
		else
		{
			timer = timer - Time.fixedDeltaTime;
		}
    }

	/// <summary>
	/// Показать скрыть диалог
	/// </summary>
	private void ShowDialogPanel(bool isShow)
	{
		DialogPanel.SetActive(isShow);
	}
}
