// 描边

Shader "Unlit/test4"
{
    Properties
    {
        // 主纹理 PerRendererData 影响
         _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor("OutlineColor",Color) = (1,1,1,1)
        _CheckRange("CheckRange",Range(0,12)) = 0
        _LineWidth("LineWidth",Float) = 0.39
        _CheckAccuracy("CheckAccuracy",Range(0.1,0.99)) = 0.9
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent"        // 渲染队列，半透明 3000
            "IgnoreProjector"="True"     // 忽略投影器
            "RenderType"="Transparent"   // 渲染类型 半透明
            "PreviewType"="Plane"        // 预览类型 平面
            "CanUseSpriteAtlas"="True"   // 可以使用图集
        }

        Cull Off        // 关闭裁剪，正面反面都会渲染
        Lighting Off    // 关闭光照
        ZWrite Off      // 关闭深度缓存
        // 混合模式 ,  Blend SF DF , 分别设置源因子 SF (Sr,Sg,Sb,Sa) 和 目标因子 DF (Dr,Dg,Db,Da) ；源颜色指当前shader的片段色 (Rs,Gs,Bs,As) ，目标色指屏幕已有色 (Rd,Gd,Bd,Ad)
        // 最终色 = （ Rs*Sr+Rd*Dr , Gs*Sg+Gd*Dg , Bs*Sb+Bd*Db , As*Sa+Ad*Da ） 也就是rgba分量乘以各自的因子后相加
        // **因子
        // One  : (1,1,1,1)  表示完全的源颜色或目标色
        // Zero : (0,0,0,0)  舍弃源颜色或目标色
        // SrcColor : 源颜色 (Rs,Gs,Bs,As)
        // SrcAlpha : 源透明度 (As,As,As,As)
        // DstColor : 目标色 (Rd,Gd,Bd,Ad)
        // DstAlpha : 目标透明度 (Ad,Ad,Ad,Ad) 
        // OneMinusSrcColor  // 1-SrcColor
        // OneMinusSrcAlpha  // 1-SrcAlpha
        // OneMinusDstColor  // 1-DstColor
        // OneMinusDstAlpha  // 1-DstAlpha
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            /// xxx_TexelSize 纹理的像素尺寸，( x = 1/width , y = 1/height , z = width , w = height )
            float4 _MainTex_TexelSize;
            fixed4 _OutlineColor;
            float _CheckRange;
            float _LineWidth;
            float _CheckAccuracy;
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 texcoord  : TEXCOORD0;
            };//
            
            

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            
            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = tex2D (_MainTex, IN.texcoord);
                // c.a > 1/_LineWidth , 则为1 ， 仅当 _LineWidth 大于 1时才能起作用，因为 _LineWidth < 1 , 1/_LineWidth > 1 , alpha值不可能大于1
                float isOut = step(abs(1/_LineWidth),c.a);

                if(c.a <= 0.5 ){
                    discard;
                }

                if(isOut != 0)
                {
                    // 获取当前像素，1/width * _CheckRange , 1/height * _CheckRange ,也就是 _CheckRange 个像素，周围的点
                    fixed4 pixelUp = tex2D(_MainTex, IN.texcoord + fixed2(0, _MainTex_TexelSize.y*_CheckRange));  
                    fixed4 pixelDown = tex2D(_MainTex, IN.texcoord - fixed2(0, _MainTex_TexelSize.y*_CheckRange));  
                    fixed4 pixelRight = tex2D(_MainTex, IN.texcoord + fixed2(_MainTex_TexelSize.x*_CheckRange, 0));  
                    fixed4 pixelLeft = tex2D(_MainTex, IN.texcoord - fixed2(_MainTex_TexelSize.x*_CheckRange, 0));  
                    // step(a,x) 返回 x>=a ? 1 : 0    ;  smoothstep(min , max , x)  返回  x <= min 为0 ； x>=max 为 1 ；x >min && x< max 返回 0~1之间的比例数
                    // 如果上下左右有透明的点， 则 bOut 为 0 ，
                    float bOut = step((1-_CheckAccuracy),pixelUp.a*pixelDown.a*pixelRight.a*pixelLeft.a);
                    // lerp(x,y,s)  返回 x+s(y-x)
                    // 如果有透明点，bOut = 0 ， c = _OutColor
                    c = lerp(_OutlineColor,c,bOut);
                    return c;
                }
                return c;
                
            }
            ENDCG
        }
    }
}
