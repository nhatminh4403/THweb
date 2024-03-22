using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebBanHang_Lab3.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên danh mục không được vượt quá 50 ký tự")]
        [DisplayName("Tên danh mục")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
