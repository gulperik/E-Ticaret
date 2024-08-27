using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Entity
{
    public enum EnumOrderState
    {
        //Kullanıcıya kargo hakkında verilcek bilgiler

        [Display(Name = "Onay Bekleniyor")]
        Waiting,
        [Display(Name = "Tamamlandı")]
        Completed,









    }
}