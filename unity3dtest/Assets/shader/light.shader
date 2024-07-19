// 多层纹理贴图 ， 并裁剪透明部分
Shader "我的/light"
{
    Properties
    {
        // 变量名为 _MainTex ， sprite的纹理为直接放到这里面，并忽略材质球中设置的纹理，；如果不想关sprite中的纹理，这里的名字变一下
        _MainTex ("Texture 主", 2D) = "white" {}  
        _light("亮度" , Float) = 1
        _color("颜色", Color) = (1,1,1,1)
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;   // 纹理 _MainTex 的 tiling 和 offset 数据，xy表示tiling, zw 表示offset ; 必须声明一下，不然不能做 TRANSFORM_TEX 运算

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);  相当于 o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }


            float _light;
            float4 _color;
            float4 frag (v2f i) : SV_Target
            {
                /*float tar_x = i.uv.x;
                float tar_y = i.uv.y + _light;
                if (tar_y > 1.0){
                    tar_y = tar_y - int(tar_y);
                }
                float2 offset = float2( tar_x , tar_y  );

                // tex2D 获取 纹理 _MainTex 在 uv 位置的颜色值透明度，rgba
                float4 col = tex2D(_MainTex, offset ); //i.uv);
                if(col.a <= 0.5 ){
                    discard;
                }
                float4 tar = float4( col.r , col.g  , col.b  , col.a );*/

                float4 col = tex2D(_MainTex, i.uv);
                float4 tar = float4( col.r , col.g  , col.b  , col.a );
                
                if (i.uv.x < 0.01 || i.uv.x > 0.99 || i.uv.y < 0.010 || i.uv.y > 0.99 ){
                    tar = float4( _color.r , _color.g , _color.b  ,_color.a );
                }
                else{
                    if(col.a <= 0.5 ){
                        discard;
                    }
                }

                return tar;
            }
            ENDCG
        }
    }
}
