using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
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
                output += array[i, j] + "\t"; // タブ区切りで表示する場合
                                              // output += array[i, j] + " "; // スペース区切りで表示する場合
            }

            output += "\n"; // 改行
        }

        Debug.Log(output);
    }
}
