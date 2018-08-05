//created by snippet - ecs_entity_services
using UnityEngine;

public class FSMDebugService {
	//other Serices include
	//public RandomService randomService = RandomService.game;
	//other Mono class include
	//public BackGroundManager bg_manager;
	


	public static FSMDebugService singleton = new FSMDebugService();

	//Contexts _contexts;

	public void Initialize(Contexts contexts) {
		//_contexts = contexts;
		//bg_manager = GameObject.Find ("BackGroundSprite");
	}

	public void fsm_msg_report(string message) {
		Debug.Log(message);
	}

}