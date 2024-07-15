using SimpleList.WebUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleList.WebUI.Models
{
    public class UserListModel
    {
        public IEnumerable<User> Users { get; set; }

        public UserSearchModel Search { get; set; }

    }
}

public class UserSearchModel
{
    [Display(Name = "Name", Prompt = "e.g. Matt or Matthew")]
    public string Name { get; set; }

}