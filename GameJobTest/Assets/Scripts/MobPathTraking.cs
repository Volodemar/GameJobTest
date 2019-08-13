using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Движение моба по пути
/// </summary>
public class MobPathTraking : MonoBehaviour
{
	[Tooltip("Родительский объект содержащий точки пути")]
	public GameObject Path;

	[Tooltip("Поворачиваться остановившись")]
	public bool isStopPoints = false;

	private Animator Animator;
	private List<Vector3> PathPoints;
	private int CurrentPoint = 0; 
	private string Status = "";

	private CharacterController CharController;

	private void Awake()
	{
		//Двигатель персонажа
		CharController	= this.GetComponent<CharacterController>();
		PathPoints		= new List<Vector3>();
		Animator		= this.GetComponent<Animator>(); 
	}

	private void Start()
	{
		// Получим все точки пути
		if(Path != null)
		{
			float dist = 1000; 
			foreach(Transform child in Path.transform)
			{
				PathPoints.Add(child.transform.position);

				// Устанавливаем цель движения к ближайшей точке
				if(dist > Vector3.Distance(this.transform.position, child.transform.position))
				{
					dist = Vector3.Distance(this.transform.position, child.transform.position);
					CurrentPoint = PathPoints.Count-1;
				}
			}
		}

		SetStatus("GoToPoint");
	}

	private void Update()
	{
		// Двигаемся от точки к точке
		OnMoved();
	}

	// Событие движение по пути
	private void OnMoved()
	{
		AnimatorStateInfo asi = Animator.GetCurrentAnimatorStateInfo(0);

		if(Status == "GoToPoint")
		{
			if (!asi.IsName("Move"))
				Animator.SetTrigger("Move");

			// Поворачиваемся в сторону цели на ходу
			Vector3 direction	= GetCurrentPoint(true) - transform.position;
			transform.rotation	= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3f * Time.deltaTime);

			//Если пришли на точку меняем статус и выходим
			if(Vector3.Distance(this.transform.position, GetCurrentPoint(true)) < 0.5)
			{
				SetStatus("InPoint");
			}
			else
			{
				Vector3 forward = transform.TransformDirection(Vector3.forward);
				CharController.SimpleMove(this.transform.forward * 5.5f);
			}
		}

		if(Status == "InPoint")
		{
			if (!asi.IsName("Idle"))
				Animator.SetTrigger("Idle");

			//Если цель достигнута сменим цель
			SetNextPoint();
			if(isStopPoints)
			{
				SetStatus("TurnToPoint");
			}
			else
			{
				SetStatus("GoToPoint");
			}
		}

		if(Status == "TurnToPoint")
		{
			if (!asi.IsName("Idle"))
				Animator.SetTrigger("Idle");

			Quaternion lastRotation = transform.rotation;

			//Поворачиваемся в сторону цели
			Vector3 direction	= GetCurrentPoint(true) - transform.position;
			transform.rotation	= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3f * Time.deltaTime);

			//Проверяем видим ли цель - перестали поворачиваться
			if(lastRotation == transform.rotation)
			{
				SetStatus("GoToPoint");
			}
		}

		// Нажата кнопка танцевать
		if(Status == "Dance")
		{
			if (!asi.IsName("Dance"))
				Animator.SetTrigger("Dance");

			if (asi.IsName("Dance"))
				SetStatus("WaitAnim");
		}

		// Выполнить сальто
		if(Status == "SaltoMortale")
		{
			if (!asi.IsName("SaltoMortale"))
				Animator.SetTrigger("SaltoMortale");

			if (asi.IsName("SaltoMortale"))
				SetStatus("WaitAnim");
		}

		// Здрасте
		if(Status == "Hi")
		{
			if (!asi.IsName("Hi"))
				Animator.SetTrigger("Hi");

			if (asi.IsName("Hi"))
				SetStatus("WaitAnim");
		}

		// Ожидание когда анимация закончится и мы перейдем в Idle
		if(Status == "WaitAnim")
		{
			if(asi.IsName("Idle"))
				SetStatus("TurnToPoint");
		}
	}

	/// <summary>
	/// Событие здрасте
	/// </summary>
	public void OnHi()
	{
		if(Status != "Hi")
		{
			SetStatus("Hi");
		}
	}

	/// <summary>
	/// Событие выполнить сальто
	/// </summary>
	public void OnSaltoMortale()
	{
		if(Status != "SaltoMortale")
		{
			SetStatus("SaltoMortale");
		}
	}

	/// <summary>
	/// Событие нажатие кнопки танцуй/не такнцуй
	/// </summary>
	public void OnClickDance()
	{
		if(Status != "Dance")
		{
			SetStatus("Dance");
		}
		else
		{
			SetStatus("TurnToPoint");
		}
	}

	/// <summary>
	/// Вернуть текущий статус
	/// </summary>
	public string GetStatus()
	{
		return Status;
	}

	/// <summary>
	/// Установить новый статус
	/// </summary>
	private void SetStatus(string StName)
	{
		//if (StName == "GoToPoint")
		//	Animator.SetTrigger("Move");
		//if (StName == "TurnToPoint")
		//	Animator.SetTrigger("Idle");
		//if (StName == "InPoint")
		//	Animator.SetTrigger("Idle");

		Status = StName;
	}

	//private void SetAnimation()
	//{
	//	AnimatorStateInfo asi = Animator.GetCurrentAnimatorStateInfo(0);
	//	asi.
	//}

	/// <summary>
	/// Возвращает координату текущей точки
	/// </summary>
	/// <param name="isFixY">Зафиксировать текущую высоту</param>
	private Vector3 GetCurrentPoint(bool isFixY = true)
	{
		if(isFixY)
			return new Vector3(PathPoints[CurrentPoint].x, this.transform.position.y, PathPoints[CurrentPoint].z);
		else
			return PathPoints[CurrentPoint];
	}

	/// <summary>
	/// Устанавливаем следующую точку пути как текущую
	/// </summary>
	private void SetNextPoint()
	{
		if(PathPoints.Count > CurrentPoint+1)
			CurrentPoint++;
		else
			CurrentPoint=0;
	}
}
