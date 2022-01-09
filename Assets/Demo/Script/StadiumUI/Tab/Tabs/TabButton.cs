﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// </summary>
/// <remarks>
/// https://www.youtube.com/watch?v=211t6r12XPQ
/// </remarks>
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Selectable))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public TabGroup tabGroup;
	public Selectable selectable;
	public Image background;
	public UnityEvent onTabSelected;
	public UnityEvent onTabDeselected;

	public virtual void Select() 
	{
		Debug.Log($"[LOG]: TabButton[{name}].Select();");
		//selectable.Select();
		if (selectable is Toggle t) t.isOn = true; //!t.isOn;
		if (onTabSelected != null)
		{
			Debug.Log($"[LOG]: TabButton[{name}].onTabSelected.Invoke();");
			onTabSelected.Invoke();
		}
	}
	public virtual void Deselect() 
	{
		if (selectable is Toggle t) t.isOn = false; //!t.isOn;
		if (onTabDeselected != null) onTabDeselected.Invoke();
	}
	public virtual void Select(UnityAction action) 
	{
		Debug.Log($"[LOG]: TabButton[{name}].Select(UnityAction);");
		if (action != null)
		{
			Debug.Log($"[LOG]: TabButton[{name}].onTabSelected.AddListener(UnityAction);");
			onTabSelected.AddListener(action);
		}
	}
	public virtual void Deselect(UnityAction action) 
	{
		if (action != null) onTabDeselected.AddListener(action);
	}

	private void Awake()
	{
		//selectable = GetComponent(typeof(Selectable)) as Selectable;
		//selectable = GetComponents(typeof(Selectable))[0] as Selectable;
		//var components = GetComponents(typeof(MonoBehaviour));
		//foreach (var component in components)
		//	if (component != null && typeof(Selectable).IsAssignableFrom(component.GetType())) //typeof(Component)
		//	{
		//		selectable = (Selectable)component;
		//		break;
		//	}
		background = GetComponent<Image>();
	}

	private void Start()
	{
		tabGroup?.Subscribe(this);
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		tabGroup?.OnTabEnter(this);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		tabGroup?.OnTabExit(this);
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log($"[LOG]: TabButton[{name}].OnPointerClick(PointerEventData);");
		tabGroup?.OnTabSelected(this);
	}
}