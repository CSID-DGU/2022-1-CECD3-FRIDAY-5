using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputResponse : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    GameObject showBox;

	[SerializeField]
	KeyboardHandler keyboardHandler;
	public void OnSelect (BaseEventData eventData) 
	{
		keyboardHandler.init(this.gameObject);
		showBox.SetActive(true);
	}

	public void OnDeselect(BaseEventData eventData){
		showBox.SetActive(false);
	}
}
