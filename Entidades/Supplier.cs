using SQLite;
using System;

[Table("Supplier")]
public class Supplier
{
    [PrimaryKey, AutoIncrement, Column("IdSupplier")]
    public int IdSupplier { get; set; }

    [Indexed]
    public string ASL_Supplier_Name { get; set; }

    public string Supplier_Manager_Name { get; set; }

    public string Supplier_Management_Model { get; set; }

    [Indexed]
    public string PO_Number { get; set; }

    public string PO_Created_Date { get; set; }

    public string PO_Type { get; set; }

    public int PO_Line_Number { get; set; }

    public string ASL_Category { get; set; }

    public string ASL_Sub_Category { get; set; }

    public string Expense_Type { get; set; }

    public string Spend_Source { get; set; }

    // Índice para consultas por mes
    [Indexed]
    public string YearMonth { get; set; }

    // Índice para consultas por proveedor
    [Indexed]
    public string ASL_Supplier_Number { get; set; }

    [Indexed]
    public string Supplier_Country { get; set; }

    public string Item_Code { get; set; }

    public string Item_Description { get; set; }

    [Indexed]
    public string Plant { get; set; }

    // Índice para búsquedas por factura
    [Indexed]
    public string Invoice_Number { get; set; }

    // Índice para reportes por fecha
    [Indexed]
    public DateTime Spend_Date { get; set; }

    public string Supplier_Entity_Code { get; set; }

    public string Supplier_Entity_Name { get; set; }

    public string Buying_Country_Code { get; set; }

    public string Buying_Country_Name { get; set; }

    public string SL_Ultimate_Basin { get; set; }

    public string SL_Ultimate_Geounit { get; set; }

    public string SL_Ultimate_Division { get; set; }

    public string SL_Ultimate_Business_Line { get; set; }

    public string Hybrid_Category { get; set; }

    public string Hybrid_Sub_Category { get; set; }

    public string Hybrid_Family_Desc { get; set; }

    public string Hybrid_Commodity_Code { get; set; }

    public string Payment_Terms { get; set; }

    public string Invoice_Currency { get; set; }

    // Recomendado usar decimal para cálculos
    public decimal Spend_PO_USD { get; set; }

    public decimal Spend_Non_PO_USD { get; set; }

    public decimal Spend_USD { get; set; }
}