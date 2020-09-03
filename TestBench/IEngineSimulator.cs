using System;
using System.Collections.Generic;
using System.Linq;

using ForwardTask.Engines;



namespace ForwardTask.TestBench
{
	public interface IEngineSimulator
	{
		void Simulate( AbstractEngine engine );
	}
}
