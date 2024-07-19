Shader "我的/NormalTexTangentSpace"
{
    Properties
    {
        _Color ("颜色", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap("法线贴图", 2D) = "bump" {}
        _BumpScale ("法线贴图缩放", Float) = 1.0
        _Specular ("高光颜色", Color) = (1,1,1,1)
        _Gloss ("光泽度", Range(8.0, 256)) = 25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;  // SV表示系统变量 , SV_POSTION 表示系统变量:在齐次裁剪空间的位置
                float2 uv : TEXCOORD0;
                float3 lightDir : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                
            };

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;
            fixed _BumpScale;
            fixed4 _Specular;
            fixed _Gloss;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
