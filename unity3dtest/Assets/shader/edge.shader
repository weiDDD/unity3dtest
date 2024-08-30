Shader "我的/edge" {
    Properties {
        _uMainTex ("Default Texture", 2D) = "" {}
        _pointNum("顶点数", Float) = 4.0
        _angleOffset("角度偏移", Range(0,360)) = 0
        _pointValue1("顶点值1", Range(0, 0.5)) = 0.5
        _pointValue2("顶点值2", Range(0, 0.5)) = 0.5
        _pointValue3("顶点值3", Range(0, 0.5)) = 0.5
        _pointValue4("顶点值4", Range(0, 0.5)) = 0.5
        _pointValue5("顶点值5", Range(0, 0.5)) = 0.5
        _pointValue6("顶点值6", Range(0, 0.5)) = 0.5
    }

    SubShader {
        Tags { "Queue"="Geometry" }

        Pass {
            
            Blend One OneMinusSrcAlpha
            Cull Off
            ZTest Always
            ZWrite Off

            CGPROGRAM

            uniform sampler2D _uMainTex;
            uniform float _pointNum;
            uniform float _angleOffset;
            uniform float _pointValue1;
            uniform float _pointValue2;
            uniform float _pointValue3;
            uniform float _pointValue4;
            uniform float _pointValue5;
            uniform float _pointValue6;

            const float PI = 3.1415926;
            
            struct a2v {
                float4 pos : POSITION;
                float2 tex : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 tex : TEXCOORD0;
                float4 color : TEXCOORD1;

            };

            v2f vert(a2v i) {
                v2f o;
                o.pos = mul(uMVPMatrix, i.pos);
                o.tex = i.tex;
                o.color = i.color;
                return o;
            }

            float4 frag(v2f i) : SV_TARGET {
                float _points[6] = {
                    _pointValue1, _pointValue2, _pointValue3, _pointValue4, _pointValue5, _pointValue6
                };

                //float4 color = tex2D(_uMainTex, i.tex) * i.color;
                float2 center = float2(0.5, 0.5);
                // 每个角度的范围
                float everAngle = 360.0 / (float)_pointNum;
                // 中心点到当前纹理坐标的向量
                float2 nowVector = float2(i.tex.x - center.x, i.tex.y - center.y);
                float vecterLength = length(nowVector);
                // 当前角度
                float nowAngle = 0.0;
                if(vecterLength == 0.0){
                    nowAngle = 0.0;
                }
                else{
                    if (nowVector.y >= 0 ){
                        nowAngle = acos(nowVector.x / vecterLength);
                    }
                    else{
                        nowAngle = 6.2831852 - acos(nowVector.x / vecterLength);  // 6.2831852
                    }
                }
                nowAngle = degrees(nowAngle); // nowAngle / PI * 180.0;

                // 确定应该在哪个三角形内
                int edgeIdx = floor((nowAngle - _angleOffset) / everAngle);

                // 找到第三边
                float startPointLen = edgeIdx > (_pointNum - 1) ? _points[0] : _points[edgeIdx];
                float endPointLen = (edgeIdx + 1) > _pointNum - 1 ? _points[0] : _points[edgeIdx + 1];

                float2 startPoint = float2( center.x + startPointLen * cos( _angleOffset + (float)edgeIdx * everAngle ),
                                                center.y + startPointLen * sin( _angleOffset + (float)edgeIdx * everAngle ) );
                float2 endPoint = float2( center.x + endPointLen * cos( _angleOffset + (float)edgeIdx * everAngle ),
                                                 center.y + endPointLen * sin( _angleOffset + (float)edgeIdx * everAngle ) );
                
                // 求交点 参数化-参数化 
                float2 pointDes1 = float2(i.tex.x - endPoint.x, i.tex.y - endPoint.y);
                float2 pointDes2 = float2(startPoint.x - endPoint.x, startPoint.y - endPoint.y);

                float2 pointChen = float2(-(center.y - i.tex.y),  center.x - i.tex.x); 
                // t * aPoint + (1 - t) * bPoint 的 t
                float arg = dot(pointDes1, pointChen) / dot(pointDes2, pointChen);
                // 交点
                float2 jiaoPoint = float2( arg * center.x + (1.0 - arg) * i.tex.x, arg * center.y + (1.0 - arg) * i.tex.y );
                // 交点向量
                float2 jiaoVector = float2(jiaoPoint.x - center.x, jiaoPoint.y - center.y);
                float jiaoLen = length(jiaoVector);

                if(jiaoLen > vecterLength){ 
                    discard;
                }
                //if(everAngle > 91.0){ 
                //    discard;
                //}

                return float4(everAngle/360., 0.0 , 0.0, 1.0);
            }

            ENDCG
        }
    }
}