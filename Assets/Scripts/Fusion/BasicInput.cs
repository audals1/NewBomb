using UnityEngine;
using Fusion;

public struct BasicInput : INetworkInput
{
	public Vector2 MoveDirection;
	public Vector2 LookRotationDelta;
	public NetworkBool Jump;
}
