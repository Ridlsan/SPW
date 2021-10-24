namespace SPW
{
	internal class ChangeItem
	{
		public readonly SwDynamicList List;
		public readonly SwItemData swItem;
		public readonly OperationType OperationType;

		/// <summary>
		/// Gets a value indicating whether item is proccessed.
		/// </summary>
		public bool Proccessed { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChangeItem"/> class.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="swItem"></param>
		/// <param name="operationType"></param>
		public ChangeItem(SwDynamicList list, SwItemData swItem, OperationType operationType)
		{
			this.List = list;
			this.swItem = swItem;
			this.OperationType = operationType;
		}
	}
}
