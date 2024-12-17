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
        //��С�ȵ���� connect
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void connectDB()   //���Դ����������Ͱҹ������
        {
            string server = @".\sqlexpress"; // �copy ���� server �Ҩҡ SqlServer
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

        private void showdata(string sql, DataGridView dgv)  //�� sql ��� dgv ����ʴ���
        {
            da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv.DataSource = ds.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectDB();
            //���� SQL ��跴�ͺ���������ç sqlQuery
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
                //MessageBox.Show(id.ToString(), "���ʷ�����͡");
                // � copy sql ����觷�� 2 �����
                //string sqlQuery = "select p.ProductID, p.ProductName, od.Quantity, od.UnitPrice, od.Discount, "
                //    +" od.Quantity* od.UnitPrice �ʹ���, od.Quantity* od.UnitPrice * od.Discount ��ǹŴ,"
                //    +" od.Quantity * od.UnitPrice *(1-od.Discount) �ʹ�ѡ��ǹŴ����"
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
