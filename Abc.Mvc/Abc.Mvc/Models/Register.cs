using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Adınız")]
        public string Name  { get; set; }

        [Required]
        [DisplayName("Soyadınız")]
        public string SurName  { get; set; }

        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage ="Eposta adresinizi düzgün giriniz.")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor.")]
        public string RePassword { get; set; }
    }
}