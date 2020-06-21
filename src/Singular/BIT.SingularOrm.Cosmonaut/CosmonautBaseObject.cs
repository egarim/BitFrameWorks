using Cosmonaut;
using Cosmonaut.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BIT.SingularOrm.Cosmonaut
{
    //TODO implement property change
    [SharedCosmosCollection("shared", true)]
    public class CosmonautBaseObject : ISharedCosmosEntity, INotifyPropertyChanged, IBrevitasEntityEvents
    {
        private string cosmosEntityName;
        public string Id { get; set; }

        [CosmosPartitionKey] public string Name { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public string CosmosEntityName
        {
            get => cosmosEntityName;
            set => SetProperty(ref cosmosEntityName, value);
        }

        #region PropertyChanged

        /// <summary>
        ///     Checks if a property already matches a desired value. Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Checks if a property already matches a desired value. Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <param name="onChanged">Action that is called after the property value has been changed.</param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            //TODO: when we remove the old OnPropertyChanged method we need to uncomment the below line
            //OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
#pragma warning disable CS0618 // Type or member is obsolete
            OnPropertyChanged(propertyName);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        [Obsolete(
            "Please use the new RaisePropertyChanged method. This method will be removed to comply wth .NET coding standards. If you are overriding this method, you should overide the OnPropertyChanged(PropertyChangedEventArgs args) signature instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="args">The PropertyChangedEventArgs</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        /// <summary>
        ///     Raises this object's PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        [Obsolete(
            "Please use RaisePropertyChanged(nameof(PropertyName)) instead. Expressions are slower, and the new nameof feature eliminates the magic strings.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = "";
            //var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        public void OnCreated()
        {
            throw new NotImplementedException();
        }

        public void OnLoaded()
        {
            throw new NotImplementedException();
        }

        public void OnSaved()
        {
            throw new NotImplementedException();
        }

        public void OnDeleted()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}