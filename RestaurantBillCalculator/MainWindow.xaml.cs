using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace RestaurantBillCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Menu> Beverages = new List<Menu>();
        List<Menu> Appetizers = new List<Menu>();
        List<Menu> MainCourses = new List<Menu>();
        List<Menu> Desserts = new List<Menu>();
        ObservableCollection<Menu> gridData = new ObservableCollection<Menu>();
        public double subTotal;
        public double tax;
        public double total;
        public MainWindow()
        {
            InitializeComponent();
            Beverages.Add(new Menu { Name = "Soda", Price = 1.95, Category = "Beverage" });
            Beverages.Add(new Menu { Name = "Tea", Price = 1.50, Category = "Beverage" });
            Beverages.Add(new Menu { Name = "Coffee", Price = 1.25, Category = "Beverage" });
            Beverages.Add(new Menu { Name = "Mineral Water", Price = 2.95, Category = "Beverage" });
            Beverages.Add(new Menu { Name = "Juice", Price = 2.50, Category = "Beverage" });
            Beverages.Add(new Menu { Name = "Milk", Price = 1.50, Category = "Beverage" });
            CBoxBeverage.ItemsSource = Beverages;

            Appetizers.Add(new Menu { Name = "Buffalo Wings", Price = 5.95, Category = "Appetizer" });
            Appetizers.Add(new Menu { Name = "Buffalo Fingers", Price = 6.95, Category = "Appetizer" });
            Appetizers.Add(new Menu { Name = "Potato Skins", Price = 8.95, Category = "Appetizer" });
            Appetizers.Add(new Menu { Name = "Nachos", Price = 8.95, Category = "Appetizer" });
            Appetizers.Add(new Menu { Name = "Mushroom Caps", Price = 10.95, Category = "Appetizer" });
            Appetizers.Add(new Menu { Name = "Shrimp Cocktail", Price = 12.95, Category = "Appetizer" });
            Appetizers.Add(new Menu { Name = "Chips and Salsa", Price = 6.95, Category = "Appetizer" });
            CBoxAppetizer.ItemsSource = Appetizers;

            Desserts.Add(new Menu { Name = "Apple Pie", Price = 5.95, Category = "Dessert" });
            Desserts.Add(new Menu { Name = "Sundae", Price = 3.95, Category = "Dessert" });
            Desserts.Add(new Menu { Name = "Carrot Cake", Price = 5.95, Category = "Dessert" });
            Desserts.Add(new Menu { Name = "Mud Pie", Price = 4.95, Category = "Dessert" });
            Desserts.Add(new Menu { Name = "Apple Crisp", Price = 5.95, Category = "Dessert" });
            CBoxDessert.ItemsSource = Desserts;

            MainCourses.Add(new Menu { Name = "Seafood Alfredo", Price = 13.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Chicken Alfredo", Price = 13.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Turkey Club", Price = 11.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Lobster Pie", Price = 19.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Prime Rib", Price = 20.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Shrimp Scampi", Price = 18.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Turkey Dinner", Price = 13.95, Category = "Main Course" });
            MainCourses.Add(new Menu { Name = "Stuffed Chicken", Price = 14.95, Category = "Main Course" });
            CBoxMainDish.ItemsSource = MainCourses;
        }
        public void AddMenuItem (Menu item)
        {
            int index;
            if (gridData.Contains(item))
            {
                index = gridData.IndexOf(item);
                gridData[index].quantity++;
                gridData.Remove(item);
                gridData.Insert(index, item);
                OrderList.ItemsSource = gridData;
                subTotal += gridData[index].Price;
                TBoxSubTotal.Text = $"{subTotal:c}";
                tax = subTotal * 0.13;
                TBoxTax.Text = $"{tax:c}";
                total = subTotal + tax;
                TBoxTotally.Text = $"{total:c}";
            }
            else
            {
                gridData.Add(item);
                index = gridData.IndexOf(item);
                gridData[index].quantity++;
                OrderList.ItemsSource = gridData;
                subTotal += gridData[index].Price;
                TBoxSubTotal.Text = $"{subTotal:c}";
                tax = subTotal * 0.13;
                TBoxTax.Text = $"{tax:c}";
                total = subTotal + tax;
                TBoxTotally.Text = $"{total:c}";
            }
        }
        public void RemoveMenuItem(Menu item)
        {
            int index = OrderList.SelectedIndex;
            subTotal -= item.Price * item.quantity;
            TBoxSubTotal.Text = $"{subTotal:c}";
            tax = subTotal * 0.13;
            TBoxTax.Text = $"{tax:c}";
            total = subTotal + tax;
            TBoxTotally.Text = $"{total:c}";
            item.quantity = 0;
            gridData.Remove(item);
        }

        private void picLink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.centennialcollege.ca/");
        }

        private void CBoxBeverage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBoxBeverage.SelectedItem != null)
            {
                Menu item = (Menu)CBoxBeverage.SelectedItem;
                AddMenuItem(item);
            }
        }

        private void CBoxAppetizer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBoxAppetizer.SelectedItem != null)
            {
                Menu item = (Menu)CBoxAppetizer.SelectedItem;
                AddMenuItem(item);
            }
        }

        private void CBoxDessert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBoxDessert.SelectedItem != null)
            {
                Menu item = (Menu)CBoxDessert.SelectedItem;
                AddMenuItem(item);
            }
        }

        private void CBoxMainDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBoxMainDish.SelectedItem != null)
            {
                Menu item = (Menu)CBoxMainDish.SelectedItem;
                AddMenuItem(item);
            }
        }

        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            subTotal = 0;
            foreach (Menu item in gridData)
            {
                double newPrice = item.Price * item.quantity;
                subTotal += newPrice;
            }
            TBoxSubTotal.Text = $"{subTotal:c}";
            tax = subTotal * 0.13;
            TBoxTax.Text = $"{tax:c}";
            total = subTotal + tax;
            TBoxTotally.Text = $"{total:c}";
        }
    }
}
