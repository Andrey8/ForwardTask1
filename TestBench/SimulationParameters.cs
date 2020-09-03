using System;
using System.Collections.Generic;
using System.Linq;

using ForwardTask.Base;



namespace ForwardTask
{
	public class SimulationParameters
	{
		public int MomentOfInertia;
		public List< Point > CrankshaftSpeedToTorquePoints;
		public Base.Temperature OverheatTemperature;
		public double HeatingSpeedOfTorqueCoeff;
		public double HeatingSpeedOfCrankshaftSpeedCoeff;
		public double CoolingSpeedOfTemperaturesCoeff;
	}
}
