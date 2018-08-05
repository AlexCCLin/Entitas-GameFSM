using UnityEngine;
using System.Collections;
using Entitas;

public class FSMDelayEventAdapter : FSMEventAdapter {
	FSMDebugService fsm_debug_service = FSMDebugService.singleton;


	public override void OnFSMProcessing(GameEntity entity) {
		fsm_debug_service.fsm_msg_report (this.gameObject.name + " : Delay Event : Receive OnFSMProcessing event");

		StartCoroutine (processing_delay (entity));
	}

	IEnumerator processing_delay(GameEntity e) {
		yield return new WaitForSeconds (1);

		//call-back
		if (e.hasFSMProcessingCnt) { //防止reset後, FSMProcessingCnt消失造成的error
			int cnt = e.fSMProcessingCnt.listener_busy_cnt;
			e.ReplaceFSMProcessingCnt (cnt - 1);
		}
	}


}
