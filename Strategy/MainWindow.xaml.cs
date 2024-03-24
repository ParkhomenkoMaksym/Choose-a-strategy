using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Strategy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal balance;
        decimal cost;

        decimal save = 0;

        int count = 0;
        int perCent;
        int perCentIndex;
        decimal temperaryBalance;

        decimal inputBalance;
        decimal inputCost;

        decimal keep;
        decimal more;

        decimal result;

        Random rnd = new Random();

        List<int> percentages;

        public MainWindow()
        {
            InitializeComponent();
            // infoBlock.Text = "";
            percentages = new List<int> {-10, -10, -10, -10, -10, -10, -10, -10, -10, -20 ,-20, -20, -20, -20,
                                            -30, -30, -30, -30, -40, -40, -50, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                                             20, 20, 20, 20, 20, 20, 20, 30, 30, 30, 30, 40, 40, 40, 50, 50, 60, 70};
            btnBuy.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnNext.Visibility = Visibility.Collapsed;
            txtBBuy.Visibility = Visibility.Collapsed;
            txtBSave.Visibility = Visibility.Collapsed;
            lblSave.Visibility = Visibility.Collapsed;
            lblBuy.Visibility = Visibility.Collapsed;
        }

        private void numberBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out decimal balance);
        }

        private void numberBalance_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (numberBalance.Text != "")
            {   
                balance = Convert.ToDecimal(numberBalance.Text);
                inputBalance = balance;
                lblError.Content = "";
            }
            else
            {
                balance = 0;
            }
            //txtBInputBal.Text = inputBalance.ToString();
        }

        private void numberCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out decimal cost);
        }
        private void numberCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (numberCost.Text != "")
            {
                cost = Convert.ToDecimal(numberCost.Text);
                inputCost = cost;
                lblError.Content = "";
            }
            else
            {
                cost = 0;
            }
        }
            
    //txtBCost.Text = inputCost.ToString();


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (balance == 0 || cost == 0)
            {
                lblError.Content = "You should have a balance and a price more than zero!";
            }
            else
            {
                CalculatePercent();
                //balanceTextBox.Visibility = Visibility.Collapsed;
                //costTextBox.Visibility = Visibility.Collapsed;
                btnStart.Visibility = Visibility.Collapsed;
                numberBalance.IsEnabled = false;
                numberCost.IsEnabled = false;

                btnBuy.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnNext.Visibility = Visibility.Visible;
                txtBBuy.Visibility = Visibility.Visible;
                txtBSave.Visibility = Visibility.Visible;

                lblSave.Visibility = Visibility.Visible;
                lblBuy.Visibility = Visibility.Visible;

                lblError.Content = "";
            }
        }

        public string ResultInfo()
        {
            result = balance - temperaryBalance;
            if (result > 0)
            {
                infoBlock.Text =
                    //$"Current per cent: +{perCent}%\n" +
                    $"Balance: ${Math.Round(balance, 2)} the difference: +{Math.Round(result, 2)}\n" +
                    $"Cost:    ${Math.Round(cost, 2)} \n" +
                    
                    $"Saved:   ${Math.Round(save, 2)}\n" +
                    $"Do you want to Save your profit, Buy more or proceed to the Next step?: ";
            }

            else if (result <= 0)
            {
                infoBlock.Text =
                    //$"Current per cent: {perCent}%\n" +
                    $"Balance: ${Math.Round(balance, 2)} difference: {Math.Round(result, 2)}\n" +
                    $"Cost:    ${Math.Round(cost, 2)} \n" +
                    
                    $"Saved:   ${Math.Round(save, 2)}\n" +
                    $"Do you want to Save your profit, Buy more or proceed to the Next step?: ";
            }
            else
            {
                lblError.Content = "Incorrect value!";
            }
            return infoBlock.Text;
        }

        public string PercentMessage()
        {
            //decimal result = balance - temperaryBalance;
            if (perCent > 0)
            {
                percentBlock.Text =
                    $"Current percentage: +{perCent}%";
                    //$"Current per cent: +{perCent}%\n" +
                    //$"Cost:    ${Math.Round(cost, 2)} \n" +
                    //$"Balance: ${Math.Round(balance, 2)} difference: +{Math.Round(result, 2)}\n" +
                    //$"Saved:   ${Math.Round(save, 2)}\n" +
                    //$"Do you want to Save your profit or Buy more or Pass?: ";
            }

            else if (perCent < 0)
            {
                percentBlock.Text =
                    $"Current percentage: {perCent}%";
                    //$"Current per cent: {perCent}%\n" +
                    //$"Cost:    ${Math.Round(cost, 2)} \n" +
                    //$"Balance: ${Math.Round(balance, 2)} difference: {Math.Round(result, 2)}\n" +
                    //$"Saved:   ${Math.Round(save, 2)}\n" +
                    //$"Do you want to Save your profit or Buy more or Pass?: ";
            }
            else
            {
                lblError.Content = "Incorrect value!";
            }
            return infoBlock.Text;
        }

        public void CalculatePercent()
        {
            if (count <= 47)
            {
                perCentIndex = rnd.Next(percentages.Count);
                perCent = percentages[perCentIndex];

                temperaryBalance = balance;

                    switch (perCent)
                    {
                        case -10:
                        case -20:
                        case -30:
                        case -40:
                        case -50:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        cost *= (1M + (perCent / 100M));
                            balance *= (1M + (perCent / 100M));

                            PercentMessage();
                            ResultInfo();


                            break;
                        default:
                            lblError.Content = "Incorrect percentage!";
                            break;
                    }
                
                count++;
                
                lblError.Content = "";
                percentages.RemoveAt(perCentIndex);
            }
            else if (percentages.Count <= 0)
            {
                btnStart.Visibility = Visibility.Visible;

                balance += save;
                save = 0;

                decimal percentageIncrease = ((Math.Round(balance, 2) - Math.Round(inputBalance, 2)) / Math.Round(inputBalance, 2)) * 100;

                infoBlock.Text = "This round has finished!\n" +
                                        $"Your balance:      ${Math.Round(balance, 2)} \n" +
                                        $"At the price:      ${Math.Round(cost, 2)} you have \n" +
                                        $"Saved:             ${Math.Round(save, 2)}\n" +

                                       $"Your first balance was: ${inputBalance}\n" + 
                                       $"At the price:           ${inputCost} \n" +

                                       $"Your profit is: {percentageIncrease:F2}%";

                btnBuy.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Collapsed;
                txtBBuy.Visibility = Visibility.Collapsed;
                txtBSave.Visibility = Visibility.Collapsed;

                numberBalance.IsEnabled = true;
                numberCost.IsEnabled = true;

                btnStart.Visibility = Visibility.Visible;
                count = 0;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtBSave.Text != "")
            {
                keep = Convert.ToDecimal(txtBSave.Text);
                
                balance -= keep;
                save += keep;
                CalculatePercent();
                txtBSave.Text = "";
                lblError.Content = "";
                keep = 0;
            }
            else
            {
                txtBSave.Text = "";
                keep = 0;
            }
            //if (txtBSave.Text != "")
            //{             
           

           // CalculatePercent();
            
            //}
            //else
            //{
                
            //    txtBSave.Text = "";
            //}

        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (txtBBuy.Text != "")
            {
                more = Convert.ToDecimal(txtBBuy.Text);
                
                save -= more;
                balance += more;

                CalculatePercent();
                txtBBuy.Text = "";
                lblError.Content = "";
            }
            else
            {
                txtBBuy.Text = "";
                more = 0;
            }
            //if (txtBBuy.Text != "")
            //{
            //save -= more;
            //    balance += more;

            ////CalculatePercent();
            //txtBBuy.Text = "";
            //    lblError.Content = "";
            
            //}
            //else
            //{ 
            //    txtBBuy.Text = "";
            //}
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            CalculatePercent();
            lblError.Content = "";
            txtBBuy.Text = "";
            txtBSave.Text = "";
        }

        private void txtBSave_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out decimal result);

        }

        private void txtBBuy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out decimal result);
        }

        private void txtBSave_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txtBSave.Text != "")
            //{
            //    keep = Convert.ToDecimal(txtBSave.Text);
            //    CalculatePercent();
            //}
            //else
            //{
            //    txtBSave.Text = "";
            //    keep = 0;
            //}
        }
        private void txtBBuy_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txtBBuy.Text != "")
            //{
            //    more = Convert.ToDecimal(txtBBuy.Text);
            //    CalculatePercent();
            //}
            //else
            //{
            //    txtBBuy.Text = "";
            //    more = 0;
            //}
        }


    }
}
