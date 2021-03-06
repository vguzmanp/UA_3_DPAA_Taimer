﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Collections;


namespace CAD
{
    public class CADComentario
    {
        private static string conexionTBD;
        
        public CADComentario()
        {
            conexionTBD = Conection.Conect.ConectionString;
            // Adquiere la cadena de conexión desde un único sitio

        }
        /// <summary>
        /// Creamos un comentario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="codActividad"></param>
        /// <param name="codUser"></param>
        public void CrearCommentBasic(string text, int codActividad, string codUser, DateTime date)
        {
            string comando;

            if (codUser == null) {
                comando = "INSERT INTO [Comentario](texto,actividad,fecha) VALUES('" + text + "', '" + codActividad + "', '" + date+ "')";
            } else {
                comando = "INSERT INTO [Comentario](texto,actividad,usuario,fecha) VALUES('" + text + "', '" + codActividad + "', '" + codUser + "', '" + date+ "')";
            }    
            SqlConnection c = null;
            SqlCommand comandoTBD;
            
            try
            {
                c = new SqlConnection(conexionTBD);
                comandoTBD = new SqlCommand(comando, c);
                c.Open();
                comandoTBD.CommandType = CommandType.Text;
                comandoTBD.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (c != null) c.Close(); // Se asegura de cerrar la conexión.
            }
        }
        /// <summary>
        /// Borramos un coment
        /// </summary>
        /// <param name="id"></param>
        public void BorrarComment(int id)
        {
            SqlConnection c = null;
            string comando = "DELETE FROM [Comentario] WHERE id= '" + id + "'";

            try
            {
                c = new SqlConnection(conexionTBD);
                c.Open();
                SqlCommand cmd = new SqlCommand(comando, c);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (c != null) c.Close(); // Se asegura de cerrar la conexión.
            }
        }

        /// <summary>
        /// Devolvemos la lista de comentarios
        /// </summary>
        /// <returns></returns>
        public DataSet GetComments()
        {

            SqlConnection con = null;
            DataSet listComments = null;
            string comando = "Select * from [Comentario] order by actividad,fecha desc";
            try
            {
                con = new SqlConnection(conexionTBD);
                SqlDataAdapter sqlAdaptador = new SqlDataAdapter(comando, con);
                listComments = new DataSet();
                sqlAdaptador.Fill(listComments);
                return listComments;

            }
            catch (SqlException)
            {
                // Captura la condición general y la reenvía.
                throw;
            }
            finally
            {
                if (con != null) con.Close(); // Se asegura de cerrar la conexión.
            }
        }
        /// <summary>
        /// Devolvemos los datos de un comentario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetDatosComment(int id)
        {

            SqlConnection con = null;
            DataSet datos = null;
            string comando = "Select * from [Comentario] where id='"+id+"'";
            try
            {
                con = new SqlConnection(conexionTBD);
                SqlDataAdapter sqlAdaptador = new SqlDataAdapter(comando, con);
                datos = new DataSet();
                sqlAdaptador.Fill(datos);
                return datos;

            }
            catch (SqlException)
            {
                // Captura la condición general y la reenvía.
                throw;
            }
            finally
            {
                if (con != null) con.Close(); // Se asegura de cerrar la conexión.
            }
        }
        /// <summary>
        /// Actualizar datos de un comentario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="codActividad"></param>
        /// <param name="codUser"></param>
        public void ModificaComment(int id, string text, int codActividad, string codUser, DateTime date)
        {
            string comando = "UPDATE [Comentario] SET texto = '" + text + "',  actividad = '" +
                codActividad + "', usuario = '" + codUser + "', fecha = '" + date.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = '" + id + "'";
            SqlConnection c = null;
            SqlCommand comandoTBD;

            try
            {
                c = new SqlConnection(conexionTBD);
                comandoTBD = new SqlCommand(comando, c);
                c.Open();
                comandoTBD.CommandType = CommandType.Text;
                comandoTBD.ExecuteNonQuery();

            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                if (c != null) c.Close(); // Se asegura de cerrar la conexión.
            }
        }
    }
}
