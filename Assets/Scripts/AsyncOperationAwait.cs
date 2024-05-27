using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Extends AsyncOperation to make it properly work with `await`
public static class AsyncOperationAwait
{
	public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
	{
		var tcs = new TaskCompletionSource<object>();
		asyncOp.completed += obj => { tcs.SetResult(null); };
		return ((Task)tcs.Task).GetAwaiter();
	}
}
