using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class BoardChecker : MonoBehaviour
{
	private ProblemLoader _loader;
	private int sizeX;
	private int sizeY;
	bool[,] _cellStatuses;
	private bool[,] _answer;

	// Start is called before the first frame update
	async void Start()
	{
		CancellationToken ct = destroyCancellationToken;
		_loader = GetComponent<ProblemLoader>();
		await _loader.LoadProblem(ct);
		sizeX = _loader.BoardSizeX;
		sizeY = _loader.BoardSizeY;
		_cellStatuses = new bool[sizeX, sizeY];
		_answer = _loader.Answer;
		Debug.Log($"X:{sizeX} Y:{sizeY}");
	}

	public void OnCellChanged(int x, int y, bool isPainted)
	{
		// Debug.Log($"OnCellChanged:{x}:{y},{isPainted}");
		_cellStatuses[x, y] = isPainted;
		Util.printArray(_cellStatuses, nameof(_cellStatuses));

		if (CheckAnswer(_cellStatuses, _answer))
			Debug.Log("clear");
	}

	private bool CheckAnswer(bool[,] cells, bool[,] answer)
	{
		int width = cells.GetLength(0);
		int height = cells.GetLength(1);

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (cells[x, y] != answer[x, y])
				{
					return false;
				}
			}
		}
		return true;
	}
}
