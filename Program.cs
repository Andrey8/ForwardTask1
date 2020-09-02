using System;
using System.Collections.Generic;
using System.Linq;

using ForwardTask.Engines;
using ForwardTask.Data;



namespace ForwardTask
{
	class Program
	{
		static void Main( string[] args )
		{
			var parameters = new EngineParameters();
			parameters.MomentOfInertia = 10;
			parameters.CrankshaftSpeedToTorquePoints = new List< Point >
			{
				new Point( 0, 20 ), 
				new Point( 75, 75 ), 
				new Point( 150, 100 ), 
				new Point( 200, 105 ), 
				new Point( 250, 75 ), 
				new Point( 300, 0 )
			};
			parameters.OverheatTemperature = 110;
			parameters.HeatingSpeedOfTorqueCoeff = 0.01;
			parameters.HeatingSpeedOfCrankshaftSpeedCoeff = 0.0001;
			parameters.CoolingSpeedOfTemperaturesCoeff = 0.1;

			Console.Write( "Enter ambient temperature : " );
			double ambientTemperature = Convert.ToInt32( Console.ReadLine() );				

			var testBench = new TestBench( parameters, ambientTemperature );
			var timeSpan = testBench.Simulate( new ICEngine() );

			Console.WriteLine( "Total time : " + timeSpan.Seconds );
		}
	}
}
