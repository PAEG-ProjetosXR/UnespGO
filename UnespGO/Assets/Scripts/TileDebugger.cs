using UnityEngine;

public class TileDebugger : MonoBehaviour
{
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (Transform child in transform)
		{
			Gizmos.DrawWireCube(child.position, new Vector3(1, 0, 1));
		}
	}
}
