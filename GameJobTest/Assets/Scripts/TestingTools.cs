using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingTools : MonoBehaviour
{
	private void Awake()
	{
		// Если игру запустили не из лобби то предзагрузка
		GameObject check = GameObject.Find("DontDestroyOnLoadEveryWhere");
		if (check==null)
		{ 
			SceneManager.LoadScene("Lobby"); 
		}
	}
}
