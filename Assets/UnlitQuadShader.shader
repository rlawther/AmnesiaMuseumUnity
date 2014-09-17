Shader "Custom/UnlitQuadShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", COLOR) = (1, 1, 1, 1)
	}
	SubShader {
        Tags {"Queue" = "Transparent"} 

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha 

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            uniform float4 _Color;

            float4 frag(v2f_img i) : COLOR {
            	float4 colour;
   				float alpha;
            	half4 basecol = tex2D (_MainTex, i.uv);
            	
				colour = basecol * _Color; 
                return colour;
            }
            ENDCG
        }
    }
	FallBack "Diffuse"
	
}

