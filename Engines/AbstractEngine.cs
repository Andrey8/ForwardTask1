using System;
using System.Collections.Generic;
using System.Linq;



namespace ForwardTask.Engines
{
	public abstract class AbstractEngine
	{
		Base.Temperature temperature;
		public Base.Temperature Temperature
		{
			get { return temperature; }
			protected set { temperature = value; }
		}



		public AbstractEngine()
		{
			this.temperature = new Base.Temperature( 0 );
		}

		public abstract void Start( Base.Temperature temperature );

		public void Heat( Base.Temperature temp )
		{
			temperature.Celsius += temp.Celsius;
		}

		public void Cool( Base.Temperature temp )
		{
			temperature.Celsius -= temp.Celsius;
		}
	}
}
