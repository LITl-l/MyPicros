using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabelGenerator : MonoBehaviour
{
	[SerializeField] private GameObject board;
	[SerializeField] private RectTransform _root;
	[SerializeField] private TextMeshProUGUI _vertical;
	[SerializeField] private TextMeshProUGUI _horizontal;

	private BoardCreator _creator;
	private int _verticalNum = 0;
	private int _horizontalNum = 0;
	// Start is called before the first frame update
	void Start()
	{
		_creator = board.GetComponent<BoardCreator>();
		_verticalNum = _creator.boardSize;
		_horizontalNum = _creator.boardSize;
		CloneLabels();
	}

	private void CloneLabels()
	{
		for (int y = 0; y < _verticalNum - 1; y++)
		{
			var newLabel = GameObject.Instantiate<TextMeshProUGUI>(_vertical, _root, true);
			newLabel.rectTransform.anchoredPosition = new Vector3(_vertical.rectTransform.anchoredPosition.x, -50 - 30 * y - 2 * y);
		}
		for (int x = 0; x < _horizontalNum - 1; x++)
		{
			var newLabel = GameObject.Instantiate<TextMeshProUGUI>(_horizontal, _root, true);
			newLabel.rectTransform.anchoredPosition = new Vector3(40 + 30 * x + 2 * x, _horizontal.rectTransform.anchoredPosition.y);
		}
	}
	// Update is called once per frame
	void Update()
	{

	}
}
