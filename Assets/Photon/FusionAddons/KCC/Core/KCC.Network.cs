namespace Fusion.Addons.KCC
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	public class KCCNetworkContext
	{
		public KCC         KCC;
		public KCCData     Data;
		public KCCSettings Settings;
	}

	// This file contains implementation related to network synchronization and interpolation based on network buffers.
	public unsafe partial class KCC
	{
		// PRIVATE MEMBERS

		private KCCNetworkContext     _networkContext;
		private IKCCNetworkProperty[] _networkProperties;
		private KCCNetworkProperties  _defaultProperties;

		// PUBLIC METHODS

		/// <summary>
		/// Set current network replication mode of the KCC.
		/// </summary>
		/// <param name="replicationMode">Network replication mode.
	    /// <list type="bullet">
	    /// <item><description>Default - The KCC is replicated to all players.</description></item>
	    /// <item><description>Inclusive - The KCC is replicated only to players added to the replication list.</description></item>
	    /// <item><description>Exclusive - The KCC is replicated to all players except those added to the replication list.</description></item>
	    /// </list>
		/// </param>
		/// <param name="clearReplicationList">If true, replication list will be cleared.</param>
		public void SetReplicationMode(EKCCReplicationMode replicationMode, bool clearReplicationList)
		{
			_replicationMode = replicationMode;

			if (clearReplicationList == true)
			{
				ClearReplicationList();
			}
		}

		/// <summary>
		/// Returns whether the KCC is replicated over the network to a specific player.
		/// </summary>
		public bool IsReplicatedToPlayer(PlayerRef player)
		{
			return ReplicateTo(player);
		}

		/// <summary>
		/// Enable/Disable replication of the KCC to a specific player. Has no effect for EKCCReplicationMode.Default.
		/// </summary>
		public void SetReplicationToPlayer(PlayerRef player, bool replicate)
		{
			int id = player.RawEncoded;

			switch (_replicationMode)
			{
				case EKCCReplicationMode.Default:   { break; }
				case EKCCReplicationMode.Inclusive: { if (replicate == true) { _replicationList.Add(id);    } else { _replicationList.Remove(id); } break; }
				case EKCCReplicationMode.Exclusive: { if (replicate == true) { _replicationList.Remove(id); } else { _replicationList.Add(id);    } break; }
				default:
				{
					throw new ArgumentOutOfRangeException(_replicationMode.ToString());
				}
			}
		}

		/// <summary>
		/// Add a specific player to the replication list of the KCC.
		/// </summary>
		public void AddPlayerToReplicationList(PlayerRef player)
		{
			_replicationList.Add(player.RawEncoded);
		}

		/// <summary>
		/// Returns list of player refs in replication list.
		/// </summary>
		public void GetReplicationList(List<PlayerRef> players)
		{
			players.Clear();

			foreach (int id in _replicationList)
			{
				players.Add(PlayerRef.FromEncoded(id));
			}
		}

		/// <summary>
		/// Clear replication list with players.
		/// </summary>
		public void ClearReplicationList()
		{
			_replicationList.Clear();
		}

		/// <summary>
		/// Returns position stored in network buffer.
		/// </summary>
		public Vector3 GetNetworkBufferPosition()
		{
			fixed (int* ptr = &ReinterpretState<int>())
			{
				return ((NetworkTRSPData*)ptr)->Position + KCCNetworkUtility.ReadVector3(ptr + NetworkTRSPData.WORDS);
			}
		}

		/// <summary>
		/// Returns interpolated position based on data stored in network buffers.
		/// </summary>
		public bool GetInterpolatedNetworkBufferPosition(out Vector3 interpolatedPosition)
		{
			interpolatedPosition = default;

			RenderSource    defaultSource    = Object.RenderSource;
			RenderTimeframe defaultTimeframe = Object.RenderTimeframe;

			Object.RenderSource    = RenderSource.Interpolated;
			Object.RenderTimeframe = GetInterpolationTimeframe();

			bool buffersValid = TryGetSnapshotsBuffers(out NetworkBehaviourBuffer fromBuffer, out NetworkBehaviourBuffer toBuffer, out float alpha);

			Object.RenderSource    = defaultSource;
			Object.RenderTimeframe = defaultTimeframe;

			if (buffersValid == false)
				return false;

			KCCNetworkProperties.ReadPositions(fromBuffer, toBuffer, out Vector3 fromPosition, out Vector3 toPosition);

			interpolatedPosition = Vector3.Lerp(fromPosition, toPosition, alpha);

			return true;
		}

		// NetworkBehaviour INTERFACE

		protected override bool ReplicateTo(PlayerRef player)
		{
			switch (_replicationMode)
			{
				case EKCCReplicationMode.Default:   { return true; }
				case EKCCReplicationMode.Inclusive: { return _replicationList.Contains(player.RawEncoded) == true;  }
				case EKCCReplicationMode.Exclusive: { return _replicationList.Contains(player.RawEncoded) == false; }
				default:
				{
					throw new ArgumentOutOfRangeException(_replicationMode.ToString());
				}
			}
		}

		// PRIVATE METHODS

		private int GetNetworkDataWordCount()
		{
			InitializeNetworkProperties();

			int wordCount = 0;

			for (int i = 0, count = _networkProperties.Length; i < count; ++i)
			{
				IKCCNetworkProperty property = _networkProperties[i];
				wordCount += property.WordCount;
			}

			return wordCount;
		}

		private void ReadNetworkData()
		{
			_networkContext.Data = _fixedData;

			fixed (int* statePtr = &ReinterpretState<int>())
			{
				int* ptr = statePtr;

				for (int i = 0, count = _networkProperties.Length; i < count; ++i)
				{
					IKCCNetworkProperty property = _networkProperties[i];
					property.Read(ptr);
					ptr += property.WordCount;
				}
			}
		}

		private void WriteNetworkData()
		{
			_networkContext.Data = _fixedData;

			fixed (int* statePtr = &ReinterpretState<int>())
			{
				int* ptr = statePtr;

				for (int i = 0, count = _networkProperties.Length; i < count; ++i)
				{
					IKCCNetworkProperty property = _networkProperties[i];
					property.Write(ptr);
					ptr += property.WordCount;
				}
			}
		}

		private void InterpolateNetworkData(KCCData data, RenderSource renderSource, RenderTimeframe renderTimeframe, float customAlpha = -1.0f)
		{
			if (_isSpawned == false)
				return;

			RenderSource    defaultSource    = Object.RenderSource;
			RenderTimeframe defaultTimeframe = Object.RenderTimeframe;

			Object.RenderSource    = renderSource;
			Object.RenderTimeframe = renderTimeframe;

			bool buffersValid = TryGetSnapshotsBuffers(out NetworkBehaviourBuffer fromBuffer, out NetworkBehaviourBuffer toBuffer, out float alpha);

			if (customAlpha >= 0.0f && customAlpha <= 1.0f)
			{
				alpha = customAlpha;
			}

			Object.RenderSource    = defaultSource;
			Object.RenderTimeframe = defaultTimeframe;

			if (buffersValid == false)
				return;

			_networkContext.Data = data;

			KCCInterpolationInfo interpolationInfo = new KCCInterpolationInfo();
			interpolationInfo.FromBuffer = fromBuffer;
			interpolationInfo.ToBuffer   = toBuffer;
			interpolationInfo.Alpha      = alpha;

			int   ticks = interpolationInfo.ToBuffer.Tick - interpolationInfo.FromBuffer.Tick;
			float tick  = interpolationInfo.FromBuffer.Tick + interpolationInfo.Alpha * ticks;

			// Set general properties.

			data.Frame           = Time.frameCount;
			data.Tick            = Mathf.RoundToInt(tick);
			data.Alpha           = interpolationInfo.Alpha;
			data.DeltaTime       = Runner.DeltaTime;
			data.UpdateDeltaTime = data.DeltaTime;
			data.Time            = tick * data.DeltaTime;

			// Interpolate all networked properties.

			for (int i = 0, count = _networkProperties.Length; i < count; ++i)
			{
				IKCCNetworkProperty property = _networkProperties[i];
				property.Interpolate(interpolationInfo);
				interpolationInfo.Offset += property.WordCount;
			}

			// User interpolation and post-processing.

			InterpolateUserNetworkData(data, interpolationInfo);
		}

		private void RestoreHistoryData(KCCData historyData)
		{
			// Some values can be synchronized from user code.
			// We have to ensure these properties are in correct state with other properties.

			if (_fixedData.IsGrounded == true)
			{
				// Reset IsGrounded and WasGrounded to history state, otherwise using GroundNormal and other ground related properties leads to undefined behavior and NaN propagation.
				// This has effect only if IsGrounded and WasGrounded is synchronized over network.
				_fixedData.IsGrounded  = historyData.IsGrounded;
				_fixedData.WasGrounded = historyData.WasGrounded;
			}

			// User history data restoration.

			RestoreUserHistoryData(historyData);
		}

		private void InitializeNetworkProperties()
		{
			if (_networkContext != null)
				return;

			_networkContext = new KCCNetworkContext();
			_networkContext.KCC      = this;
			_networkContext.Settings = _settings;

			_defaultProperties = new KCCNetworkProperties(_networkContext);

			List<IKCCNetworkProperty> properties = new List<IKCCNetworkProperty>(32);
			properties.Add(_defaultProperties);

			InitializeUserNetworkProperties(_networkContext, properties);

			_networkProperties = properties.ToArray();
		}

		// PARTIAL METHODS

		partial void InitializeUserNetworkProperties(KCCNetworkContext networkContext, List<IKCCNetworkProperty> networkProperties);
		partial void InterpolateUserNetworkData(KCCData data, KCCInterpolationInfo interpolationInfo);
		partial void RestoreUserHistoryData(KCCData historyData);
	}
}
