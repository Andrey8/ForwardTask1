using System;
using System.Collections.Generic;
using System.Linq;



namespace ForwardTask.Engines
{
	public class InternalCombustionEngine : AbstractEngine
	{
		public double CrankshaftSpeed { get; set; }



		public InternalCombustionEngine() : base() { }

		public override void Start( Base.Temperature temperature )
		{
			this.CrankshaftSpeed = 0;
			Temperature.Celsius = temperature.Celsius;
		}
	}
}
