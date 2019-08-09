using Omu.ValueInjecter;
using reusable_modules_sharing_server.Models;
using reusable_modules_sharing_server.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WidgetServer.ViewModels;

namespace WidgetServer.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Familyname { get; set; }
        public string GoogleID { get; set; }

        // noben ne sme videti tega
        //public string Private { get; set; }

        public ICollection<Widget> Widgets { get; set; }

      
    }

    public static class UserExtensions
    {
        public static UserViewModel ToViewModel(this User model)
        {
            if (model == null)
                return null;

            var vm = Mapper.Map<UserViewModel>(model);
            return vm;
        }

        public static ListUserViewModel ToListViewModel(this User model)
        {
            if (model == null)
                return null;

            var vm = Mapper.Map<ListUserViewModel>(model);
            vm.Widgets = model.Widgets.ToViewModel();
            return vm;
        }


    }
}
