#ifndef PLANE_CLIPPING_INCLUDED
#define PLANE_CLIPPING_INCLUDED

//Plane clipping definitions

#if CLIP_PLANE || CLIP_TWO_PLANES || CLIP_SPHERE

	//PLANE_CLIPPING_ENABLED will be defined.
	//This makes it easier to check if this feature is available or not.
	#define PLANE_CLIPPING_ENABLED 1

	uniform float _SectionOffset = 0;
	uniform float3 _SectionPlane;
	uniform float3 _SectionPoint;
	#if CLIP_TWO_PLANES
		uniform float3 _SectionPlane2;
	#endif

	#if CLIP_SPHERE
		uniform float _Radius = 0;
	#endif

	//discard drawing of a point in the world if it is behind any one of the planes.
	void PlaneClip(float3 posWorld) {
	#if CLIP_SPHERE
		//if(distance((posWorld, _SectionPoint)>2) discard;
		//if (dot((posWorld - _SectionPoint),(posWorld - _SectionPoint))>_Radius*_Radius) discard;
		//if (all(abs(posWorld - _SectionPoint) > _Radius)) discard;

		//float3 range = abs(posWorld - _SectionPoint);
		//if (range.x < _Radius && range.y < _Radius && range.z < _Radius) discard;
		if (!all(1.0-saturate(abs(posWorld - _SectionPoint) - abs(_Radius)))) discard;

		#endif
	//	#endif
	//	#endif
	}

	//preprocessor macro that will produce an empty block if no clipping planes are used.
	#define PLANE_CLIP(posWorld) PlaneClip(posWorld);
	    
#else
	//empty definition
	#define PLANE_CLIP(s)
#endif

#endif // PLANE_CLIPPING_INCLUDED