using System;
using System.Collections.Generic;
using System.Linq;



namespace ForwardTask.Engines
{
	public class ICEngine : IEngine
	{
		double crankshaftSpeed;

		double temperature;
		public double Temperature
		{
			get { return temperature; }
			private set { temperature = value; }
		}


		
		public void Start( double temperature )
		{
			this.crankshaftSpeed = 0;
			this.temperature = temperature;
		}

		public void Heat( double temp )
		{
			temperature += temp;
		}

		public void Cool( double temp )
		{
			temperature -= temp;
		}
	}
}
