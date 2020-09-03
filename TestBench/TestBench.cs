using System;
using System.Collections.Generic;
using System.Linq;



namespace ForwardTask.TestBench
{
	public class TestBench
	{
		public static void Start( IEngineSimulator simulator, Engines.AbstractEngine engine )
		{
			simulator.Simulate( engine );
		}
	}
}
