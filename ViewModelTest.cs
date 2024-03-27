using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BindingDemoForGithub
{
    public class ViewModelTest : INotifyPropertyChanged
    {
        public ViewModelTest()
        {
            string[] firstNames = new string[] {"James", "Robert", "John", "Michael", "David", "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth"};
            string[] lastNames = new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };

            foreach (string firstName in firstNames)
            {
                foreach (string lastName in lastNames)
                {
                    _allUsers.Add(new TestUser() {GUID = new Guid(), UserName = firstName + " " + lastName});
                }
            }
        }

        private List<TestUser> _allUsers = new List<TestUser>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<TestUser> _testUsers = new List<TestUser>();

        public List<TestUser> TestUsers
        {
            get
            {
                return _testUsers;
            }
            set
            {
                _testUsers = value;
                OnPropertyChanged();
            }
        }

        private string _searchString;

        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                SearchUsers(value);
                _searchString = value;
            }
        }

        private void SearchUsers(string searchString)
        {
            try
            {
                TestUsers = _allUsers.FindAll(x => x.UserName.ToLower().Contains(searchString.ToLower()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public class TestUser
        {
            public string UserName { get; set; }
            public Guid GUID { get; set; }
        }
    }
}
