//created by snippet - ecs_entity_services
using UnityEngine;
using System.Collections.Generic;

public class FSMSwitchService {
	//other Serices include
	//public RandomService randomService = RandomService.game;
	//other Mono class include
	//public BackGroundManager bg_manager;
	FSMDebugService fsm_debug_service = FSMDebugService.singleton;

	public static FSMSwitchService singleton = new FSMSwitchService();

	Contexts _contexts;

	public void Initialize(Contexts contexts) {
		_contexts = contexts;
	}
	//提供一個init API
	public GameEntity fsm_init() {
		GameEntity e = _contexts.game.CreateEntity ();
		e.AddFSMState(GAMESTATE.GAME_INIT, GAMESTATE.GAME_INIT);

		List<GAMESTATE> _stack_list = new List<GAMESTATE> ();
		_stack_list.Add (GAMESTATE.GAME_INIT);

		e.AddFSMStack (_stack_list);
		return e;
	}


	//我們提供一種服務讓外部系統switch fsm : 如果fsm 沒有switch component,就新增,有,就用replace
	public void fsm_switch(GameEntity e, GAMESTATE from_state, GAMESTATE to_state) {
		if (e.isFSMReturn) {
			fsm_debug_service.fsm_msg_report ("Warning, Already have FSMReturn in entity");
			return;
		}

		if (e.hasFSMSwitch)
			e.ReplaceFSMSwitch (false, from_state, to_state);
		else
			e.AddFSMSwitch (false, from_state, to_state);
	}

	//我們提供一種服務讓外部系統switch fsm : 如果fsm 沒有switch component,就新增,有,就用replace
	//這是決定要stack fsm_state的 function call
	public void fsm_switch_stack(GameEntity e, GAMESTATE from_state, GAMESTATE to_state) {
		if (e.isFSMReturn) {
			fsm_debug_service.fsm_msg_report ("Warning, Already have FSMReturn in entity");
			return;
		}

		if (e.hasFSMSwitch)
			e.ReplaceFSMSwitch (true, from_state, to_state);
		else
			e.AddFSMSwitch (true, from_state, to_state);
	}

	public void fsm_reset(GameEntity e, string reason) {
		if(!e.hasFSMReset)
			e.AddFSMReset (reason);
	}
	public void fsm_hold(GameEntity e, string reason) {
		if(!e.hasFSMHold)
			e.AddFSMHold (reason);
	}
	public void fsm_continue(GameEntity e, string reason) {
		if(!e.hasFSMContinue)
			e.AddFSMContinue (reason);
	}
	public void fsm_return(GameEntity e) {
		if (e.hasFSMSwitch) {
			fsm_debug_service.fsm_msg_report ("Warning, Already have FSMSwitch in entity");
			return;
		}

		if (!e.isFSMReturn)
			e.isFSMReturn = true;
	}



}