using UnityEngine;

public class InputStateBase
{
	public static bool IsPersistent;
	
   
	protected InputStateBase() { }
	
	public virtual void OnEnter()
	{
			
	}

	public virtual void Execute() { }

	public virtual void FixedExecute() { }

	public virtual void OnExit() { }

	public static void print(object message) => Debug.Log(message);
}

public sealed class DisabledState : InputStateBase
{
	public override void OnEnter()
	{
		print("In disabled state");
	}
}

public sealed class IdleState : InputStateBase
{
	public override void OnEnter()
	{
		print("In idle");
	}
}
