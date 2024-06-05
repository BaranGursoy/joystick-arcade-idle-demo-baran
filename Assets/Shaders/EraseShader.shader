Shader "Custom/EraseShader"
{
Properties
{
_MainTex ("Base (RGB)", 2D) = "white" {}
_EraseTex ("Erase Texture", 2D) = "black" {}
_ErasePosition ("Erase Position", Vector) = (0,0,0,0)
_EraseRadius ("Erase Radius", Float) = 0.1
}
SubShader
{
Tags { "Queue"="Transparent" }
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

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        sampler2D _MainTex;
        sampler2D _EraseTex;
        float4 _ErasePosition;
        float _EraseRadius;

        v2f vert (appdata_t v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        fixed4 frag (v2f i) : SV_Target
        {
            fixed4 color = tex2D(_MainTex, i.uv);
            float dist = distance(i.uv, _ErasePosition.xy);
            if (dist < _EraseRadius)
            {
                color.a *= 0.0; 
            }
            return color;
        }
        ENDCG
    }
}
}