﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webbanhang.Context
{
    using DocumentFormat.OpenXml.Wordprocessing;
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Họ")]
        public string FirstName { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Tên")]
        public string LastName { get; set; }
      
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Quản trị viên")]

        public Nullable<bool> IsAdmin { get; set; }
    }
}