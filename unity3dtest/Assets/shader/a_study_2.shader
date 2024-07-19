Shader "我的/a_study_2"
{
    Properties
    {
        _Color("颜色" , Color) = (1,1,1,1)
    }
    SubShader
    {
        //***** Tags 标签 Tags { key1 = value1 key2 = value2 ... }  在SubShader中描述如何渲染
        //**参数介绍：
        // 1. Queue 渲染队列，指定对象什么时候渲染;
        //     Background  值为1000 ， 此队列的对象最先进行渲染
        //     Geometry    值为2000 ，通常用于不透明对象，多用于场景的物件和角色等
        //     AlphaTest   值为2450.要么完全透明，要么不透明，多用于利用贴图来实现边缘透明的效果   
        //     Transparent 值为3000，常用于半透明对象
        //     Overlay     值为4000，用于叠加效果，最后渲染的东西放到这里。
        //  PS： 可以写 "Queue" = "AlphaTest+1" 来表示值为2451的渲染队列
        // 2.RenderType 渲染类型
        // 3.DisableBatching  是否禁用批处理： False , True , LODFading 仅当LOD激活时禁用批处理
        // 4.ForceNoShadowCasting 是否强制关闭投射阴影 ： True ， False
        // 5.IgnoreProjector  是否忽略投射器的影响：True , False
        // 6.CanUseSpriteAtlas  是否可以用精灵打包图集：True , False
        // 7.PreviewType  决定材质面板的预览窗口如何显示模型： Plane 平面预览， Skybox 天空盒预览

        Tags { "Queue" = "AlphaTest" "PreviewType" = "Plane" }

        Cull Off      // 在渲染时默认只有朝向摄像机的面才会被渲染， 这个参数可以描述想渲染哪个面， 取值：Off 前后面都不剔除 , Back 剔除背对摄像机的面 , Front 剔除朝向摄像机的面
        LOD 100       // LOD 表示 level of detail 细节程度，可以定义多组SubShader 然后写上不同的LOD值，然后在代码里面用 Shader.globalMaximumLOD = 100 激活LOD=100的SubShader
        Lighting Off  // 关闭光照
        ZWrite Off    // 关闭深度缓存
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
        Blend One OneMinusSrcAlpha  

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;

            // 应用程序阶段 传入的数据的结构体
            struct appdata
            {
                float4 vertex:POSITION;    // 顶点坐标 , xyzw
                float4 tangent : TANGENT;  // 切线
                float3 normal : NORMAL;    // 法线 
                float2 uv:TEXCOORD0;       // uv纹理坐标 取整范围为 0~1 ， 默认四个顶点取值为 {x=0,y=0} , {x=0,y=1} , {x=1,y=0} , {x=1,y=1}
                float2 uv1:TEXCOORD1;      // uv纹理坐标 
                float2 uv2:TEXCOORD2;      // uv纹理坐标 
                float2 uv3:TEXCOORD3;      // uv纹理坐标 
                fixed4 color : COLOR;      // 顶点色
            };

            // 顶点着色器 传递给 片元着色器的结构体
            struct v2f
            {
                float4 pos:SV_POSITION;     // SV_POSITION 表示顶点着色器输出的转换到 屏幕裁剪空间的顶点位置
                float2 uv:TEXCOORD0;      
                float2 uv1:TEXCOORD1;      // uv纹理坐标 
                float2 uv2:TEXCOORD2;      // uv纹理坐标 
                float2 uv3:TEXCOORD3;      // uv纹理坐标 
                fixed4 color : COLOR;      // 顶点色
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos( v.vertex );
                // 顶点着色器对每个
                o.uv = v.uv;
                return o;
            }

            struct f_ret
            {
                fixed4 color : SV_TARGET ;     /// SV_TARGET 表示渲染的最终颜色值
                float depth : SV_Depth ;       /// 标识表示，渲染的深度值
            };

            f_ret frag( v2f i ) 
            {
                //return _Color;
                f_ret ret;
                ret.color = fixed4( i.uv, 1 , 0.5 ) ;
                //ret.color1 = fixed4( i.uv, 0 , 0.5 ) ;
                ret.depth = 100 ;   // 
                return ret;
            }

            ENDCG
        }
    }
}
