using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class BoardChecker : MonoBehaviour
{
	private BoardCreator _creator;
	private int sizeX;
	private int sizeY;
	bool[,] cellStatuses;
	public string problemAddress;
	private Texture2D _problem;
	private bool[,] _answer;
	public bool[,] answer { get { return _answer; } }

	// Start is called before the first frame update
	void Start()
	{
		_creator = this.GetComponent<BoardCreator>();
		LoadProblem();
		Debug.Log($"X:{sizeX} Y:{sizeY}");
	}

	private async void LoadProblem()
	{
		_problem = await Addressables.LoadAssetAsync<Texture2D>(problemAddress).Task;
		Color[] colors = _problem.GetPixels();
		sizeX = _problem.width;
		sizeY = _problem.height;
		// Debug.Log($"X:{sizeX} Y:{sizeY}");

		_answer = new bool[sizeX, sizeY];
		cellStatuses = new bool[sizeX, sizeY];
		for (int y = 0; y < sizeY; y++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				Color color = colors[y * sizeX + x];
				// Color pixel = _problem.GetPixel(x, y);
				bool isBlack = (color == Color.black);
				_answer[x, y] = isBlack;
			}
		}
		Rotate<bool>(_answer);
		ReverseHorizontal<bool>(_answer);
		printArray<bool>(_answer, nameof(answer));
		Addressables.Release(_problem);
	}

	public void OnCellChanged(int x, int y, bool isPainted)
	{
		// Debug.Log($"OnCellChanged:{x}:{y},{isPainted}");
		cellStatuses[x, y] = isPainted;
		printArray<bool>(cellStatuses, nameof(cellStatuses));

		if (CheckAnswer(cellStatuses, _answer))
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

	private void Rotate<T>(T[,] array)
	{
		int width = array.GetLength(0);
		int height = array.GetLength(1);

		T[,] copy = (T[,])array.Clone();

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				array[x, y] = copy[height - 1 - y, x];
			}
		}
	}

	private void ReverseHorizontal<T>(T[,] array)
	{
		int width = array.GetLength(0);
		int height = array.GetLength(1);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width / 2; x++)
			{
				T temp = array[x, y];
				array[x, y] = array[width - 1 - x, y];
				array[width - 1 - x, y] = temp;
			}
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

	// Update is called once per frame
	void Update()
	{

	}
}
