
// 作用于顶点的漫反射
Shader "我的/diffuseFragment"
{
    Properties
    {
        _Diffuse ("Diffuse", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "LightMode"="ForwardBase" }

        Pass{
        CGPROGRAM
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #pragma vertex vert
        #pragma fragment frag

        #include "Lighting.cginc"

        fixed4 _Diffuse;

        struct a2v {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };

        struct v2f {
            float4 pos : SV_POSITION;
            float3 worldNormal : TEXCOORD0;
        };

        v2f vert(a2v v){
            v2f o;
            // 顶点数据从 本地坐标系转到 齐次裁剪坐标系
            o.pos = UnityObjectToClipPos(v.vertex);
            
            // 获取世界坐标系下的发线向量
            o.worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject ));

            return o;
        }
        
        fixed4 frag(v2f i) : SV_Target {
            fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

            fixed3 worldNormal = normalize(i.worldNormal);

            fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

            fixed3  diffuse = _Diffuse.rgb * _LightColor0.rgb * saturate( dot(worldNormal, worldLightDir) );

            fixed3 color = ambient + diffuse;

            return fixed4(color, 1.0);
        }

        ENDCG
        }
    }
    FallBack "Diffuse"
}
