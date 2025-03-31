using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

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

    // Màu sắc theme
    private Color primaryColor = Color.FromArgb(51, 102, 153);  // Xanh dương đậm
    private Color secondaryColor = Color.FromArgb(240, 240, 240); // Xám nhạt
    private Color accentColor = Color.FromArgb(255, 128, 0);     // Cam nhấn

    public MainForm()
    {
        // Cấu hình form chính
        this.Text = "Quản Lý Bán Hàng - Phiên bản Nâng cao";
        this.Size = new Size(900, 550);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = secondaryColor;
        this.Font = new Font("Segoe UI", 9, FontStyle.Regular);

        // Tạo panel header
        var headerPanel = new Panel { 
            Dock = DockStyle.Top, 
            Height = 50,
            BackColor = primaryColor
        };
        
        var headerLabel = new Label {
            Text = "QUẢN LÝ BÁN HÀNG",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        headerPanel.Controls.Add(headerLabel);

        // Panel nhập liệu
        var inputPanel = new Panel { 
            Dock = DockStyle.Top, 
            Height = 120,
            BackColor = Color.White,
            Padding = new Padding(10)
        };
        
            var lblCustomer = new Label { 
            Text = "Tên KH:", 
            Location = new Point(20, 15),
            ForeColor = Color.Black,  // Đổi từ Color.FromArgb(64,64,64) sang Color.Black
            Font = new Font("Segoe UI", 9, FontStyle.Regular)
        };
        
        var txtCustomer = new TextBox { 
            Location = new Point(120, 12), 
            Width = 200,
            BorderStyle = BorderStyle.FixedSingle
        };
        
        var lblProduct = new Label { 
            Text = "Mặt hàng:", 
            Location = new Point(20, 45),
            ForeColor = Color.FromArgb(64, 64, 64)
        };
        
        var txtProduct = new TextBox { 
            Location = new Point(120, 42), 
            Width = 200,
            BorderStyle = BorderStyle.FixedSingle
        };
        
        var lblPrice = new Label { 
            Text = "Giá tiền:", 
            Location = new Point(20, 75),
            ForeColor = Color.FromArgb(64, 64, 64)
        };
        
        var txtPrice = new TextBox { 
            Location = new Point(120, 72), 
            Width = 200,
            BorderStyle = BorderStyle.FixedSingle
        };
        
        var btnAdd = new Button { 
            Text = "THÊM GIAO DỊCH", 
            Location = new Point(350, 40),
            Width = 150,
            Height = 30,
            BackColor = accentColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9, FontStyle.Bold)
        };
        btnAdd.FlatAppearance.BorderSize = 0;

        // DataGridView với style đẹp
        dataGridView = new DataGridView {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = secondaryColor,
            BorderStyle = BorderStyle.None,
            EnableHeadersVisualStyles = false
        };

        // Thiết lập style cho DataGridView
        dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle {
            BackColor = primaryColor,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9, FontStyle.Bold)
        };
        
        dataGridView.RowHeadersVisible = false;
        dataGridView.DefaultCellStyle.BackColor = Color.White;
        dataGridView.DefaultCellStyle.ForeColor = Color.Black;
        dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightGray;
        dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

        // Thêm cột
        dataGridView.Columns.Add("Customer", "TÊN KHÁCH HÀNG");
        dataGridView.Columns.Add("Product", "MẶT HÀNG");
        dataGridView.Columns.Add("Price", "GIÁ TIỀN (VND)");
        dataGridView.Columns.Add("Date", "NGÀY MUA");
        
        // Định dạng cột
        dataGridView.Columns["Price"].DefaultCellStyle.Format = "N0";
        dataGridView.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridView.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

        // Sự kiện thêm giao dịch (giữ nguyên logic cũ)
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
                MessageBox.Show("Vui lòng nhập giá tiền hợp lệ!", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        };

        // Thêm controls vào input panel
        inputPanel.Controls.AddRange(new Control[] {
            lblCustomer, txtCustomer,
            lblProduct, txtProduct,
            lblPrice, txtPrice,
            btnAdd
        });

        // Thêm controls vào form
        this.Controls.Add(dataGridView);
        this.Controls.Add(inputPanel);
        this.Controls.Add(headerPanel);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}