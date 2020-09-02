using System;
using System.Collections.Generic;
using System.Linq;



namespace ForwardTask.Engines
{
	public interface IEngine
	{
		void Start( double temperature );
		void Heat( double temp );
		void Cool( double temp );
	}
}
