//created by snippet - ecs_services
using System;

public class FSMServices {

	public static FSMServices singleton = new FSMServices();

	public void Initialize(Contexts contexts, FSMGameController uniTestController) {

		FSMDebugService.singleton.Initialize(contexts);
		FSMSwitchService.singleton.Initialize (contexts);
		FSMAdapterService.singleton.Initialize (contexts, uniTestController);
	}
}