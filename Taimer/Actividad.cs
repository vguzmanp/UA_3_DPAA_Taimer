﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Taimer {
    /// <summary>
    /// Clase Actividad: clase abstracta de la que heredarán Actividad_a y Actividad_p
    /// </summary>
    abstract public class Actividad:IEquatable<object> {

        #region PARTE PROTECTED

        /// <summary>
        /// Código de la Actividad y clave primaria en la base de datos
        /// </summary>
        protected int codigo;   
                                  
        /// <summary>
        /// Nombre de la actividad
        /// </summary>
        protected string nombre;

        /// <summary>
        /// Descripción de la Actividad
        /// </summary>
        protected string descripcion;

        /// <summary>
        /// Lista de turnos que tiene la Actividad
        /// </summary>
        protected List<Turno> turnos = new List<Turno>();

        /// <summary>
        /// Último código de turno asignado
        /// </summary>
        protected int codigoturno;

        public int CodTurnos
        {
            get { return codigoturno; }
            set { codigoturno = value; }
        }

        /// <summary>
        ///  Codifica el código del un turno
        /// </summary>
        /// <param name="t"> turno a codificar </param>
        protected void AsignarCodigo(Turno t) {
            codigoturno++;
            t.Codigo = codigoturno;
        }

        #endregion

        #region PARTE PÚBLICA

        /// <summary>
        /// Constructor Básio
        /// </summary>
        /// <param name="nom_"> Nombre de la Actividad</param>
        /// <param name="desc_"> Descripción de la Actividad</param>
        /// <param name="cod_"> Código de la Actividad </param>
        public Actividad(string nom_, string desc_, int cod_, int codTurno = 1) {
            nombre = nom_;
            descripcion = desc_;
            codigo = cod_;
            codigoturno = codTurno;
            turnos = new List<Turno>();
        }


        /// <summary>
        /// Constructor Avanzado
        /// </summary>
        /// <param name="nom_"> Nombre de la Actividad </param>
        /// <param name="desc_"> Descripción de la Actividad </param>
        /// <param name="cod_"> Código de la Actividad </param>
        /// <param name="turnos_"> Lista de turnos a la que se puede asistir </param>
        public Actividad(string nom_, string desc_, int cod_,int codTurno, List<Turno> turnos_)
        {
            nombre = nom_;
            descripcion = desc_;
            codigo = cod_;
            turnos = new List<Turno>(turnos_);
            codigoturno = codTurno;
            foreach (Turno t in turnos) {
                if (t.Codigo == -1) { //si tiene el codigo por defecto
                    AsignarCodigo(t);
                }
            }
        }

        /// <summary>
        /// Constructor de Copia
        /// </summary>
        /// <param name="act"> Actividad que se desea copiar </param>
        public Actividad(Actividad act) {
            CopiarDesde(act);
        }

        /// <summary>
        /// Copia una actividad en otra, sin necesidad de crearla
        /// </summary>
        /// <param name="act">Actividad que se desea copiar</param>
        /// <returns></returns>
        public virtual void CopiarDesde(Actividad act)
        {
            if (this != act)
            {
                nombre = act.nombre;
                descripcion = act.descripcion;
                codigo = act.codigo;
                codigoturno = act.codigoturno;

                turnos = new List<Turno>();
                foreach (Turno item in act.turnos)
                {
                    turnos.Add(new Turno(item));
                }
            }
        }

        /// <summary>
        /// Asigna/Devuelve el nombre de la Actividad
        /// </summary>
        public string Nombre {
            get { return nombre; }
            set { nombre = value; }
        }


        /// <summary>
        /// Asigna/Devuelve la descripción de la Actividad
        /// </summary>
        public string Descripcion {
            get { return descripcion; }
            set { descripcion = value; }
        }


        /// <summary>
        /// Asigna/Devuelve el código de la Actividad
        /// </summary>
        public int Codigo {
            get { return codigo; }
            set { codigo = value; }
        }

        public int Codigoturno
        {
            get { return codigoturno; }
            set { codigoturno = value; }
        }


        // *** FUNCIONES SOBRE LA LISTA DE TURNOS ***


        /// <summary>
        /// Asigna/Devuelve la lista de turnos
        /// </summary>
        public List<Turno> Turnos {
            set {
                foreach (Turno t in value) {
                    AsignarCodigo(t);
                    turnos.Add(t);
                }
            }
            get { return turnos; }
        }


        /// <summary>
        /// Borrar un turno
        /// </summary>
        /// <param name="turno"> Turno que se quiere borrar </param>
        /// <returns> Devuelve TRUE si se ha borrado FALSE en caso contrario </returns>
        public bool BorraTurnoBool(Turno turno) {
            try {
                turno.Borrar();
                return true;
            }
            catch {
                return false;
            }
            //return Turnos.Remove(turno);
        }


        /// <summary>
        /// Borrar un turno
        /// Lanaza excepción si no se puede borrar el turno
        /// </summary>
        /// <param name="turno"> Turno que se quiere borrar </param>
        public void BorraTurno(Turno turno) {
            bool borrado = Turnos.Remove(turno);

            if (!borrado)
                throw new MissingMemberException("No existe el turno que se desea borrar.");
           // turno.Borrar();
        }

        /// <summary>
        /// Borrar un turno a partir de su código
        /// </summary>
        /// <param name="codigobuscado"> Código del turno que se desea borrar </param>
        /// <returns>Devuelve TRUE si se ha borrado y FALSE en caso contrario </returns>
        public bool BorraTurnoBool(int codigobuscado) {
            foreach (Turno t in turnos) {
                if (t.Codigo == codigobuscado) {
                    /*try {
                        t.Borrar();
                    }
                    catch {
                        return false;
                    }*/
                    return Turnos.Remove(t);
                }
            }
            return false;
        }


        /// <summary>
        /// Borrar un turno a partir de su código
        /// Lanza excepción si no se puede borrar el turno
        /// </summary>
        /// <param name="codigobuscado">Código del turno que se desea borrar </param>
        public void BorraTurno(int codigobuscado) {
            bool borrado = false;

            foreach (Turno t in turnos) {
                if (t.Codigo == codigobuscado) {
                    borrado = Turnos.Remove(t);
                    t.Borrar();
                    break;
                }
            }

            if (!borrado)
                throw new MissingMemberException("No existe el turno que se desea borrar.");
        }


        /// <summary>
        /// Indica si la actividad es académica
        /// </summary>
        /// <returns>Devuelve TRUE si es académica y FALSE en caso contrario</returns>
        public bool EsAcademica()
        {
            Type tipo = GetType();
            string s = tipo.ToString();

            if (s == "Taimer.Actividad_a")
                return true;
            else
                return false;
        }


        /// <summary>
        /// Indica si la actividad es personal
        /// </summary>
        /// <returns>Devuelve TRUE si es personal y FALSE en caso contrario </returns>
        public bool EsPersonal()
        {
            Type tipo = GetType();
            string s = tipo.ToString();

            if (s == "Taimer.Actividad_p")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Completa la lista de turnos de una actividad según el contenido de la BD
        /// </summary>
        public void SetTurnos()
        {
            CAD.CADTurno turno = new CAD.CADTurno();
            CAD.CADActividad act = new CAD.CADActividad();
            DataSet data=turno.GetTurnosByAct(codigo);            
            turnos = Turno.TurnosToList(data,this);
            int tope = turnos.Count;
            //MessageBox.Show("Actividad " + codigo + " tiene " + tope);
            if(tope>0)
                codigoturno = turnos[tope-1].Codigo;
            
            act.ModificaActividad(nombre, descripcion, codigo, codigoturno);
        }

        /// <summary>
        /// Compara dos Actividades según su código.
        /// </summary>
        /// <param name="otra"></param>
        /// <returns></returns>
        public override bool Equals(object otra)
        {
            var actividad = otra as Actividad;
            if (actividad == null) return false;
            return this.Codigo == actividad.Codigo;
        }

        public Actividad()
        {
        }

        #endregion
    }
}

