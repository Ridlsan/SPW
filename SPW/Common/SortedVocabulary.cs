namespace SPW.Common
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Enchanced dictionary.
	/// </summary>
	/// <typeparam name="TKey">Type of index.</typeparam>
	/// <typeparam name="TValue">Type of values.</typeparam>
	public class SortedVocabulary<TKey, TValue> : SortedDictionary<TKey, TValue>
	{
		/// <summary>
		/// Gets or sets function to add value if it not exists in dictionary.
		/// </summary>
		public Func<TKey, TValue> OnMissingGet { get; set; }

		/// <summary>
		/// Gets value by key.
		/// </summary>
		/// <param name="key">Index in vocabulary.</param>
		/// <returns>Value.</returns>
		public new TValue this[TKey key]
		{
			get
			{
				if (!this.ContainsKey(key))
				{
					if (this.OnMissingGet != null)
					{
						base[key] = this.OnMissingGet(key);
					}
				}

				return base[key];
			}

			set
			{
				base[key] = value;
			}
		}
	}
}
