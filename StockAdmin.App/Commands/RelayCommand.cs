using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SigiFluent
{
        /// <summary>
        /// Un comando cuyo único propósito es transmitir su funcionalidad a otros objetos mediante la invocación de delegados. El valor de retorno por defecto para el método CanExecute es 'true'.
        /// </summary>
        public class RelayCommand<T> : ICommand
        {

            #region Declarations

            readonly Predicate<T> _canExecute;
            readonly Action<T> _execute;

            #endregion

            #region Constructors

            /// <summary>
            /// <see cref="RelayCommand&lt;T&gt;"/>  y el comando siempre pueden ser ejecutados.
            /// </summary>
            /// <param name="execute">La lógica de ejecución.</param>
            public RelayCommand(Action<T> execute)
                : this(execute, null)
            {
            }

            /// <summary>
            /// <see cref="RelayCommand&lt;T&gt;"/> Inicia una nueva isntancia de la clase.
            /// </summary>
            /// <param name="execute">La lógica de ejecución.</param>
            /// <param name="canExecute">El estaod de la lógica de ejecución.</param>
            public RelayCommand(Action<T> execute, Predicate<T> canExecute)
            {

                if (execute == null)
                    throw new ArgumentNullException("execute");
                _execute = execute;
                _canExecute = canExecute;
            }

            #endregion

            #region ICommand Members

            public event EventHandler CanExecuteChanged
            {
                add
                {
                    if (_canExecute != null)
                        CommandManager.RequerySuggested += value;
                }
                remove
                {
                    if (_canExecute != null)
                        CommandManager.RequerySuggested -= value;
                }
            }


            public Boolean CanExecute(Object parameter)
            {
                return _canExecute == null || _canExecute((T)parameter);
            }

            public void Execute(Object parameter)
            {
                _execute((T)parameter);
            }

            #endregion
        }

        /// <summary>
        /// Un comando cuyo único propósito es transmitir su funcionalidad a otros objetos mediante la invocación de delegados. El valor de retorno por defecto para el método CanExecute es 'true'.
        /// </summary>
        public class RelayCommand : ICommand
        {

            #region Declaraciones

            readonly Func<Boolean> _canExecute;
            readonly Action _execute;

            #endregion

            #region Constructores

            /// <summary>
            /// <see cref="RelayCommand&lt;T&gt;"/> Inicia una nueva isntancia de la clase.
            /// </summary>
            /// <param name="execute">La lógica de ejecución.</param>
            public RelayCommand(Action execute)
                : this(execute, null)
            {
            }

            /// <summary>
            /// <see cref="RelayCommand&lt;T&gt;"/> Inicia una nueva isntancia de la clase.
            /// </summary>
            /// <param name="execute">La lógica de ejecución.</param>
            /// <param name="canExecute">El estaod de la lógica de ejecución.</param>
            public RelayCommand(Action execute, Func<Boolean> canExecute)
            {

                if (execute == null)
                    throw new ArgumentNullException("execute");
                _execute = execute;
                _canExecute = canExecute;
            }

            #endregion

            #region Miembros ICommand

            public event EventHandler CanExecuteChanged
            {
                add
                {

                    if (_canExecute != null)
                        CommandManager.RequerySuggested += value;
                }
                remove
                {

                    if (_canExecute != null)
                        CommandManager.RequerySuggested -= value;
                }
            }


            public Boolean CanExecute(Object parameter)
            {
                return _canExecute == null || _canExecute();
            }

            public void Execute(Object parameter)
            {
                _execute();
            }

            #endregion
        }
    
}
