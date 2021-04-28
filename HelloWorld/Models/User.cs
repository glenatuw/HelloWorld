using System.ComponentModel;

namespace HelloWorld.Models
{
    class User : IDataErrorInfo, INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string password = string.Empty;
        private string nameError;
        private string passwordError;
        private bool isSubmitEnabled;
        private const int MAX_INPUT_SIZE = 12;

        public string NameError
        {
            get
            {
                return nameError;
            }
            set
            {
                if (nameError != value)
                {
                    nameError = value;
                    OnPropertyChanged("NameError");
                }
            }
        }

        public string PasswordError
        {
            get
            {
                return passwordError;
            }
            set
            {
                if (passwordError != value)
                {
                    passwordError = value;
                    OnPropertyChanged("PasswordError");
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        // IDataErrorInfo interface
        public string Error
        {
            get
            {
                return NameError;
            }
        }

        public bool IsSubmitEnabled
        {
            get
            {
                return isSubmitEnabled;
            }

            set
            {
                if (isSubmitEnabled != value)
                {
                    isSubmitEnabled = value;
                    OnPropertyChanged("IsSubmitEnabled");
                }
            }

        }

        // check that a field (name or password) is OK
        private bool checkValidInput(string field, string fieldName, out string error)
        {
            if (string.IsNullOrEmpty(field))
            {
                error = fieldName + " cannot be empty.";
                return false;
            }

            if (field.Length > MAX_INPUT_SIZE)
            {
                error = fieldName + " cannot be longer than " + MAX_INPUT_SIZE + " characters.";
                return false;
            }

            error = string.Empty;
            return true;
        }

        private void checkSubmit()
        {
            string error;
            if (checkValidInput(Name, "Name", out error) && checkValidInput(Password, "Password", out error))
                IsSubmitEnabled = true;
            else
                IsSubmitEnabled = false;
        }

        // IDataErrorInfo interface
        // Called when ValidatesOnDataErrors=True
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        {
                            string error;
                            checkValidInput(Name, "Name", out error);
                            NameError = error;
                            checkSubmit();
                            return NameError;
                        }

                    case "Password":
                        {
                            string error;
                            checkValidInput(Password, "Password", out error);
                            PasswordError = error;
                            checkSubmit();
                            return PasswordError;
                        }
                    default:
                        return string.Empty;
                }
            }
        }

        // INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}