// 多层纹理贴图 ， 并裁剪透明部分
Shader "Unlit/test3"
{
    Properties
    {
        // 变量名为 _MainTex ， sprite的纹理为直接放到这里面，并忽略材质球中设置的纹理，；如果不想关sprite中的纹理，这里的名字变一下
        _MainTex ("Texture 主", 2D) = "white" {}  
        _MainTex2 ("Texture2", 2D) = "white" {}
        
    }
    SubShader
    {   
        // Tags 标签 Tags { key1 = value1 key2 = value2 ... }  在SubShader中描述如何渲染
        // Queue 渲染队列，指定对象什么时候渲染
        Tags { "Queue" = "Transparent" }

        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;    // 定义两个 uv 坐标，
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                
            };

            sampler2D _MainTex;
            sampler2D _MainTex2;
            float4 _MainTex_ST;   // 纹理 _MainTex 的 tiling 和 offset 数据，xy表示tiling, zw 表示offset ; 必须声明一下，不然不能做 TRANSFORM_TEX 运算
            float4 _MainTex2_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);  相当于 o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv, _MainTex2);
                return o;
            }

            float4 mix(float4 a,float4 b,float c)
            {
                return a*(1.0-c) + b*c;
            }

            float4 frag (v2f i) : SV_Target
            {
                // tex2D 获取 纹理 _MainTex 在 uv 位置的颜色值透明度，rgba
                float4 col = tex2D(_MainTex, i.uv);
                float4 col2 = tex2D(_MainTex2, i.uv2);

                // 如果 col,col2 颜色 都 为0，则丢掉 ，必须得有这个才能实现 alpha为0的不可见
                if (col.a == 0 ){
                    discard; // 有这句话表示，丢掉这个片段
                }
                
                return mix(col , col2 , 0.5);
            }
            ENDCG
        }
    }
}
