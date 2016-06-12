using UnityEngine;
using System.Collections.Generic;

namespace Anim {
	// OnDisable this will repair all first level transforms to their original state
	// Fixes an issue where the animator loses all notion of enabled/disabled GameObject states
	public class AnimatorRepair : MonoBehaviour {
		[Tooltip("Defaults to self")]
		[SerializeField] Transform target;
		
		Dictionary<Transform, bool> transRecord = new Dictionary<Transform, bool>();
		
		void Awake () {
			if (target == null) target = transform;
			
			// Cache and record the default state of the character          
			foreach (Transform child in target) {
				transRecord[child] = child.gameObject.activeInHierarchy;
			}
		}
		
		void OnDisable () {
			foreach (Transform child in target) {
				if (transRecord.ContainsKey(child)) {
					child.gameObject.SetActive(transRecord[child]);
				}
			}
		}
	}


}