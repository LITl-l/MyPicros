using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Binarization : MonoBehaviour
{
	[SerializeField] private ComputeShader _computeShader;
	[SerializeField] private Texture2D _texture;
	[SerializeField] private RawImage _renderImage;

	private RenderTexture _result;

	struct ThreadSize
	{
		public uint x;
		public uint y;
		public uint z;

		public ThreadSize(uint x, uint y, uint z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		if (!SystemInfo.supportsComputeShaders)
		{
			Debug.LogError("ComputeShader is not supported");
			return;
		}

		//RenderTexture 初期化
		_result = new RenderTexture(_texture.width, _texture.height, 0, RenderTextureFormat.ARGB32);
		_result.enableRandomWrite = true;
		_result.Create();

		//ComputeShderのカーネルインデックスを取得
		var kernelIndex = _computeShader.FindKernel("Binarization");

		ThreadSize threadSize = new ThreadSize();
		_computeShader.GetKernelThreadGroupSizes(kernelIndex, out threadSize.x, out threadSize.y, out threadSize.z);

		//データをコピー
		_computeShader.SetTexture(kernelIndex, "Texture", _texture);
		_computeShader.SetTexture(kernelIndex, "Result", _result);

		//実行
		_computeShader.Dispatch(kernelIndex, _texture.width / (int)threadSize.x, _texture.height / (int)threadSize.y, (int)threadSize.z);

		//適応
		_renderImage.texture = _result;
	}

	private void OnDestroy()
	{
		_result = null;
	}
}
