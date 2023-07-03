using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	private enum PaintMode
	{
		None,
		Paint,
		Erase,
		Mark
	}
	private static PaintMode _mode;
	private Image _image;
	private Sprite _markSprite;
	private int x;
	private int y;
	public (int, int) GetCoord { get { return (x, y); } set { x = value.Item1; y = value.Item2; } }
	private bool isBlack = false;
	private bool isMarked = false;
	private BoardChecker _checker;

	private async void Start()
	{
		_image = GetComponent<Image>();
		_checker = GameObject.FindWithTag("Board").GetComponent<BoardChecker>();
		_mode = PaintMode.None;
		_markSprite = await Addressables.LoadAssetAsync<Sprite>("XMark").Task;
	}

	private void OnDestroy()
	{
		Addressables.Release(_markSprite);
	}

	public void OnPointerDown()
	{
		// Debug.Log($"Cell ({x}, {y}) is clicked");
		if (isBlack)
			_mode = PaintMode.Erase;
		else if (isMarked)
			if (Input.GetMouseButtonDown(1))
				_mode = PaintMode.Erase;
			else
			{
				if (Input.GetMouseButtonDown(1))
					_mode = PaintMode.Mark;
				_mode = PaintMode.Paint;
			}
		ColorChange();
	}

	public void OnPointerUp()
	{
		_mode = PaintMode.None;
	}

	public void OnPointerEnter()
	{
		if (_mode != PaintMode.None)
			ColorChange();
	}

	private void ColorChange()
	{
		if (_mode == PaintMode.Paint)
		{
			_image.color = Color.black;
			isBlack = true;
		}
		else if (_mode == PaintMode.Erase)
		{
			_image.sprite = null;
			_image.color = Color.white;
			isMarked = false;
			isBlack = false;
		}
		else if (_mode == PaintMode.Mark)
		{
			_image.sprite = _markSprite;
			isMarked = true;
		}

		_checker.OnCellChanged(x, y, isBlack);
	}
}
