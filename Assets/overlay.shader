Shader "Custom/overlay" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Layer1 ("Layer1 (RGBA)", 2D) = "white" {}
		_Layer2 ("Layer2 (RGBA)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _Layer1;
		sampler2D _Layer2;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float alpha = 1.0;
			half4 basecol = tex2D (_MainTex, IN.uv_MainTex);
			half4 layer1col = tex2D (_Layer1, IN.uv_MainTex);
			half4 layer2col = tex2D (_Layer2, IN.uv_MainTex);
			half4 colour;
			
			
			colour = (layer1col * layer1col.a);
			alpha -= layer1col.a;
			
			colour += (layer2col * layer2col.b);
			alpha -= layer2col.a;
			
			//if (alpha > 0)
			clamp(alpha, 0, 1);
			colour += (basecol * alpha);

			o.Albedo = colour.rgb;
			o.Alpha = basecol.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

