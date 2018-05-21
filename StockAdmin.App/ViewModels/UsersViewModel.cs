using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;

namespace SigiFluent.ViewModels
{
    public class UsersViewModel:BaseViewModel
    {

        public ObservableCollection<LocalUser> UsersCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _users == null)
                {
                    _users = Membership.GetUsers();
                }

                IsBusy = false;
                return _users;

            }
            set
            {
                _users = value;
                RaisePropertyChanged("UsersCollection");
            }
        }

        public ObservableCollection<UserDocument> UserDocumentsCollection
        {
            get
            {

                docs =docs = new ObservableCollection<UserDocument>(AllDocuments);
                if (SelectedUser == null) return docs;

                foreach (var doc in from doc in docs let document = SelectedUser.UserDocuments.FirstOrDefault(dc => dc.Document == doc.Document) where document == null select doc)
                {
                    doc.IsAvailable = false;
                }
                if (SelectedUser.UserDocuments.Any()) return docs;
                foreach (var doc in docs)
                {
                    doc.IsAvailable = true;
                    SelectedUser.UserDocuments.Add(doc);
                }
                return docs;

            }
            set
            {
                if (value != null)
                {
                    docs = value;
                   
                    RaisePropertyChanged("UserDocumentsCollection");
                    
                }

            }
        }

        public UserDocument SelectedDocument
        {
            get { return document; }
            set { document = value;RaisePropertyChanged("SelectedUser"); }
        }

        public LocalUser SelectedUser
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged("SelectedUser");
                RaisePropertyChanged("UserDocumentsCollection");
            }
        }

        #region  Commands

        public ICommand EditCurrentCommand
        {
            get
            {
                return new RelayCommand(EditSeletedUser,CanExecuteEdit);
            }
        }

        public ICommand DeleteSelectedCommand
        {
            get
            {
                return new RelayCommand(DeleteSelectedUser,CanExecteDelete);
            }
        }

        public ICommand SaveDetailsCommand
        {
            get
            {
                return  new RelayCommand(Save);
            }
        }

        public ICommand CreateNewUserCommand
        {
            get
            {
                return new RelayCommand(CreateNewUser);
            }
        }

       

        #endregion

        public void EditSeletedUser()
        {
            ShowModal(new UsersView());
           //ViewModelManager.ShowModal(new UsersView());
             
        }

        public void DeleteSelectedUser()
        {
            if (SelectedUser != null)
            {
                Membership.DeleteUser(SelectedUser);
                UsersCollection.Remove(SelectedUser);
            }
        }

        public void Save()
        {
            //TODO save Changes 

            foreach (var userDocument in docs)
            {
                var doc = SelectedUser.UserDocuments.FirstOrDefault(d => d.Document == userDocument.Document);
                if(doc!=null) doc.IsAvailable = userDocument.IsAvailable;
                else if(userDocument.IsAvailable) SelectedUser.UserDocuments.Add(userDocument);

            }
            Membership.AddOrUpdate(SelectedUser);

            SaveChanges();
            IsModalVisible = false;
            RefreshItemSource();
        }

        public void CreateNewUser()
        {
            

            SelectedUser = new LocalUser()
                           {
                               UserDocuments = AllDocuments,
                           };
            ShowModal(new UsersView());
        }

        #region Overrides
        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("UsersCollection");
        }

       
        #endregion

        #region private properties

        private ObservableCollection<LocalUser> _users;
        private LocalUser _user;

        private ObservableCollection<UserDocument> docs; 

        private List<UserDocument> AllDocuments
        {
            get
            {
                return  Membership.GetDocumentsForUser().Select(d => new UserDocument()
                {
                    Document = d,
                    ObjType = d.ObjType,

                }).ToList();
            }
        }

        private UserDocument document;

        #endregion
    }
}
