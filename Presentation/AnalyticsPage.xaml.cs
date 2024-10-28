﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL.Data;
using DAL.Models;
using BusinessLogic.Session;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Definitions.Series;
using BusinessLogic.Services;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AnalyticsPage.xaml
    /// </summary>
    public partial class AnalyticsPage : Page
    {
        private ExpenseService expenseService;
        private ExpenseCategoryService expenseCategoryService;

        public AnalyticsPage()
        {
            InitializeComponent();
            expenseService = new ExpenseService();
            expenseCategoryService = new ExpenseCategoryService();
            UpdatePieChart();
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {

        }


        private void UpdatePieChart()
        {
            var categories = expenseCategoryService.GetCategories();
            var pieSeriesCollection = new SeriesCollection();
            var legendItems = new List<dynamic>();


            var colors = new List<SolidColorBrush>
            {
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#2D4E6C")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#3C6890")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#3274B6")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#4A82B4")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#5995D2")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#8DB6E0")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#6E9BC3")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#86ACCD")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#AECCE9")),
                (SolidColorBrush)(new BrushConverter().ConvertFrom("#BDD5ED"))
            };

            if (categories.Count == 0)
            {
                pieSeriesCollection.Add(new PieSeries
                {
                    Title = "Немає даних",
                    Values = new ChartValues<double> { 1 }, 
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#BDD5ED"))
                });

                legendItems.Add(new
                {
                    Title = "Немає даних",
                    Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#BDD5ED"))
                });
            }
            else
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    var category = categories[i];
                    var color = colors[i % colors.Count];

                    pieSeriesCollection.Add(new PieSeries
                    {
                        Title = category.Name,
                        Values = new ChartValues<double> { expenseService.GetTotalExpensesByCategoryId(category.Id) },
                        Fill = color
                    });

                    legendItems.Add(new
                    {
                        Title = category.Name,
                        Color = color
                    });
                }
            }

            AnalyticsPieChart.Series = pieSeriesCollection;
            AnalyticsLegend.ItemsSource = legendItems;
        }
    }
}
