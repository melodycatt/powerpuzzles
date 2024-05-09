Shader "Hidden/Pixelation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float2 BlockCount;
			float2 BlockSize;

			fixed4 frag (v2f_img i) : SV_Target
			{
				//uv = (0..1)
				float2 blockPos = floor(i.uv * BlockCount);
				float2 blockCenter = blockPos * BlockSize;
				float4 tex = tex2D(_MainTex, blockCenter);
				//float2 pointUV = (floor(blockCenter * _MainTex_TexelSize.zw) + 0.5) * _MainTex_TexelSize.xy;
				//fixed4 tex = tex2Dlod(_MainTex, float4(pointUV, 0, 0));
				return tex;
			}
			ENDCG
		}
	}
}
