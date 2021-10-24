namespace SPW
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	///  Changes manager.
	/// </summary>
	internal class ChangesManager
	{
		/// <summary>
		/// Gets or sets changes.
		/// </summary>
		private List<ChangeItem> items = new List<ChangeItem>();

		// TODO add redo handler when fail happens.
		// TODO Add Retries for update failures when simulatanius conflicts happens

		/// <summary>
		///  Runs all pending changes.
		/// </summary>
		public void RunChanges()
		{
			foreach (var item in this.items.Where(i => i.OperationType == OperationType.Add))
			{
				item.List.Create(item.swItem);
			}

			foreach (var item in this.items.Where(i => i.OperationType == OperationType.Update))
			{
				item.List.Update(item.swItem);
			}

			foreach (var item in this.items.Where(i => i.OperationType == OperationType.Recycle))
			{
				item.List.Recycle(item.swItem);
			}

			foreach (var item in this.items.Where(i => i.OperationType == OperationType.Delete))
			{
				item.List.Delete(item.swItem);
			}
		}

		public void AddChange(SwDynamicList list, SwItemData swItem, OperationType operationType)
		{
			this.items.Add(new ChangeItem(list, swItem, operationType));
		}
	}
}
