using System.Runtime.Serialization;

namespace Tools.Misc.AppStore.Enums
{
	public enum ReceiptType
	{
		[EnumMember(Value = "Production")]
		Production,

		[EnumMember(Value = "ProductionVPP")]
		VolumeProduction,

		[EnumMember(Value = "ProductionSandbox")]
		Sandbox,

		[EnumMember(Value = "ProductionVPPSandbox")]
		VolumeSandbox
	}
}