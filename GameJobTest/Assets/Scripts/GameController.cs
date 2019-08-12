using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[Tooltip("Панель меню")]
	public GameObject Menu;

	// Индикатор паузы
	private bool isPaused = false;

	// Игровой цыкл
	private void Update()
	{
		//Считываем нажатия клавиш
		if(Input.GetKeyDown(KeyCode.Escape))
			OnPause();
	}


	/// <summary>
	/// Старт игры
	/// </summary>
	public void OnClickPlay()
	{
		SceneManager.LoadScene("Level1");
	}

	/// <summary>
	/// Выход из игры
	/// </summary>
	public void OnClickExit()
	{
		Application.Quit();
	}

	/// <summary>
	/// Продолжить игру
	/// </summary>
	public void OnClickContinue()
	{
		OnPause();
	}

	public void OnClickMainMenu()
	{
		SceneManager.LoadScene("Lobby");
	}

	/// <summary>
	/// Включить/выключить паузу
	/// </summary>
	void OnPause()
	{
		if(isPaused)
		{
			isPaused = false;
			Menu.SetActive(false);
			Time.timeScale = 1;
		}
		else
		{
			isPaused = true;
			Menu.SetActive(true);
			Time.timeScale = 0;
		}
	}
}
