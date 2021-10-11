﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : ContentPage
    {
        public SignUpView()
        {
            InitializeComponent();
            
        }
        private async void BackButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void SignUpButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainListView());
        }
    }
}