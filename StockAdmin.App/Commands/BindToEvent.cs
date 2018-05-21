     using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

 
namespace SigiFluent.Commands
{
   
    public class InvokeDataCommand : TriggerAction<FrameworkElement>
    {

#if SILVERLIGHT
		/// <summary>Backing DP for the Command property</summary>
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(Binding), typeof(InvokeDataCommand), new PropertyMetadata(null, InvokeDataCommand.HandleCommandChanged));
#else
        /// <summary>Backing DP for the Command property</summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeDataCommand), new PropertyMetadata(null, InvokeDataCommand.HandleCommandChanged));
#endif
        /// <summary>Backing DP for the CommandParameter property</summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeDataCommand), new PropertyMetadata(null));

#if SILVERLIGHT
		private BindingListener listener = new BindingListener();

		/// <summary>
		/// Binding to the command which is to be invoked
		/// </summary>
		public Binding Command {
			get { return (Binding)this.GetValue(InvokeDataCommand.CommandProperty); }
			set { this.SetValue(InvokeDataCommand.CommandProperty, value); }
		}
#else
        /// <summary>
        /// Binding to the command which is to be invoked
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(InvokeDataCommand.CommandProperty); }
            set { this.SetValue(InvokeDataCommand.CommandProperty, value); }
        }
#endif

        /// <summary>
        /// Optional parameter for the command.
        /// </summary>
        public object CommandParameter
        {
            get { return (object)this.GetValue(InvokeDataCommand.CommandParameterProperty); }
            set { this.SetValue(InvokeDataCommand.CommandParameterProperty, value); }
        }

#if SILVERLIGHT
		/// <summary>
		/// Initialization
		/// </summary>
		protected override void OnAttached() {
			base.OnAttached();

			this.listener.Element = this.AssociatedObject;
		}

		/// <summary>
		/// Cleanup
		/// </summary>
		protected override void OnDetaching() {
			base.OnDetaching();
			this.listener.Element = null;
		}
#endif

        /// <summary>
        /// Fire the command.
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {

#if SILVERLIGHT
			ICommand command = this.listener.Value as ICommand;
#else
            var command = this.Command;
#endif

            if (command != null && command.CanExecute(this.CommandParameter))
            {
                command.Execute(this.CommandParameter);
            }
        }

        private static void HandleCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((InvokeDataCommand)sender).OnCommandChanged(e);
        }

        /// <summary>
        /// Notification that the command property has changed.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCommandChanged(DependencyPropertyChangedEventArgs e)
        {
#if SILVERLIGHT
            this.listener.Binding = this.Command;
#endif
        }
    }
   

}
