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
	private BoardChecker _checker;
	private int _verticalNum = 0;
	private int _horizontalNum = 0;
	private bool[,] _answer;
	// Start is called before the first frame update
	void Start()
	{
		_creator = board.GetComponent<BoardCreator>();
		_checker = board.GetComponent<BoardChecker>();
		_verticalNum = _creator.boardSize;
		_horizontalNum = _creator.boardSize;
		CloneLabels();
	}

	private void CloneLabels()
	{
		_answer = _checker.answer;
		printArray<bool>(_answer);
		for (int y = 0; y < _verticalNum - 1; y++)
		{
			var newLabel = GameObject.Instantiate<TextMeshProUGUI>(_vertical, _root, true);
			newLabel.rectTransform.anchoredPosition = new Vector3(_vertical.rectTransform.anchoredPosition.x, -50 - 30 * y - 2 * y);
			string labelText = "";
			int count = 0;
			for (int x = 0; x < _horizontalNum - 1; x++)
			{
				bool state = _answer[x, y];
				if (state)
					count++;
				else
				{
					labelText += $"{count}";
					count = 0;
				}
			}
			newLabel.text = labelText;
		}
		for (int x = 0; x < _horizontalNum - 1; x++)
		{
			var newLabel = GameObject.Instantiate<TextMeshProUGUI>(_horizontal, _root, true);
			newLabel.rectTransform.anchoredPosition = new Vector3(40 + 30 * x + 2 * x, _horizontal.rectTransform.anchoredPosition.y);
			string labelText = "";
			int count = 0;
			for (int y = 0; y < _verticalNum - 1;)
			{
				bool state = _answer[x, y];
				if (state)
					count++;
				else
				{
					labelText += $"{count}";
					count = 0;
				}
			}
			newLabel.text = labelText;
		}
	}

	private void printArray<T>(T[,] array, string name = "")
	{
		string output = name + ":\n";
		int rows = array.GetLength(0);
		int columns = array.GetLength(1);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				output += array[i, j] + "\t"; // タブ区切りで表示する場合
																			// output += array[i, j] + " "; // スペース区切りで表示する場合
			}

			output += "\n"; // 改行
		}

		Debug.Log(output);
	}
}
