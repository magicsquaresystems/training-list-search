using SimpleList.WebUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.WebUI.Models
{
    public class OrderListModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public OrderSearchModel Search { get; set; }
    }

}
public class OrderSearchModel
{
    [Display(Name = "Search", Prompt = "Search by Product or Customer")]
    public string Name { get; set; } = "";
    public List<SelectListItem> SearchByList { get; set; }
    public string SearchByValue { get; set; } = "";
}

