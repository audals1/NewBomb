namespace Fusion.Addons.KCC
{
	using UnityEngine;

	public sealed class KCCShapeCastInfo
	{
		// PUBLIC MEMBERS

		public Vector3                 Position;
		public float                   Radius;
		public float                   Height;
		public float                   Extent;
		public Vector3                 Direction;
		public float                   MaxDistance;
		public LayerMask               LayerMask;
		public QueryTriggerInteraction TriggerInteraction;
		public KCCShapeCastHit[]       AllHits;
		public int                     AllHitCount;
		public KCCShapeCastHit[]       ColliderHits;
		public int                     ColliderHitCount;
		public KCCShapeCastHit[]       TriggerHits;
		public int                     TriggerHitCount;

		// CONSTRUCTORS

		public KCCShapeCastInfo() : this(KCC.CACHE_SIZE)
		{
		}

		public KCCShapeCastInfo(int maxHits)
		{
			AllHits      = new KCCShapeCastHit[maxHits];
			TriggerHits  = new KCCShapeCastHit[maxHits];
			ColliderHits = new KCCShapeCastHit[maxHits];

			for (int i = 0; i < maxHits; ++i)
			{
				AllHits[i] = new KCCShapeCastHit();
			}
		}

		// PUBLIC METHODS

		public void AddHit(RaycastHit raycastHit)
		{
			if (AllHitCount == AllHits.Length)
				return;

			KCCShapeCastHit hit = AllHits[AllHitCount];
			if (hit.Set(raycastHit) == true)
			{
				++AllHitCount;

				if (hit.IsTrigger == true)
				{
					TriggerHits[TriggerHitCount] = hit;
					++TriggerHitCount;
				}
				else
				{
					ColliderHits[ColliderHitCount] = hit;
					++ColliderHitCount;
				}
			}
		}

		public void Reset(bool deep)
		{
			Position           = default;
			Radius             = default;
			Height             = default;
			Extent             = default;
			Direction          = default;
			MaxDistance        = default;
			Radius             = default;
			LayerMask          = default;
			TriggerInteraction = QueryTriggerInteraction.Collide;
			AllHitCount        = default;
			ColliderHitCount   = default;
			TriggerHitCount    = default;

			if (deep == true)
			{
				for (int i = 0, count = AllHits.Length; i < count; ++i)
				{
					AllHits[i].Reset();
				}
			}
		}

		public void DumpHits(KCC kcc)
		{
			if (AllHitCount <= 0)
				return;

			kcc.Log($"ShapeCast Hits ({AllHitCount})");

			KCCShapeCastHit[] hits = AllHits;
			for (int i = 0, count = AllHitCount; i < count; ++i)
			{
				KCCShapeCastHit hit = AllHits[i];
				kcc.Log($"Collider: {hit.Collider.name}, Type: {hit.Type}, IsTrigger: {hit.IsTrigger}");
			}
		}
	}
}
