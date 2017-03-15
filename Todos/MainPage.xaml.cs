using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            System.Nullable<bool> a;

            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
            this.ViewModel = new ViewModels.TodoItemViewModel();
        }

        ViewModels.TodoItemViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == typeof(ViewModels.TodoItemViewModel))
            {
                this.ViewModel = (ViewModels.TodoItemViewModel)(e.Parameter);
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
             AppViewBackButtonVisibility.Collapsed;
        }

        private void TodoItem_ItemClicked(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedItem = (Models.TodoItem)(e.ClickedItem);
            if (InlineToDoItemViewGrid.Visibility == Visibility.Collapsed)
            {
                Frame.Navigate(typeof(NewPage), ViewModel);
            }
            else
            {
                title.Text = ViewModel.SelectedItem.title;
                Details.Text = ViewModel.SelectedItem.description;
                DateShower.Date = ViewModel.SelectedItem.date;
                OverviewImg.Source = ViewModel.SelectedItem.image;
                createButton.Content = "update";
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {

            if (InlineToDoItemViewGrid.Visibility == Visibility.Collapsed)
            {
                ViewModel.SelectedItem = null;
                Frame.Navigate(typeof(NewPage), ViewModel);
            }
           
        }

        private void Onchecked(object sender, RoutedEventArgs e)
        {
            //
            //
        }

        private void Onunchecked(object sender, RoutedEventArgs e)
        {
            //this.ViewModel.SelectedItem.completed = false;
        }
        /*
        private void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (CanCreate(sender, e))
            {
                if (createButton.Content.ToString() == "Create")
                    ViewModel.AddTodoItem(title.Text, Details.Text);
                else
                {
                    //ViewModel.UpdateTodoItem(title.Text, Details.Text);
                    ViewModel.SelectedItem.title = title.Text;
                    ViewModel.SelectedItem.description = Details.Text;
                    ViewModel.SelectedItem.date = DateShower.Date.Date;
                }
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }
        */
        private bool CanCreate(object sender, RoutedEventArgs e)
        {
            {
                if (DateShower.Date.AddHours(1) < System.DateTime.Now)
                {
                    new MessageDialog("设定时间必须大于等于当前时间！").ShowAsync();
                    return false;
                }
                if (title.Text == "")
                {
                    new MessageDialog("必须设置一个标题！").ShowAsync();
                    return false;
                }
                if (Details.Text == "")
                {
                    new MessageDialog("必须在Detail项填入内容！").ShowAsync();
                    return false;
                }
                return true;
            }

        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            Details.Text = "";
            DateShower.Date = System.DateTime.Now;
        }

        private void CreateOrUpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (CanCreate(sender, e))
            {
                if (createButton.Content.ToString() == "Create") CreateButton_Clicked(sender, e);
                else UpdateButton_Clicked(sender, e);
               /* Frame.Navigate(typeof(MainPage), ViewModel);*/
            }
        }
        private void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTodoItem(title.Text, Details.Text, DateShower.Date.Date);
            ViewModel.SelectedItem = null;

        }
        private void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.UpdateTodoItem(ViewModel.SelectedItem.Getid(), title.Text, Details.Text, DateShower.Date.Date);
            }
        }




    }
}
