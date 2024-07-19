Shader "我的/specularVertex"
{
    Properties
    {
        _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
        _Specular ( "Specular", Color ) = (1, 1, 1, 1)
        _Gloss ("Gloss", Range(8.0, 256)) = 20
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
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 color : COLOR;
            };

            fixed3 _Diffuse;
            fixed4 _Specular;
            fixed _Gloss;

            v2f vert (a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                // 获取环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                // 世界法线
                fixed3 worldNormal = normalize( mul(v.normal, (float3x3)unity_WorldToObject ) );
                // 世界坐标下的 指向光源的向量
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

                // 计算漫反射
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot( worldNormal, worldLightDir ));
                // 计算高光反射向量
                fixed3 reflectDir = normalize( reflect(-worldLightDir, worldNormal) );
                // 顶点到视口的向量 （直接用相机的位置 - 顶点位置）
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz );
                // 计算高光着色
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(reflectDir, viewDir)), _Gloss);

                o.color = ambient + diffuse + specular;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color, 1.0);
            }
            ENDCG
        }
    }
}
