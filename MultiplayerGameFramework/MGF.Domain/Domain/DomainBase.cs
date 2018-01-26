using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace MGF.Domain
{
    [Serializable()]
    public abstract class DomainBase : IProcessDirty
    {
        // Keep track of whether object is new, deleted or dirty.
        private bool isObjectNew = true;
        private bool isObjectDirty = false;
        private bool isObjectDeleted;

        #region IProcessDirty

        [Browsable(false)]
        public bool IsNew
        {
            get { return isObjectNew; }
            set { isObjectNew = value; } // only used during deserialization - must be public
        }

        [Browsable(false)]
        public bool IsDirty
        {
            get { return isObjectDirty; }
            set { isObjectDirty = value; } // only used during deserialization - must be public
        }

        [Browsable(false)]
        public bool IsDeleted
        {
            get { return isObjectDeleted; }
            set { isObjectDeleted = value; } // only used during deserialization - must be public
        }

        #endregion

        [NonSerialized()]
        private PropertyChangedEventHandler _nonSerializableHandlers;
        private PropertyChangedEventHandler _serializableHandlers;

        [Browsable(false), XmlIgnore()]
        public virtual bool IsSavable
        {
            get { return isObjectDirty; } // Usually some validation goes on here.
        }

        // Pattern from CLSA.Net - a domain driven design pattern based in BindableBase.
        // Necessary to make serialization work properly and more importantly safely.
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (value.Method.IsPublic && (value.Method.DeclaringType.IsSerializable || value.Method.IsStatic))
                    _serializableHandlers = (PropertyChangedEventHandler)Delegate.Combine(_serializableHandlers, value);
                else
                    _nonSerializableHandlers = (PropertyChangedEventHandler)Delegate.Combine(_nonSerializableHandlers, value);
            }

            remove
            {
                if (value.Method.IsPublic && (value.Method.DeclaringType.IsSerializable || value.Method.IsStatic))
                    _serializableHandlers = (PropertyChangedEventHandler)Delegate.Remove(_serializableHandlers, value);
                else
                    _nonSerializableHandlers = (PropertyChangedEventHandler)Delegate.Remove(_nonSerializableHandlers, value);
            }
        }

        // Automatically called by MarkDirty. Refresh all properties (useful in applications that need to refresh data).
        protected virtual void OnUnknownPropertyChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_nonSerializableHandlers != null)
                _nonSerializableHandlers.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (_serializableHandlers != null)
                _serializableHandlers.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Used by the constructor to denote a brand new object that is not stored in the database and ensures it isn't being 
        // marked for deletion.
        protected virtual void MarkNew()
        {
            isObjectNew = true;
            isObjectDeleted = false;
            MarkDirty();
        }

        // Used by Fetch to denote an object that already exist and has been pulled from database.
        protected virtual void MarkOld()
        {
            isObjectNew = false;
            MarkClean();
        }

        protected void MarkDeleted()
        {
            isObjectDeleted = true;
            MarkDirty();
        }

        // Call any time data changes to denote that the object needs to be saved.
        protected void MarkDirty()
        {
            MarkDirty(false);
        }

        protected void MarkDirty(bool suppressEvent)
        {
            isObjectDirty = true;

            if (!suppressEvent)
            {
                // Force properties to refresh - only useful for web pages and win forms.
                OnUnknownPropertyChanged();
            }
        }

        protected void PropertyHasChanged()
        {
            PropertyHasChanged(new StackTrace().GetFrame(1).GetMethod().Name.Substring(4));
        }

        protected virtual void PropertyHasChanged(string propertyName)
        {
            MarkDirty(true);
            OnPropertyChanged(propertyName);
        }

        protected void MarkClean()
        {
            isObjectDirty = false;
        }

        public virtual void Delete()
        {
            this.MarkDeleted();
        }
    }
}
