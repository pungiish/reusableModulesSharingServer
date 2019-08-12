using Omu.ValueInjecter;
using reusable_modules_sharing_server.Models;
using System.Collections.Generic;
using WidgetServer.ViewModels;

namespace reusable_modules_sharing_server.ViewModels
{
    public class NewWidgetViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateWidgetViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }

    }

    public class ListWidgetViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public UserViewModel User { get; set; }
    }

    public static class WidgetModelExtensions
    {
        public static Widget ToModel(this NewWidgetViewModel viewmodel)
        {
            if (viewmodel == null)
                return null;

            return Mapper.Map<Widget>(viewmodel);
        }

        public static Widget ToModel(this UpdateWidgetViewModel viewmodel)
        {
            if (viewmodel == null)
                return null;

            return Mapper.Map<Widget>(viewmodel);

        }
    }

}
