using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Binarize : MonoBehaviour
{
	[SerializeField] private Texture2D _texture;
	[SerializeField] private float _threshold = 0.5f;
	[SerializeField] private RawImage _renderer;

	// Start is called before the first frame update
	void Start()
	{
		_renderer.texture = BinarizeImage(_texture, _threshold);
	}

	// Update is called once per frame
	private Texture2D BinarizeImage(Texture2D original, float threshold)
	{
		Texture2D binarized = new Texture2D(original.width, original.height, TextureFormat.RGBA32, false);
		binarized.SetPixels(original.GetPixels());

		Color[] pixels = binarized.GetPixels();
		for (int i = 0; i < pixels.Length; i++)
		{
			Color pixel = pixels[i];

			pixel.r = pixel.g = pixel.b = pixel.a >= threshold ? 1.0f : 0.0f;
			pixels[i] = pixel;
		}

		binarized.SetPixels(pixels);
		binarized.Apply();

		return binarized;
	}
}
