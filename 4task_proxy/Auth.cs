﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace vp4_proxy
{
    public class Auth
    {
        private List<User> users = new List<User>();
        static public User LoggedIn;

        public Auth()
        {
            RegisterAdmin();
        }
        private void RegisterAdmin()
        {
            string filePath = "C:\\Users\\Professional\\source\\repos\\vp4_proxy\\vp4_proxy\\Admin.json";
            string jsonString = File.ReadAllText(filePath);
            User admin = JsonSerializer.Deserialize<User>(jsonString);
            users.Add(admin);
        }
        public void Register(User user)
        {
            var alreadyRegistred = users.Find(x => x.email == user.email);
            if (alreadyRegistred == null) users.Add(user);
            else throw new Exception("User with such email is alredy registred");
        }
        public User LogIn(string email, string password) 
        {
            var registredUser = users.Find(x => x.email == email);
            if (registredUser != null && registredUser.password == password) LoggedIn = registredUser;
            else throw new Exception("User not found.");
            return registredUser;
        }
        public void LogOut()
        {
            if (LoggedIn != null) LoggedIn = null;
            else throw new Exception("LogIn before.");
        }
    }
}
