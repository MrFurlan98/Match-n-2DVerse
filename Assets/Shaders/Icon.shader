Shader "Unlit/Icon"
{
	Properties
	{
		[PerRendererData]_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white"{}
		_DisolveValue("Disolve Value", Range(0, 1)) = 0
		_Opacity("Opacity", Range(0,1)) = 1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Opaque" }
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
	
			
			#include "UnityCG.cginc"



			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _NoiseTex;
			float _DisolveValue;
			float _Opacity;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				
				o.uv = TRANSFORM_TEX(v.uv , _MainTex);


				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
			   
				fixed4 nCol = tex2D(_NoiseTex, i.uv);

				clip(nCol.r - _DisolveValue);

				clip(col.a - 1);

				col.a = col.a - _Opacity;

				return col;
			}
			ENDCG
		}
	}
}
