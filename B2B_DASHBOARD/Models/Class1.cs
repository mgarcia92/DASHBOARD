using System;

public class Class1
{

    
    public string comerce_name { get; set; }
    public string latitud { get; set; }
    public string longitud { get; set; }

    public Class1()
	{}


    public static List<Class1> Cordenadas(string Adivisor_code, string Date_active) {

        string query = string.Empty;
        string locaDate = DateTime.Now.ToString("yyyy-MM-dd");
        DBContextMSSQL db = new DBContextMSSQL();
        query = $@"select cusBusinessName as comercio, cusLatitude as latitud,cusLongitude as Longitud from dbo.customerRoute  cr
                INNER JOIN customer c ON cr.cusCode = c.cusCode
                where rotCode = '{Adivisor_code}'
                AND ctrVisitToday = '1'
                AND {Date_active}  != '0'
                order by {Date_active} Asc";

        DataTable table = db.ReadSql(query);

        if (table.Rows.Count > 0)
        {
            return table.ToList<Class1>();
        }

        return null;

    }
}
