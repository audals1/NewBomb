using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using Fusion.Addons.KCC;


	/// <summary>
	/// The minimalistic player implementation. Shows essential code to control KCC.
	/// </summary>
	[DefaultExecutionOrder(-5)]
	public sealed class BasicPlayer : NetworkBehaviour
	{
		public KCC       KCC;

		private MyInput _accumulatedInput;

		public override void Spawned()
		{
			if (Object.HasInputAuthority == true)
			{
				// Register input polling for local player.
				Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnPlayerInput);
			}
		}

		public override void Despawned(NetworkRunner runner, bool hasState)
		{
			// Unregister input polling.
			runner.GetComponent<NetworkEvents>().OnInput.RemoveListener(OnPlayerInput);
		}

		public override void FixedUpdateNetwork()
		{
			if (GetInput(out MyInput input) == true)
			{

				if (input.Jump == true && KCC.Data.IsGrounded == true)
				{
					// Set world space jump vector. This value is processed later when KCC executes its FixedUpdateNetwork().
					KCC.Jump(Vector3.up * 6.0f);
				}
			}
		}

		public override void Render()
		{
			Keyboard keyboard = Keyboard.current;
			if (keyboard != null)
			{
				Vector2 moveDirection = default;

				if (keyboard.wKey.isPressed == true) { moveDirection += Vector2.up;    }
				if (keyboard.sKey.isPressed == true) { moveDirection += Vector2.down;  }
				if (keyboard.aKey.isPressed == true) { moveDirection += Vector2.left;  }
				if (keyboard.dKey.isPressed == true) { moveDirection += Vector2.right; }

				_accumulatedInput.MoveDirection = moveDirection.normalized;

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
	}
