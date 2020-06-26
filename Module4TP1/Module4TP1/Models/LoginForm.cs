﻿using Module4TP1.Entities;
using Module4TP1.Services;
using Module4TP1.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Module4TP1.Models
{
    public class LoginForm
    {
            private readonly ITwitterService twitterService;
            private readonly Entry login;
            private readonly Entry password;
            private readonly Xamarin.Forms.Switch isRemind;
            private readonly ErrorForm error;
            private User user;
            private INavigation navigation;
            


        public LoginForm(Entry login, Entry password, Xamarin.Forms.Switch isRemind, Label errorLabel, Button button, INavigation navigation)
            {
                this.twitterService = new TwitterService();
                this.navigation = navigation;
                this.login = login;
                this.password = password;
                this.isRemind = isRemind;
                this.error = new ErrorForm(errorLabel);
                button.Clicked += Button_Clicked;
                
    }

        
        private void Button_Clicked(object sender, EventArgs e)
            {
                Debug.WriteLine("btn clicked");

                var current = Connectivity.NetworkAccess;
                Debug.WriteLine("Internet : " + current);

                this.error.Hide();
                
                    if (this.IsValid())
                    {
                        if (twitterService.Authenticate(this.user))
                        {
                            this.error.Hide();
                            this.navigation.PushAsync(new TweetsPage());
                        }
                        else
                        {
                            this.error.Error = "Utilisateur non trouvé";
                            this.error.Display();
                        }
                    }
                    else
                    {
                        this.error.Display();
                    }
            }

        public Boolean IsValid()
            {
                Boolean result = true;

                User user = new User();
                user.Login = login.Text;
                user.Password = password.Text;
                Boolean isRemind = this.isRemind.IsToggled;

                bool haveError = false;
                StringBuilder stringBuilder = new StringBuilder();

                if (String.IsNullOrEmpty(user.Login) || user.Login.Length < 3)
                {
                    haveError = true;
                    stringBuilder.Append("L'identifiant ne peut pas être null et doit posséder au moins 3 caractères.");
                }

                if (String.IsNullOrEmpty(user.Password) || user.Password.Length < 6)
                {
                    if (haveError)
                    {
                        stringBuilder.Append("\n");
                    }
                    haveError = true;
                    stringBuilder.Append("Le mot de passe ne peut pas être null et doit posséder au moins 6 caractères.");
                }

                if (haveError)
                {
                    this.error.Error = stringBuilder.ToString();
                }

                result = !haveError;
                this.user = user;

                return result;
            }
        }
}
