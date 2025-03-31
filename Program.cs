using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace SalesManager;

public class SaleRecord
{
    public string CustomerName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
}

public partial class MainForm : Form
{
    private List<SaleRecord> sales = new List<SaleRecord>();
    private DataGridView dataGridView;

    public MainForm()
    {
        // Cấu hình form chính
        this.Text = "Quản Lý Bán Hàng";
        this.Size = new Size(800, 500);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Tạo controls
        var panel = new Panel { Dock = DockStyle.Top, Height = 120 };
        
        var lblCustomer = new Label { Text = "Tên KH:", Location = new Point(10, 15) };
        var txtCustomer = new TextBox { Location = new Point(100, 12), Width = 150 };
        
        var lblProduct = new Label { Text = "Mặt hàng:", Location = new Point(10, 45) };
        var txtProduct = new TextBox { Location = new Point(100, 42), Width = 150 };
        
        var lblPrice = new Label { Text = "Giá tiền:", Location = new Point(10, 75) };
        var txtPrice = new TextBox { Location = new Point(100, 72), Width = 150 };
        
        var btnAdd = new Button { 
            Text = "Thêm giao dịch", 
            Location = new Point(270, 40),
            Width = 120
        };

        dataGridView = new DataGridView {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };

        // Thiết lập DataGridView
        dataGridView.Columns.Add("Customer", "Tên KH");
        dataGridView.Columns.Add("Product", "Mặt hàng");
        dataGridView.Columns.Add("Price", "Giá tiền");
        dataGridView.Columns.Add("Date", "Ngày mua");
        dataGridView.Columns["Price"].DefaultCellStyle.Format = "N0";
        dataGridView.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

        // Sự kiện thêm giao dịch
        btnAdd.Click += (s, e) => {
            if (decimal.TryParse(txtPrice.Text, out decimal price))
            {
                var record = new SaleRecord {
                    CustomerName = txtCustomer.Text,
                    ProductName = txtProduct.Text,
                    Price = price,
                    PurchaseDate = DateTime.Now
                };
                
                sales.Add(record);
                dataGridView.Rows.Add(
                    record.CustomerName,
                    record.ProductName,
                    record.Price,
                    record.PurchaseDate
                );
                
                // Clear các ô nhập
                txtCustomer.Clear();
                txtProduct.Clear();
                txtPrice.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá tiền hợp lệ!");
            }
        };

        // Thêm controls vào panel
        panel.Controls.AddRange(new Control[] {
            lblCustomer, txtCustomer,
            lblProduct, txtProduct,
            lblPrice, txtPrice,
            btnAdd
        });

        // Thêm controls vào form
        this.Controls.Add(dataGridView);
        this.Controls.Add(panel);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}