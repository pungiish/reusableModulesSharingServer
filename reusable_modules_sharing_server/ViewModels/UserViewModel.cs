using Omu.ValueInjecter;
using reusable_modules_sharing_server.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WidgetServer.Models;

namespace WidgetServer.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Familyname { get; set; }
        public string GoogleID { get; set; }

        public User ToUser(UserViewModel userViewModel)
        {

            var user = Mapper.Map<User>(userViewModel);
            return user;
        }
    }

    public class ListUserViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Familyname { get; set; }
        public string GoogleID { get; set; }
        public ICollection<NewWidgetViewModel> Widgets { get; set; }
    }
}
