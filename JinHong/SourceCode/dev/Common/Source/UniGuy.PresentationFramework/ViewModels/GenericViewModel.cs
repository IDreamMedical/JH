using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Windows.View;

namespace UniGuy.Windows.ViewModels
{
    /// <summary>
    /// 泛型ViewModel
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class GenericViewModel<TView> : ViewModelBase where TView : IView
    {

         private readonly TView view;


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel&lt;TView&gt;"/> class and attaches itself as <c>DataContext</c> to the view.
        /// </summary>
        /// <param name="view">The view.</param>
        protected GenericViewModel(TView view) : base(view)
        {
            this.view = view;
        }


        /// <summary>
        /// Gets the associated view as specified view type.
        /// </summary>
        /// <remarks>
        /// Use this property in a ViewModel class to avoid casting.
        /// </remarks>
        protected TView ViewCore { get { return view; } }


    }
}
