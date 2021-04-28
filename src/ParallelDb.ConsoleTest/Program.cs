using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ParallelDb.Data;
using ParallelDb.Data.Entities;

namespace ParallelDb.ConsoleTest
{
    class Program
    {
        static void Main()
        {
            Random r = new(DateTime.Now.Millisecond);
            
            var db = new DataContext();
            //очищаем таблицу
            db.TruncateAsync();

            for (int i = 0; i < 100; i++)
                db.Elements.Add(Element.CreateInstance(r));
            db.SaveChanges();

            var list = db.Elements.Select(e => e.Id).ToList();
            using (SqlConnection conn = db.Database.GetDbConnection() as SqlConnection)
            {
                string de = nameof(DepElement) + 's',
                    eid = nameof(DepElement.ElementId),
                    n = nameof(DepElement.Name),
                    nu = nameof(DepElement.Number);
                DataTable depElements = new(de);

                var column = new DataColumn {DataType = typeof(int), ColumnName = "Id"};
                depElements.Columns.Add(column);
                column = new DataColumn {DataType = typeof(int), ColumnName = eid, AllowDBNull = true};
                depElements.Columns.Add(column);
                column = new DataColumn {DataType = typeof(string), ColumnName = n, AllowDBNull = true};
                depElements.Columns.Add(column);
                column = new DataColumn {DataType = typeof(int), ColumnName = nu, AllowDBNull = true};
                depElements.Columns.Add(column);

                int id = 0;
                if(db.DepElements.Any())
                    id = db.DepElements.AsQueryable().Max(d => d.Id);

                for (int j = 0; j < 10; j++)
                {
                    for (int i = 1; i < 100001; i++)
                    {
                        DepElement item = DepElement.CreateInstance(r, list);
                        var row = depElements.NewRow();
                        id += i;
                        row["Id"] = id;
                        row[eid] = (object)item.ElementId ?? DBNull.Value;
                        row[n] = (object)item.Name ?? DBNull.Value;
                        row[nu] = (object)item.Number ?? DBNull.Value;
                        depElements.Rows.Add(row);
                    }
                    SqlBulkCopy bulkCopy =
                        new
                        (
                            conn,
                            SqlBulkCopyOptions.TableLock |
                            SqlBulkCopyOptions.FireTriggers |
                            SqlBulkCopyOptions.UseInternalTransaction,
                            null
                        ) {DestinationTableName = depElements.TableName};
                    conn?.Open();
                    bulkCopy.WriteToServer(depElements);
                    conn?.Close();
                }
            }
            WriteLine("OK");
            ReadKey();
        }
    }
}
