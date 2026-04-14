Shader "Unlit/TestGround"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR] _ColorA ("Color A", Color) = (0.1890176, 0.6430788, 2.357161, 0) 
        [HDR] _ColorB ("Color B", Color) = (0.1890176, 0.6430788, 2.357161, 0)

        _GlowIntensity ("Glow Intensity", Float) = 2
        _OutlineSize ("Outline Size", Float) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Transparent"
                "Queue"="Transparent" }
        
        Pass{
            
            Cull Off
            ZWrite Off
            Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.28318530718

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float4 _ColorA;
            float4 _ColorB;
            float _GlowIntensity;
            float _OutlineSize;

            struct MeshData
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv;
                return o;
            }


            fixed4 frag (Interpolators i) : SV_Target
            {
                float4 baseTexture = tex2D(_MainTex, i.uv);
                baseTexture.rgb *= baseTexture.a;
                float sideRemover = (abs(i.normal.y) > 0.9);
                return baseTexture * sideRemover;
                //return float4(combined.rgb, 1 - saturate(1 - length(combined.rgb))) ;
              
            }
            ENDCG
        }
    }
}