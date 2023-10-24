using GreenSale.Desktop.Pages.Accunt;
using GreenSale.Desktop.Pages.CreateAd;
using GreenSale.Integrated.Interfaces.BuyerPosts;
using GreenSale.Integrated.Interfaces.SellerPosts;
using GreenSale.Integrated.Interfaces.Storages;
using GreenSale.Integrated.Interfaces.Users;
using GreenSale.Integrated.Services.BuyerPosts;
using GreenSale.Integrated.Services.SellerPosts;
using GreenSale.Integrated.Services.Storages;
using GreenSale.Integrated.Services.Users;
using GreenSale.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GreenSale.Desktop.Pages.Dashbord;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Dashboard : Page
{
    private ISellerPost _serviceSeller;
    private IBuyerPostService _serviceBuyer;
    private IStorageService _serviceStorage;
    private IUserService _serviseUser;
    public Dashboard()
    {
        InitializeComponent();
        this._serviceSeller = new SellerPostService();
        this._serviceBuyer = new BuyerPostService();
        this._serviceStorage = new StorageService();
        this._serviseUser = new UserService();
    }

    private void User_MouseDown(object sender, MouseButtonEventArgs e)
    {
        NavigationService?.Navigate(new UserAccaunt());
    }

    public async void Page_Loaded_Dashbord(object sender, RoutedEventArgs e)
    {
        long buoyed_count = 0;
        long new_post = 0;
        var result = await _serviseUser.GetAsync();
        user_name.Text = result.FirstName.ToString();
        var seller_count = await _serviceSeller.CountAsync();
        var buyer_count = await _serviceBuyer.CountAsync();
        var user_count = await _serviseUser.CountAsync();

        long count = seller_count + buyer_count;
        post_count.SubTitle = $"Bir yillik e'lonlar soni {count}";


        /* buoyed_count += seller_post.Count(seller => seller.status == 1);
         new_post += seller_post.Count(seller => seller.status == 0);*/


        /*buoyed_count += buyer_post.Count(buyer => buyer.status == 1);
        new_post += buyer_post.Count(buyer => buyer.status == 0);*/

        buoyed_count += await _serviceSeller.CountAgreedAsync();
        buoyed_count += await _serviceBuyer.CountAgreeAsync();

        new_post += await _serviceSeller.CountNewAsync();
        new_post += await _serviceBuyer.CountNewAsync();


        var res = CanCulateAsync(count, buoyed_count);
        post_buyed.SubTitle = $"Bir yillik kelishilgan e'lonlar soni {buoyed_count}";
        post_buyed.Amount = Convert.ToInt32(res);

        post_new.SubTitle = $"Yangi e'lonlar soni {new_post}";
        post_new.Amount = 100 - Convert.ToInt32(res);

        user_counthml.SubTitle = $"Foydalanuvchilar soni {user_count}";

        var storage_post = await _serviceStorage.StorageDaylilyCreatedAsync(7);
        var buyer_post = await _serviceBuyer.BuyerDaylilyCreatedAsync(7);
        var seller_post = await _serviceSeller.SellerDaylilyCreatedAsync(7);

        seller_post.Reverse();
        storage_post.Reverse();
        buyer_post.Reverse();

        for (int i = 0; i < 8; i++)
        {
            if (seller_post.Count < 8)
            {
                seller_post.Add(new PostCreatedAt { Count = 0, Kun = DateTime.Now });
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (buyer_post.Count < 8)
            {
                buyer_post.Add(new PostCreatedAt { Count = 0, Kun = DateTime.Now });
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (storage_post.Count < 8)
            {
                storage_post.Add(new PostCreatedAt { Count = 0, Kun = DateTime.Now });
            }
        }

        storage_post.Reverse();
        buyer_post.Reverse();
        seller_post.Reverse();

        chart(seller_post, buyer_post, storage_post);
        loader.Visibility = Visibility.Collapsed;
    }


    public long CanCulateAsync(long count, long statsus)
    {
        try
        {
            var x = (statsus * 100) / count;

            return x;
        }
        catch
        {
            return 0;
        }
        
    }

    private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
    {

    }


    public void chart(List<PostCreatedAt> sellerPosts, List<PostCreatedAt> buyer_post, List<PostCreatedAt> storages)
    {
        DateTime now = DateTime.Now;
        List<Double> item = new List<Double>();
        seller_post_dg.Values.Clear();
        buyer_post_dg.Values.Clear();
        storage_post_dg.Values.Clear();
        
        for(int i = 0;i < sellerPosts.Count;i++)
        {
            seller_post_dg.Values.Add(Convert.ToDouble(sellerPosts[i].Count));
            /*if (Convert.ToInt32(sellerPosts[i].Kun.ToString("dd")) == (Convert.ToInt32(now.ToString("dd"))) - i)
            {
                seller_post_dg.Values.Add(Convert.ToDouble(sellerPosts[i].Count));
            }
            else
            {
                seller_post_dg.Values.Add(Convert.ToDouble(0));
            }*/
        }

        for (int i = 0; i < buyer_post.Count; i++)
        {
            buyer_post_dg.Values.Add(Convert.ToDouble(buyer_post[i].Count));
        }

        for (int i = 0; i < storages.Count; i++)
        {
            storage_post_dg.Values.Add(Convert.ToDouble(storages[i].Count));
        }

        /*foreach (var pst in sellerPosts)
        {
            
            if (Convert.ToInt32(pst.Kun.ToString("dd")) == (Convert.ToInt32(now.ToString("dd"))) - i)
            {
                seller_post_dg.Values.Add(Convert.ToDouble(pst.Count));
            }
            else
            {
                seller_post_dg.Values.Add(Convert.ToDouble(0));
            }
            i++;
        }
        i = 0;
        foreach (var pst in buyer_post)
        {
            
            if (Convert.ToInt32(pst.Kun.ToString("dd")) == (Convert.ToInt32(now.ToString("dd"))) - i)
            {
                buyer_post_dg.Values.Add(Convert.ToDouble(pst.Count));
            }
            else
            {
                buyer_post_dg.Values.Add(Convert.ToDouble(0));
            }
            i++;
        }
        i = 0;
        foreach (var pst in storages)
        {
            
            if (Convert.ToInt32(pst.Kun.ToString("dd")) == (Convert.ToInt32(now.ToString("dd"))) - i)
            {
                storage_post_dg.Values.Add(Convert.ToDouble(pst.Count));
            }
            else
            {
                storage_post_dg.Values.Add(Convert.ToDouble(0));
            }
            i++;
        }*/

        /*for (int i = 1; i < 7; i++)
        {
            var day = sellerPosts.Count(item => item.createdAt.ToString("dd") == (Convert.ToInt32(now.ToString("dd")) - i).ToString());
            item.Add(day);
            seller_post_dg.Values.Add(Convert.ToDouble(day));
            buyer_post_dg.Values.Add(Convert.ToDouble(day));
            storage_post_dg.Values.Add(Convert.ToDouble(day));
        }*/




    }

}
