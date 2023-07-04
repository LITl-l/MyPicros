using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
	/// <summary>
	/// ??????????(array)????????(degree)?
	/// 90ｰ?????????????Rotate()???????????????
	/// </summary>
	/// <param name="array"></param>
	/// <param name="degree"></param>
	/// <typeparam name="T"></typeparam>
	public static void RotateOptional<T>(T[,] array, int degree)
	{
		if (degree < 0)
			degree = 360 + degree;
		//90ｰ???????
		int normalizedDegree = degree % 360;
		// Debug.Log($"normalized{normalizedDegree}");
		//????
		int rotation = normalizedDegree / 90;
		// Debug.Log($"rotating{rotation}");

		for (int i = 0; i < rotation; i++)
		{
			Rotate(array);
		}
		// printArray(array, "Rotation");
	}

	/// <summary>
	/// ??????????(array)??????90ｰ????
	/// </summary>
	/// <param name="array"></param>
	/// <typeparam name="T"></typeparam>
	public static void Rotate<T>(T[,] array)
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

	public static void ReverseHorizontal<T>(T[,] array)
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

	public static void printArray<T>(T[,] array, string name = "")
	{
		string output = name + ":\n";
		int rows = array.GetLength(0);
		int columns = array.GetLength(1);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				output += array[i, j] + "\t"; // タブ区旙りで表示する晝插
																			// output += array[i, j] + " "; // スペ拏ス区旙りで表示する晝插
			}

			output += "\n"; // 改捏
		}

		Debug.Log(output);
	}
}
