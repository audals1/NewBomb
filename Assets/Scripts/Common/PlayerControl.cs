using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using Fusion.Addons.KCC;


public class PlayerControl : NetworkBehaviour
{
	public KCC KCC;
	private NetworkMecanimAnimator _animator;

	private MyInput _accumulatedInput;

	[Networked] public float JumpCount { get; set; }

    private void Awake()
    {
		_animator = GetComponent<NetworkMecanimAnimator>();
    }

    private void Start()
    {
		KCC.OnCollisionEnter += OnCollisionEnterCallBack;
    }


    public override void Spawned()
	{
		if (Object.HasInputAuthority == true)
		{
			Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnPlayerInput);
		}
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		runner.GetComponent<NetworkEvents>().OnInput.RemoveListener(OnPlayerInput);
	}

	public override void FixedUpdateNetwork()
	{
		if (HasStateAuthority)
        {
			if (GetInput(out MyInput input) == true)
			{
				if (input.Jump == true && KCC.Data.IsGrounded == true)
				{
					KCC.Jump(Vector3.up * 6.0f);
					JumpCount++;
					Debug.Log(JumpCount);
					_animator.SetTrigger("IsJump", true);
				}
			}
		}
	}

	public override void Render()
	{
		Keyboard keyboard = Keyboard.current;
		if (keyboard != null)
		{
			if (keyboard.spaceKey.wasPressedThisFrame == true)
			{
				_accumulatedInput.Jump = true;
			}
		}
	}

	private void OnPlayerInput(NetworkRunner runner, NetworkInput networkInput)
	{
		// Accumulated input is consumed.
		networkInput.Set(_accumulatedInput);

		// Reset accumulated input to default.
		_accumulatedInput = default;
	}

	public partial interface IKCCProcessor
	{
		float GetPriority(KCC kcc) => default;       // Processors with higher priority are executed first.

		void OnEnter(KCC kcc, KCCData data) { } // Called when a KCC starts interacting with the processor.
		void OnExit(KCC kcc, KCCData data) { } // Called when a KCC stops interacting with the processor.
		void OnStay(KCC kcc, KCCData data) { } // Called when a KCC interacts with the processor and the movement is fully predicted or extrapolated.
		void OnInterpolate(KCC kcc, KCCData data) { } // Called when a KCC interacts with the processor and the movement is interpolated.
	}

	private void OnCollisionEnterCallBack(KCC kcc, KCCCollision collision)
    {
		if (collision.Collider.name == "Rope")
		{
			Debug.Log("로프 충돌");
		}
	}
}


