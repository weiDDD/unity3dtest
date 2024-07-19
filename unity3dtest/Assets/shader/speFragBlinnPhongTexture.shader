// 基于片段着色器的高光反射光照模型， blinnPhong 模型
// blinnPhong 模型， 基于 光照方向 和 视线方向 相加之后的归一化向量 与 法线 投影 来确定光照的强度

Shader "我的/speFragBlinnPhongTexture"
{
    Properties
    {
        _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
        _Specular ( "Specular", Color ) = (1, 1, 1, 1)
        _Gloss ("Gloss", Range(8.0, 256)) = 20
        _MainTex("纹理", 2D) = "White" {}
        _Color("颜色",Color) = (1,1,1,1)
    }
    SubShader
    {

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "Lighting.cginc"

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            fixed3 _Diffuse;
            fixed4 _Specular;
            fixed _Gloss;
            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST; // 纹理名 + _ST 表示纹理的 重复(xy) 和 偏移(zw)

            v2f vert (a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.worldNormal = UnityObjectToWorldNormal(v.normal); // mul(v.normal, (float3x3)unity_WorldToObject);

                o.worldPos = UnityObjectToWorldDir(v.vertex); // mul(unity_ObjectToWorld, v.vertex).xyz; 

                o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;  // TRANSFORM_TEX(v.texcoord, _MainTex); 效果等效

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 先获取纹理颜色
                fixed3 texColor = tex2D( _MainTex, i.uv).rgb * _Color.rgb;

                // 获取环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * texColor;
                // 世界法线
                fixed3 worldNormal = normalize( i.worldNormal );  // 归一化放在片段着色器中处理，因为片段着色器的法线是通过取顶点法线的相对值来算的。
                // 世界坐标下的 指向光源的向量
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos)); // normalize(_WorldSpaceLightPos0.xyz);

                // 计算漫反射
                fixed3 diffuse = _LightColor0.rgb *  texColor * saturate(dot( worldNormal, worldLightDir ));
                // 计算高光反射向量
                // fixed3 reflectDir = normalize( reflect(-worldLightDir, worldNormal) );
                // 顶点到视口的向量 （直接用相机的位置 - 顶点位置）
                fixed3 viewDir = UnityWorldSpaceViewDir(i.worldPos); // normalize(_WorldSpaceCameraPos.xyz - i.worldPos );

                fixed3 halfDir = normalize(worldLightDir + viewDir);

                // 计算高光着色
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(worldNormal , halfDir)), _Gloss);

                fixed3 color = ambient + diffuse + specular;

                return fixed4(color, 1.0);
            }
            ENDCG
        }
    }
}
