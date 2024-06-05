Shader "Custom/PaintDiffuseErase" 
{
    Properties 
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _PaintMap ("PaintMap", 2D) = "white" {} // texture to paint on
    }
    SubShader 
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200

        Pass 
        {
            ZTest LEqual
            Cull Off
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f 
            {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };

            struct appdata 
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };

            sampler2D _PaintMap;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv0 = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv1 = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw; // lightmap uvs
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 mainColor = tex2D(_MainTex, i.uv0); // main texture
                half4 paint = tex2D(_PaintMap, i.uv1);     // painted on texture
                
                // Multiply main color by paint color but set alpha to minimum
                mainColor.a = min(mainColor.a, paint.a);
                return mainColor;
            }
            ENDCG
        }
    }
}