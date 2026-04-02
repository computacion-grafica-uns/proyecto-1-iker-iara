Shader "Lighting/SingleColor"
{
	Properties{
		_MaterialColor ("Material Color",Color)=(0.25,0.5,0.5,1)
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			// declaramos que la función llamada "vert" definirá el shader de vértices
			#pragma vertex vert
			// declaramos que la función llamada "frag" definirá el shader de fragmentos
			#pragma fragment frag

			//datos de cada vértice
			struct appdata {
				float4 vertex : POSITION;
			};

			//datos que pasaremos del shader de vértices al de fragmentos
			struct v2f {
				float4 vertex : SV_POSITION;
			};

			float4 _MaterialColor;

			// shader de vértices
			v2f vert(appdata v)
			{
				v2f o;
				//veremos más adelante qué hace esta función
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			// shader de fragmentos
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 fragColor = _MaterialColor;
				return fragColor;
			}
			ENDCG
		}
	}
}