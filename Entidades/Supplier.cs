using SQLite;
using System;

[Table("Supplier")]
public class Supplier
{
    [PrimaryKey, AutoIncrement, Column("IdSupplier")]
    public int IdSupplier { get; set; }

    [Indexed]
    public string ASL_Supplier_Name { get; set; }
    public string SL_Division_Code { get; set; }
    public string SL_Business_Line_Code { get; set; }
    public string SL_Business_Line_Desc { get; set; }
    public string Supplier_Manager_Name { get; set; }
    public string Supplier_Management_Model { get; set; }
    [Indexed]
    public string PO_Number { get; set; }
    public string PO_Created_Date { get; set; }
    public int PO_Line_Number { get; set; }
    public string ASL_Category { get; set; }
    public string ASL_Sub_Category { get; set; }
    public string Expense_Type { get; set; }
    public string Spend_Source { get; set; }
    [Indexed]
    public string YearMonth { get; set; }
    [Indexed]
    public string ASL_Supplier_Number { get; set; }
    public string Supplier_Country_Name { get; set; }
    public string Plant_code { get; set; }
    public string Supplier_Entity_Code { get; set; }
    public string Supplier_Entity_Name { get; set; }
    public string Simulated_NIS_Line_Desc { get; set; }
    public string Buying_Country_Code { get; set; }
    public string Buying_Country_Name { get; set; }
    public string SL_Ultimate_Basin_Code { get; set; }
    public string SL_Ultimate_Geounit_Code { get; set; }
    public string SL_Ultimate_Division_Code { get; set; }
    public string SL_Ultimate_Business_Line_Code { get; set; }
    public string SL_Ultimate_Business_Line_Desc { get; set; }
    public string Watch_List_Name { get; set; }
    public string Payment_Terms { get; set; }
    public decimal Spend_PO_USD { get; set; }
    public decimal Spend_Non_PO_USD { get; set; }
    public decimal Spend_USD { get; set; }
    public int DistinctCountInvoice_Number { get; set; }

}