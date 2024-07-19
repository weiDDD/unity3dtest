/// 棋盘 shader 

Shader "Unlit/test2"
{
    Properties
    {
        _Color ("颜色", Color) = (1,1,1,1)
    }
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
                float2 uv : TEXCOORD;

            };

            struct v2f
            {
                float2 uv : TEXCOORD;
                float4 pos : SV_POSITION;
                float3 color : Color;
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = float3(1.0, 1.0, 0.0);
                return o;
            }
            // 自定义的函数
            bool checker(float2 uv)
            {
                float num = 6;  // 这里如果用int 就不行， 可能是 1/num 得到整数
                int u_index = floor( uv.x / (1 / num) );
                int v_index = floor( uv.y / ( 1/num ) );
                return (u_index + v_index) % 2 == 1;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if ( checker(i.uv) )
                {
                    return fixed4(1,0,0,1);
                }
                else
                {
                    return fixed4(i.color, 1);
                }
                
            }
            ENDCG
        }
    }
}
