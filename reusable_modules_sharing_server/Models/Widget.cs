using Omu.ValueInjecter;
using reusable_modules_sharing_server.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WidgetServer.Models;
using WidgetServer.ViewModels;

namespace reusable_modules_sharing_server.Models
{
    public class Widget
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Text { get; set; }
        public string Tag { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }

    }

    public static class WidgetExtensions {

        public static NewWidgetViewModel ToNewViewModel(this Widget model)
        {
            if (model == null)
                return null;

            var vm = Mapper.Map<NewWidgetViewModel>(model);
            return vm;
        }

        public static ListWidgetViewModel ToViewModel(this Widget model)
        {
            if (model == null)
                return null;

            var vm = Mapper.Map<ListWidgetViewModel>(model);
            vm.User = model.User.ToViewModel();
            return vm;
        }

        

        public static ICollection<NewWidgetViewModel> ToViewModel(this ICollection<Widget> model)
        {
            if (model == null)
                return null;

            var vm = model.Select(w => Mapper.Map<NewWidgetViewModel>(w)).ToList();
            return vm;
        }
        
    }
}
