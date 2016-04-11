using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSP_Chroma_Control
{
    /// <summary>
    /// This class summarizes the state of the game in flight mode to consider when lighting up the keyboard.
    /// </summary>
    class CraftState
    {
        private Decimal LiquidFuel { get; set; }
        private Decimal Oxidizer { get; set; }
        private Decimal Electricity { get; set; }
        private Decimal MonoPropellant { get; set; }
        private Decimal SolidFuel { get; set; }
        private Decimal EVAPropellant { get; set; }
        private Decimal IntakeAir { get; set; }
        private Decimal XenonGas { get; set; }
        private Decimal Ore { get; set; }
        private Decimal Ablator { get; set; }
        private Boolean Overheating { get; set; }
        private Boolean Controllable { get; set; }
    }
}
