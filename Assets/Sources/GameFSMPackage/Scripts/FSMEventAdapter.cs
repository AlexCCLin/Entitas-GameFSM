using UnityEngine;
using System.Collections;
using Entitas;

public class FSMEventAdapter : MonoBehaviour, IFSMProcessingListener {
	FSMDebugService fsm_debug_service = FSMDebugService.singleton;


	public virtual void OnFSMProcessing(GameEntity entity) {
		fsm_debug_service.fsm_msg_report (this.gameObject.name + " : Receive OnFSMProcessing event");

		//call-back
		int cnt = entity.fSMProcessingCnt.listener_busy_cnt;
		entity.ReplaceFSMProcessingCnt (cnt - 1);
	}

}
