using Microsoft.Data.SqlClient;
using System.Data;

namespace DBMS.SQL.QueryOrders
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //ประกาศตัวแปร connect
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void connectDB()   //ใช้เปิดการเชื่อมต่อฐานข้อมูล
        {
            string server = @".\sqlexpress"; // ไปcopy ชื่อ server มาจาก SqlServer
            string db = "northwind";
            string strCon = string.Format(@"Data Source={0};Initial Catalog={1};"
                            + "Integrated Security=True;Encrypt=False", server, db);
            conn = new SqlConnection(strCon);
            conn.Open();
        }
        private void disconnectDB()
        {
            conn.Close();
        }

        private void showdata(string sql, DataGridView dgv)  //ส่ง sql และ dgv ที่แสดงผล
        {
            da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv.DataSource = ds.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectDB();
            //ไปเอา SQL ที่ทดสอบแล้วมาใส่ตรง sqlQuery
            //string sqlQuery = "Select o.OrderID, Format(o.OrderDate,'dd-MMM-yyyy') as Order_Date , "
            //                 + " Format(o.ShippedDate,'dd-MMM-yyyy') as Shipped_Date, "
            //                 + " sh.CompanyName, TitleOfCourtesy+firstname + ' ' + LastName EmployeeName, "
            //                 + " c.CompanyName, c.Phone,"
            //                 + " Convert(decimal(10,2),sum(od.Quantity * od.UnitPrice *(1-od.Discount))) TotalCash"
            //                 + " from orders o join [Order Details] od on o.OrderID = od.OrderID"
            //                 + " join Shippers sh on sh.ShipperID = o.ShipVia"
            //                 + " join Employees e on o.EmployeeID = e.EmployeeID"
            //                 + " join Customers c on o.CustomerID = c.CustomerID"
            //                 + " group by o.OrderID, o.OrderDate, o.ShippedDate, sh.CompanyName, "
            //                 + " TitleOfCourtesy+firstname + ' ' + LastName, c.CompanyName, c.Phone";
            string sqlQuery = "select * from OrderList";
            showdata(sqlQuery, dgvOrders);

        }

        private void dgvOrders_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex!= -1)
            {
                int id = Convert.ToInt32(dgvOrders.CurrentRow.Cells[0].Value);
                //MessageBox.Show(id.ToString(), "รหัสที่เลือก");
                // ไป copy sql คำสั่งที่ 2 มาใส่
                //string sqlQuery = "select p.ProductID, p.ProductName, od.Quantity, od.UnitPrice, od.Discount, "
                //    +" od.Quantity* od.UnitPrice ยอดเต็ม, od.Quantity* od.UnitPrice * od.Discount ส่วนลด,"
                //    +" od.Quantity * od.UnitPrice *(1-od.Discount) ยอดหักส่วนลดแล้ว"
                //    +" from orders o join [Order Details] od on o.OrderID = od.OrderID"
                //    +" join products p on od.ProductID = p.ProductID"
                //    +" where o.orderID = @id";
                string sqlQuery = "Select * from DetailsList where OrderID =  @id";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvDetails.DataSource = ds.Tables[0];
            }
        }
    }
}
