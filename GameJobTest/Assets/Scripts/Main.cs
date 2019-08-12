using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static Main instance;

    void Awake()
    {
        //Загрузился дубликат при возврате в лобби. Удаляем дубликат.        
        if (instance != null)
        {
            if (this != null)
            {
                if (this.gameObject != null)
                    DestroyImmediate(this.gameObject);
            }

            return;
        }
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
	}
}
