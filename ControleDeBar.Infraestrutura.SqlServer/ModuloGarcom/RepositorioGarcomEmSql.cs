using ControleDeBar.Dominio.ModuloGarcom;
using Microsoft.Data.SqlClient;

namespace ControleDeBar.Infraestrutura.SqlServer.ModuloGarcom
{
    public class RepositorioGarcomEmSql : IRepositorioGarcom
    {
        private readonly string _sql =
             "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ControleDeBarDb;Integrated Security=True";

        public void CadastrarRegistro(Garcom novoRegistro)
        {
            var sqlInserir =
                @"INSERT INTO [TBGARCOM]
            (
                [ID],
                [NOME],
                [CPF]
            )
            VALUES
            (
                @ID,
                @NOME,
                @CPF
            );";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlInserir, con);

            ConfigurarParametrosGarcom(novoRegistro, sqlCommand);

            con.Open();

            sqlCommand.ExecuteNonQuery();

            con.Close();
        }

        public bool EditarRegistro(Guid idRegistro, Garcom registroEditado)
        {
            var sqlEditar =
            @"UPDATE [TBGARCOM]	
		    SET
			    [NOME] = @NOME,
			    [CPF] = @CPF
		    WHERE
			    [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlEditar, con);

            registroEditado.Id = idRegistro;

            ConfigurarParametrosGarcom(registroEditado, sqlCommand);

            con.Open();

            var linhasAfetadas = sqlCommand.ExecuteNonQuery();

            con.Close();

            return linhasAfetadas > 0;
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            var sqlExcluir =
            @"DELETE FROM [TBGARCOM]
                WHERE
            [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlExcluir, con);

            sqlCommand.Parameters.AddWithValue("ID", idRegistro);

            con.Open();

            var linhasAfetadas = sqlCommand.ExecuteNonQuery();

            con.Close();

            return linhasAfetadas > 0;
        }

        public Garcom SelecionarRegistroPorId(Guid idRegistro)
        {
            var sqlSelecionarPorId =
            @"SELECT 
		        [ID], 
		        [NOME], 
		        [CPF]
	        FROM 
		        [TBGARCOM]
            WHERE
                [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlSelecionarPorId, con);

            sqlCommand.Parameters.AddWithValue("ID", idRegistro);

            con.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            Garcom? garcom = null;

            if (reader.Read())
                garcom = ConverterParaGarcom(reader);

            con.Close();

            return garcom;
        }

        public List<Garcom> SelecionarRegistros()
        {
            var sqlSelecionarTodos =
            @"SELECT 
		        [ID], 
		        [NOME], 
		        [CPF]
	        FROM 
		        [TBGARCOM]";

            SqlConnection con = new SqlConnection(_sql);

            con.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlSelecionarTodos, con);

            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            var garcons = new List<Garcom>();

            while (dataReader.Read())
            {
                var garcom = ConverterParaGarcom(dataReader);

                garcons.Add(garcom);
            }

            con.Close();

            return garcons;
        }

        private Garcom ConverterParaGarcom(SqlDataReader reader)
        {
            var garcom = new Garcom(
                Convert.ToString(reader["NOME"]),
                Convert.ToString(reader["CPF"])
                );

            garcom.Id = Guid.Parse(reader["ID"].ToString());

            return garcom;
        }

        private void ConfigurarParametrosGarcom(Garcom garcom, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", garcom.Id);
            sqlCommand.Parameters.AddWithValue("NOME", garcom.Nome);
            sqlCommand.Parameters.AddWithValue("CPF", garcom.Cpf);
        }
    }
}
