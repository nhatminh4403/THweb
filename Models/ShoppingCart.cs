﻿using Microsoft.AspNetCore.Identity;

namespace WebBanHang_Lab3.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity = existingItem.Quantity + item.Quantity;
            }
            else
            {
                Items.Add(item);
            }   
        }
		public void IncreaseQuantity(int productId)
		{
			var item = Items.FirstOrDefault(i => i.ProductId == productId);
			if (item != null)
			{
				item.Quantity++;
			}
		}
		public void DecreaseQuantity(int productId)
		{
			var item = Items.FirstOrDefault(i => i.ProductId == productId);
			if (item != null && item.Quantity > 1)
			{
				item.Quantity--;
			}
		}
		public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }
    }
}
