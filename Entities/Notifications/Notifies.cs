﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }
        
        [NotMapped]
        public string NameProp { get; set; }

        [NotMapped]
        public string mensage { get; set; }

        [NotMapped]
        public List<Notifies> Notifications;

        public bool ValidateStringProperty(string value, string nameProperty) 
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    mensage = "Required field!",
                    NameProp = nameProperty
                });
                return false;
            }  
            return true;
        }

        public bool ValidateIntProperty(int value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    mensage = "The value must be greater than 0!",
                    NameProp = nameProperty
                });
                return false;
            }
            return true;
        }

        public bool ValidateDecimalProperty(decimal value, string nameProperty)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(nameProperty))
            {
                Notifications.Add(new Notifies
                {
                    mensage = "The value must be greater than 0!",
                    NameProp = nameProperty
                });
                return false;
            }
            return true;
        }

    }
}
