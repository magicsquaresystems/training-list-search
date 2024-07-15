﻿using SimpleList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleList.WebUI.Models
{
    public class UserListModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public UserSearchModel Search { get; set; }
    }

    public class UserSearchModel
    {
        [Display(Name = "Name", Prompt = "e.g. Matt or Matthew")]
        public string Name { get; set; }
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string Brand { get; set; }
        public string ProductDescription { get; set; }
        public decimal Cost { get; set; }
    }
}