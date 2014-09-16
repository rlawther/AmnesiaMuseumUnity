Shader "Custom/overlayFrag" {
	Properties {
		//_MainTex ("Base (RGB)", 2D) = "white" {}
		_Colour1 ("Colour1", COLOR) = (1, 1, 1, 1)
		_Layer1 ("Layer1 (RGBA)", 2D) = "black" {}
		_Colour2 ("Colour2", COLOR) = (1, 1, 1, 1)
		_Layer2 ("Layer2 (RGBA)", 2D) = "black" {}
		_Colour3 ("Colour3", COLOR) = (1, 1, 1, 1)
		_Layer3 ("Layer3 (RGBA)", 2D) = "black" {}
		_Colour4 ("Colour4", COLOR) = (1, 1, 1, 1)
		_Layer4 ("Layer4 (RGBA)", 2D) = "black" {}
		_Colour5 ("Colour5", COLOR) = (1, 1, 1, 1)
		_Layer5 ("Layer5 (RGBA)", 2D) = "black" {}
		_Colour6 ("Colour6", COLOR) = (1, 1, 1, 1)
		_Layer6 ("Layer6 (RGBA)", 2D) = "black" {}
	}
	SubShader {
        Tags {"Queue" = "Transparent"} 

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha 

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            //uniform sampler2D _MainTex;
            uniform sampler2D _Layer1;
            uniform sampler2D _Layer2;
            uniform sampler2D _Layer3;
            uniform sampler2D _Layer4;
            uniform sampler2D _Layer5;
            uniform sampler2D _Layer6;
            
            uniform float4 _Colour1;
            uniform float4 _Colour2;
            uniform float4 _Colour3;
            uniform float4 _Colour4;
            uniform float4 _Colour5;
            uniform float4 _Colour6;

            float4 frag(v2f_img i) : COLOR {
            	float4 colour;
   				float alpha;
            	//half4 basecol = tex2D (_MainTex, i.uv);
            	half4 layer1col = tex2D (_Layer1, i.uv) * _Colour1;
            	half4 layer2col = tex2D (_Layer2, i.uv) * _Colour2;
            	half4 layer3col = tex2D (_Layer3, i.uv) * _Colour3;
            	half4 layer4col = tex2D (_Layer4, i.uv) * _Colour4;
            	half4 layer5col = tex2D (_Layer5, i.uv) * _Colour5;
            	half4 layer6col = tex2D (_Layer6, i.uv) * _Colour6;
            	
				colour = (layer1col * layer1col.a) + 
				  (layer2col * layer2col.a) +
				  (layer3col * layer3col.a) +
				  (layer4col * layer4col.a) +
				  (layer5col * layer5col.a) +
				  (layer6col * layer6col.a);
                
                //colour += tex2D(_MainTex, i.uv);
	            alpha = 1.0 -
	            	layer1col.a - 
	            	layer3col.a - 
	            	layer4col.a - 
	            	layer5col.a - 
	            	layer6col.a - 
	            	layer2col.a;
				  
				 			
				// faster to not have this??
				if (alpha >= 0.95)
					discard;
					
				alpha = clamp(alpha, 0, 1);
				colour.a = 1.0 - alpha;

                return colour;
            }
            ENDCG
        }
    }
	FallBack "Diffuse"
}
