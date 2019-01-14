// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CrossSection/Solid" {

Properties {
	_Color ("Main Color", Color) = (1,1,1,1)	
	_SectionColor ("Section Color", Color) = (1,0,0,1)
}

   SubShader {

		//  crossection pass (backfaces + fog)
	   Pass {
         Cull front // cull only front faces
         
         CGPROGRAM 
         
         #pragma vertex vert
         #pragma fragment frag
		 #pragma multi_compile_fog
		 #include "UnityCG.cginc"

 		 fixed4 _SectionColor;
		 #pragma multi_compile __ CLIP_PLANE CLIP_SECOND
		 #pragma multi_compile __ CLIP_SECOND
		 
		 #include "CGIncludes/section_clipping_CS.cginc"
  		 
         struct vertexInput {
            float4 vertex : POSITION;			
         };

		 struct fragmentInput{
                float4 pos : SV_POSITION;
				float3 wpos : TEXCOORD0;
                UNITY_FOG_COORDS(1)
         };

		 fragmentInput vert(vertexInput i){
                fragmentInput o;
                o.pos = UnityObjectToClipPos (i.vertex);
                o.wpos = mul (unity_ObjectToWorld, i.vertex).xyz;

                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
         }

         fixed4 frag(fragmentInput i) : SV_Target {
				if( _SectionColor.a <0.5f) discard;
				PLANE_CLIP(i.wpos);
                fixed4 color = _SectionColor;
                UNITY_APPLY_FOG(i.fogCoord, color); 
                return color;
         }

         ENDCG  
         
      }
     
     Pass {
         Cull BACK // cull only front faces
         
         CGPROGRAM 
         #include "UnityCG.cginc"
         #pragma vertex vert
         #pragma fragment frag
         fixed4 _Color;
         
 		 fixed4 _SectionColor;
		 #pragma multi_compile __ CLIP_PLANE CLIP_SECOND
		 
		 #include "CGIncludes/section_clipping_CS.cginc"
  		 
         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float3 wpos : TEXCOORD0;
         };
 
         vertexOutput vert(appdata_base input) {
            vertexOutput output;
            output.pos =  UnityObjectToClipPos(input.vertex);
            output.wpos = mul (unity_ObjectToWorld, input.vertex).xyz;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR  {
         	PLANE_CLIP(input.wpos);
         	return _Color;
         }
         ENDCG  
         
      }
   }
}