// 上帝之光
Shader "Unlit/test5"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _centerX("中心点x" ,Range(0,1) ) = 0.5
        _centerY("中心点y" ,Range(0,1) ) = 0.5
        _u_num_sample("纹理采样个数" , Int ) = 5
        //_u_density("纹理采样密度", Range(0,1) ) = 1.0
        _u_weight("纹理采样权重" , Range(0,1) ) = 1.0
        //_u_Decay("纹理采样衰减" , Range(0,1) ) = 0.98
        //_Exposure( "暴光", Range(0,1) ) = 0.8

    }
    SubShader
    {
        Tags { "Queue" = "Transparent"  }
        LOD 100 

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
            float _centerX;
            float _centerY;
            int _u_num_sample;
            float _u_weight;

            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {   
                float _u_density = 1.0;
                float _u_Decay = 0.98;
                float _Exposure = 0.8;

                float4 color = tex2D(_MainTex, i.uv);
                if (color.a <= 0.1){
                    discard;
                }

                // 中心点 float2 赋值时前面得加上 float2 (x,y) , 不然有问题
                float2 centerPos = float2(_centerX , _centerY);

                float2 newUV = float2(i.uv.x , i.uv.y);
                // 中心点到当前的纹理坐标的向量
                float2 deltaUv = newUV - centerPos ;
                // 把这个向量分成n个小向量
                deltaUv = deltaUv / ( (float)(_u_num_sample) * _u_density ) ;
                // 动态的衰减
                float dynDecay = 1.0;
 
                for(int i=0 ; i < _u_num_sample ; i++ )
                {
                    // 从当前像素 向中心点加向量
                    newUV = newUV - deltaUv;
                    // 获取 对应位置的颜色值
                    float4 newColor = tex2D(_MainTex, newUV);

                    newColor = newColor * _u_weight * dynDecay;
                    // 动态衰减* 衰减百分比
                    dynDecay = dynDecay * _u_Decay;

                    color = color + newColor;
                }

                color = color * _Exposure;
                return color;
            }
            ENDCG
        }
    }
}
