//created by snippet - ecs_game_controller
using Entitas;
using UnityEngine;

public class FSMGameController : MonoBehaviour {

	//prefabs
	public GameObject cube_prefab;
	//
	public GameObject[] static_event_listerner_objs;


	public FSMServices _services = FSMServices.singleton;

	Systems _systems;

	void Awake() {
		var contexts = Contexts.sharedInstance;
		_services.Initialize (contexts, this);
		_systems = new FSMGameSystems(contexts);
	}

	void Start() {
		_systems.Initialize();
	}

	void Update() {
		_systems.Execute();
		_systems.Cleanup();
	}

	void OnDestroy() {
		_systems.TearDown();
	}
}