using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Specialized;

namespace IL41ML_HFT_2021221.WpfApp_v2
{
	public partial class NonCrudViewModel : ObservableObject
	{
		[ObservableProperty]
		private INotifyCollectionChanged collection;

		public NonCrudViewModel(INotifyCollectionChanged collection) => this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
	}
}
