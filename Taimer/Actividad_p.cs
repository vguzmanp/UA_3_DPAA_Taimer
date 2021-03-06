﻿using CAD;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Windows.Forms;

namespace Taimer {
    /// <summary>
    /// Clase Actividad_p: esta clase hereda de Actividad y representa cualquier otro tipo de actividad idependiente del ambito academico.
    /// Estas actividades son propias de cada usuario
    /// </summary>
    public class Actividad_p : Actividad {

        #region PARTE PRIVADA
        // (Nombre, descripción y código vienen de la clase Actividad)

        /// <summary>
        /// Usuario de la actividad personal
        /// </summary>
        private User usuario;

        #endregion

        #region PARTE PUBLICA

        /// <summary>
        /// Constructor básico (sin lista de turnos)
        /// </summary>
        /// <param name="nom_">Nombre de la Actividad_p</param>
        /// <param name="desc_">Descripción de la Actividad_p</param>
        /// <param name="usu_">Usuario de la Activiad_p (por defecto null se añade le usuario cuando se añade al propio usuario)</param>
        /// <param name="cod_">Codigo de la actividad_p (por defecto 0)</param>
        public Actividad_p(string nom_, string desc_, User usu_ = null, int cod_ = 0)
            : base(nom_, desc_, cod_) {
                usuario = usu_;
        }


        /// <summary>
        ///  Constructor avanzado (con lista de turnos)
        /// </summary>
        /// <param name="nom_">Nombre de la Actividad_p</param>
        /// <param name="desc_">Descripción de la Actividad_p</param>
        /// <param name="turnos_">Listas de turnos en los que se realiza la Actividad_p </param>
        /// <param name="usu_">Usuario al que pertenece esta Actividad_p</param>
        /// <param name="cod_">Codigo de la actividad_p (por defecto 0)</param>
        public Actividad_p(string nom_, string desc_, List<Turno> turnos_, int codTurno, User usu_ = null, int cod_ = 0)
            : base(nom_, desc_, cod_, codTurno, turnos_) {

            usuario = usu_;
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="act">Actividad_p que se desea copiar</param>
        public Actividad_p(Actividad_p act)
            : base(act) {
                // el constructor de copia de la clase base ya llama a CopiarDesde
                usuario = act.usuario;     
//                act.Codigo = usuario.CodActPers;
                
        }

        /// <summary>
        /// Copia una actividad_p en otra, sin necesidad de crearla. Llama a CopiarDesde de la clase Actividad, y añade lo específico para Actividad_p
        /// </summary>
        /// <param name="act">Actividad_a que se desea copiar</param>
        /// <returns></returns>
        public override void CopiarDesde(Actividad act)
        {
            if (this != act)
            {
                base.CopiarDesde(act);
                Actividad_p aux = (Actividad_p)act;
                usuario = aux.usuario;
                //aux.Codigo = usuario.CodActPers;
            }
        }

        /// <summary>
        /// Asigna/Devuelve el usuario de la Actividad_p
        /// </summary>
        public User Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }


        // Añadir turno a una actividad personal (SÍ se comprueba solapamiento)
       /* public void AddTurno(Turno turnonuevo)
        {
            bool insertado = false;
            
            for (int i = 0; i < turnos.Count; i++) {

                if (turnos[i].HoraInicio > turnonuevo.HoraInicio) {
                    foreach (Turno item in turnos) { //¿¿??
                        // Comprobar si se superponen y lanzar excepción si así es
                        item.Superpone(turnonuevo);
                    }

                    AsignarCodigo(turnonuevo);
                    turnonuevo.Actividad = this;
                    turnos.Insert(i, turnonuevo);
                    insertado = true;
                    break;
                }
            }

            if (!insertado) {
                // Comprobar si se superponen y lanzar excepción si así es
                if(turnos.Count > 0)
                    turnonuevo.Superpone(turnos[(turnos.Count - 1)]);

                turnonuevo.Actividad = this;
                AsignarCodigo(turnonuevo);
                

                turnos.Add(turnonuevo);
            }
        }*/

        /// <summary>
        /// Añade un turno a la Actividad_p
        ///  Los turnos están ordenados de forma creciente
        /// </summary>
        /// <param name="turnonuevo"> Turno que se desea añadir </param>
        public void AddTurno(Turno turnonuevo) {
            bool insertado = false;

            turnonuevo.Actividad = this;
            AsignarCodigo(turnonuevo);

            for (int i = 0; i < turnos.Count; i++) {
                turnonuevo.Superpone(turnos[i]); //compruebo si se solapan los turnos
                if (turnos[i].HoraInicio > turnonuevo.HoraInicio) {
                    turnos.Insert(i, turnonuevo);
                    insertado = true;
                    break;
                }
            }

            if (!insertado)
                turnos.Add(turnonuevo);
        }

        /// <summary>
        /// Asigna/Devuelve la lista de Turnos
        /// </summary>
        new public List<Turno> Turnos {
            set {
                foreach (Turno t in value) {
                    AddTurno(t); //Para que los meta con codigo correcto y ordenados
                }
            }
            get { return turnos; }
        }

        /// <summary>
        /// Añade la Actividad Personal a la base de datos
        /// </summary>
        public void Agregar() {
            CADActividad_p act = new CADActividad_p();

            if (codigo == 0) //codigo por defecto
                codigo = UltimoCodigo - 1;
            // MessageBox.Show("nombre = " + nombre + ", descripcion = " + descripcion + ", codigo= " + codigo + ", dni = " + usuario.DNI);
            act.CrearActivida_pAll(nombre, descripcion, codigo, usuario.DNI);

            foreach (Turno t in turnos) //se añaden los turnos a la BD
            {
                t.Actividad = this;
                t.Agregar();
            }

            
        }

        /// <summary>
        /// Modifica una Actividad Personal en la base de datos
        /// </summary>
        public void Modificar()
        {
            CADActividad_p act = new CADActividad_p();
            act.ModificaActividad_p(Nombre, Descripcion, Codigo,codigoturno,usuario.DNI);
        }

        /// <summary>
        /// Borra la actividad personal de la base de datos
        /// </summary>
        public void Borrar() {
            CADActividad_p act = new CADActividad_p();
            act.BorrarActividad_p(codigo);
        }


        /// <summary>
        /// Devuelve el ultimo codigo de activdades personales añadido a la base de datos
        /// </summary>
        public static int UltimoCodigo {
            get {
                CADActividad_p act = new CADActividad_p();
                return int.Parse(act.LastCode().Tables[0].Rows[0].ItemArray[0].ToString());
            }
        }

        /// <summary>
        /// Convierte un DataSet con filas de actividades personales a una lista de objetos Actividad_p
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Actividad_p> Actividades_pToList(DataSet data, User autor)
        {
            if (data != null)
            {
                CAD.CADActividad act = new CAD.CADActividad();
                CAD.CADUser user = new CAD.CADUser();
                List<Actividad_p> list = new List<Actividad_p>();
                DataSet aux = new DataSet();
                int cod;
                string dniUser = "", nom, desc = "";
                DataRowCollection rows = data.Tables[0].Rows;

                for (int i = 0; i < rows.Count; i++)
                {
                    cod = (int)rows[i].ItemArray[0];
                    dniUser = rows[i].ItemArray[1].ToString();

                    aux = act.GetDatosActividad(cod);

                    if (aux != null)
                    {
                        nom = aux.Tables[0].Rows[0].ItemArray[1].ToString();
                        desc = aux.Tables[0].Rows[0].ItemArray[2].ToString();

                        Actividad_p nueva = new Actividad_p(nom, desc, autor, cod);
                        nueva.SetTurnos();
                        list.Add(nueva);
                    }
                    else
                        return null;
                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// Convierte un DataSet que será contendrá una Actividad_p en un objeto
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Actividad_p Actividad_pToObject(DataSet data, User autor)
        {
            if (data != null)
            {
                CAD.CADActividad act = new CAD.CADActividad();
                CAD.CADUser user = new CAD.CADUser();
                Actividad_p actp;
                DataSet aux = new DataSet();
                int cod;
                string dniUser = "", nom, desc = "";
                DataRowCollection rows = data.Tables[0].Rows;

                if (rows.Count != 0)
                {
                    cod = (int)rows[0].ItemArray[0];
                    dniUser = rows[0].ItemArray[1].ToString();

                    aux = act.GetDatosActividad(cod);

                    if (aux != null)
                    {
                        nom = aux.Tables[0].Rows[0].ItemArray[1].ToString();
                        desc = aux.Tables[0].Rows[0].ItemArray[2].ToString();
                        //actp = new Actividad_p(nom, desc, User.UserToObject(user.GetDatosUser(dniUser)), cod);
                        actp = new Actividad_p(nom, desc, autor, cod);
                        actp.SetTurnos();

                        return actp;
                    }
                    else
                        return null;
                }
            }
            return null;
        }
        
        #endregion
    }
}
