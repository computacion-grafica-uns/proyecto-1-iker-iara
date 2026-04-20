Shader "ShaderSombra"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color: COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color: COLOR;
            };

            uniform float4x4 _ModelMatrix;
            uniform float4x4 _ViewMatrix;
            uniform float4x4 _ProjectionMatrix;

            v2f vert(appdata vt)
            {
                v2f o;

                float4 worldPos = mul(_ModelMatrix, vt.vertex);
                float3 lightWorldPos = float3(0.5, 2.4, 0.7);

                float a = lightWorldPos.x;
                float b = lightWorldPos.y;
                float c = lightWorldPos.z;
                float u = worldPos.x;
                float v = worldPos.y;
                float w = worldPos.z;

                worldPos.y = 0.005;
                worldPos.x = (a*v - b*u) / (v - b);
                worldPos.z = (c*v - b*w) / (v - b);
                
                o.vertex = mul(mul(_ProjectionMatrix, _ViewMatrix), worldPos);
                o.color = vt.color;

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return(i.color);
            }
            ENDCG
        }
    }
}
