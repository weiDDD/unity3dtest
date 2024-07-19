
// 作用于顶点的漫反射
Shader "我的/diffuseVertex"
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
            fixed3 color : COLOR;
        };

        v2f vert(a2v v){
            v2f o;
            // 顶点数据从 本地坐标系转到 齐次裁剪坐标系
            o.pos = UnityObjectToClipPos(v.vertex);

            // 获取环境光
            fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
            // 获取世界坐标系下的发线向量
            fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject ));
            // 获取从这个物体 指向 世界坐标系下的 一个灯光的向量
            fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
            // 计算漫反射光
            fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

            o.color = ambient + diffuse;
            return o;
        }
        
        fixed4 frag(v2f i) : SV_Target {
            return fixed4(i.color, 1.0);
        }

        ENDCG
        }
    }
    FallBack "Diffuse"
}
