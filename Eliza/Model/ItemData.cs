using MessagePack;

namespace Eliza.Model
{
	// Union, but idk where this is inherited
	[MessagePackObject]
	public class ItemData
	{
		[Key(0)]
		public int ItemID;
	}
}
