using UnityEngine;
using Fusion;

enum MyButtons
{
    Forward = 0,
    Backward = 1,
    Left = 2,
    Right = 3,
    Jump = 4,
}

public struct MyInput : INetworkInput
{
    public NetworkButtons buttons;
    public Vector3 aimDirection;
    public Vector3 MoveDirection;
    public NetworkBool Jump;
}
