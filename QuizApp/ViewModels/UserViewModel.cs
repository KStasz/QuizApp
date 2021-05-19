using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using System.Windows.Input;
using QuizApp.ViewModelHelpers;
using QuizApp.AdminPages.Users;

namespace QuizApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ButtonProperties
        private ICommand _btnCreateUser;
        public ICommand BtnCreateUser
        {
            get
            {
                return _btnCreateUser;
            }
            private set { }
        }

        private ICommand _btnSave;
        public ICommand BtnSave
        {
            get
            {
                return _btnSave;
            }
            private set { }
        }

        private ICommand _btnDelete;
        public ICommand BtnDelete
        {
            get
            {
                return _btnDelete;
            }
            private set { }
        }

        #endregion

        #region Fields
        private readonly IApplicationUserRepository _applicationUserRepository;
        #endregion

        #region Properties

        private ObservableCollection<ApplicationUsers> _avilableApplicationUsers;
        public ObservableCollection<ApplicationUsers> AvilableApplicationUsers
        {
            get
            {
                return _avilableApplicationUsers;
            }
            set
            {
                _avilableApplicationUsers = value;
                NotifyPropertyChanged(nameof(AvilableApplicationUsers));
            }
        }

        private ApplicationUsers _selectedApplicationUsers;
        public ApplicationUsers SelectedApplicationUsers
        {
            get
            {
                return _selectedApplicationUsers;
            }
            set
            {
                _selectedApplicationUsers = value;
                if (SelectedApplicationUsers != null)
                    SelectedRole(SelectedApplicationUsers.RoleId);
                NotifyPropertyChanged(nameof(SelectedApplicationUsers));
            }
        }

        private ApplicationUserRoles _selectedApplicationUserRoles;
        public ApplicationUserRoles SelectedApplicationUserRoles
        {
            get
            {
                return _selectedApplicationUserRoles;
            }
            set
            {
                _selectedApplicationUserRoles = value;
                if (SelectedApplicationUserRoles != null && SelectedApplicationUsers != null)
                    AssignRoleToUser(SelectedApplicationUserRoles);
                NotifyPropertyChanged(nameof(SelectedApplicationUserRoles));
            }
        }

        private ObservableCollection<ApplicationUserRoles> _avilableRoles;
        public ObservableCollection<ApplicationUserRoles> AvilableRoles
        {
            get
            {
                return _avilableRoles;
            }
            set
            {
                _avilableRoles = value;
                NotifyPropertyChanged(nameof(AvilableRoles));
            }
        }

        #endregion

        #region Constructor
        public UserViewModel(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
            SetInitialData();
        }
        #endregion

        #region PrivateMethods

        private void SetInitialData()
        {
            ReadUsers();
            ReadRoles();
            _btnDelete = new RelayCommand(x => BtnDelete_Click());
            _btnCreateUser = new RelayCommand(x => BtnCreateUser_Click());
            _btnSave = new RelayCommand(x => BtnSave_Click());
        }

        private void BtnDelete_Click()
        {
            if (SelectedApplicationUsers != null)
            {
                _applicationUserRepository.DeleteUser(SelectedApplicationUsers);
                ReadUsers();
                SelectedApplicationUserRoles = new ApplicationUserRoles() { RoleName = "" };
            }
        }

        private void AssignRoleToUser(ApplicationUserRoles selectedApplicationUserRoles)
        {
            SelectedApplicationUsers.RoleId = selectedApplicationUserRoles.Id;
            SelectedApplicationUsers.role = selectedApplicationUserRoles;
        }

        private void SelectedRole(int roleId)
        {
            SelectedApplicationUserRoles = AvilableRoles.FirstOrDefault(x => x.Id == roleId);
        }

        private void ReadRoles()
        {
            AvilableRoles = new ObservableCollection<ApplicationUserRoles>(_applicationUserRepository.GetRoles());
        }

        private void ReadUsers()
        {
            AvilableApplicationUsers = new ObservableCollection<ApplicationUsers>(_applicationUserRepository.ListOfUsers());
        }

        private void BtnSave_Click()
        {
            if (SelectedApplicationUsers.Password.Length >= 6)
            {
                _applicationUserRepository.UpdateUser(SelectedApplicationUsers);
                ReadUsers();
                SelectedApplicationUserRoles = new ApplicationUserRoles() { RoleName = "" };
            }
        }

        private void BtnCreateUser_Click()
        {
            var createUser = new CreateUserWindow();
            createUser.ShowDialog();
            if (createUser.DialogResult == true)
            {
                _applicationUserRepository.CreateUser(new ApplicationUsers() { Username = createUser.username, Password = createUser.password });
                ReadUsers();
            }
        }
        #endregion
    }
}
