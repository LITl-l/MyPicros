using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	public static bool PaintMode = false;
	private Image _image;

	public int x;
	public int y;

	private bool isBlack;

	private BoardChecker _board;

	private void Start()
	{
		_image = this.GetComponent<Image>();
		_board = GameObject.FindWithTag("Board").GetComponent<BoardChecker>();
	}

	public void OnPointerDown()
	{
		Debug.Log($"Cell ({x}, {y}) is clicked");
		PaintMode = !PaintMode;
		ColorChange();
	}

	public void OnPointerUp()
	{
		PaintMode = false;
	}

	public void OnPointerEnter()
	{
		ColorChange();
	}

	private void ColorChange()
	{
		if (PaintMode == true)
		{
			_image.color = Color.black;
			isBlack = true;
		}
		else
		{
			_image.color = Color.white;
			isBlack = false;
		}

		_board.OnCellChanged(x, y, isBlack);
	}
}
