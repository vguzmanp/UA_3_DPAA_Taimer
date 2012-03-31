﻿// ¿Puede haber dos o más DNI iguales?

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taimer;

namespace Taimer {

    /// <summary>
    /// Clase User: Representa al usuario de la aplicación
    /// </summary>
    public class User {

        #region PARTE PRIVADA

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        private string nombre;

        /// <summary>
        /// DNI del usuario
        /// También es la clavi primaria de la base de datos
        /// </summary>
        private string dni;

        /// <summary>
        /// e-Mail del usuario
        /// </summary>
        private string email;

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        private string password;

        /// <summary>
        /// Cusro en el que se encuentra el usuario
        /// </summary>
        private int curso;

        /// <summary>
        /// Titulación en la que está matriculado el usuario
        /// </summary>
        private string titulacion;

        /// <summary>
        /// Último código de horario asignado
        /// </summary>
        private int codHorarios;

        /// <summary>
        /// Lista de Actividades academicas en la que está matriculado el usuario
        /// </summary>
        private List<Actividad_a> actAcademicas = new List<Actividad_a>();          // Un usuario tiene (0,N) actividades académicas

        /// <summary>
        /// Listas de Actividades personales que realiza el usuario
        /// </summary>
        private List<Actividad_p> actPersonales = new List<Actividad_p>();          // Un usuario tiene (0,N) actividades personales

        /// <summary>
        /// Lista de Horarios que tiene guardados el usuario
        /// </summary>
        private List<Horario> horarios = new List<Horario>();                       // Un usuario tiene (0,N) horarios

        /// <summary>
        /// Ultimo código de actividades personales creadas
        /// </summary>
        private int codActPers;

        /// <summary>
        /// Asigna un código un horario
        /// </summary>
        /// <param name="h">Horario al que se le quiere dar un código</param>
        private void AsignarCodigo(Horario h) {
            h.ID = codHorarios;
            codHorarios++;
        }

        #endregion

        #region PARTE PÚBLICA

        /// <summary>
        ///  Constructor básico (sin listas)
        /// </summary>
        /// <param name="nom_">Nombre del Usuario</param>
        /// <param name="dni_">DNI del usuario</param>
        /// <param name="email_">e-Mail del usuario</param>
        /// <param name="pass_">Constraseña del usuario</param>
        /// <param name="curso_">Curso del usuario</param>
        /// <param name="tit_">Titulación del usuario</param>
        public User(string nom_, string dni_, string email_, string pass_, int curso_, string tit_) {
            codHorarios = 0;
            nombre = nom_;
            dni = dni_;
            password = pass_;
            email = email_;
            curso = curso_;
            titulacion = tit_;
        }


        /// <summary>
        /// Constructor completo
        /// Uso exclusivo de los CADs
        /// </summary>
        /// <param name="nom_">Nombre del usuario</param>
        /// <param name="dni_">DNI del usuario</param>
        /// <param name="email_">e-Mail del usuario</param>
        /// <param name="pass_">Contraseña del usuario</param>
        /// <param name="curso_">Curso del usuario</param>
        /// <param name="tit_">Titulación del usuario</param>
        /// <param name="acta_">Lista de actividades academicas en las que está matriculado el usuario</param>
        /// <param name="actp_">Lista de actividades personales que realiza el usuario</param>
        /// <param name="hor_">Lista de horarios que tiene alamacenados el usuario</param>
        public User(string nom_, string dni_, string email_, string pass_, int curso_, string tit_, List<Actividad_a> acta_, List<Actividad_p> actp_, List<Horario> hor_){
            codHorarios = hor_.Count;
            /* -- Al meterle las listas, sobre todo si vienen de los CAD, ya tienen su código puesto
            for (int i = 0; i < actp_.Count; i++)
                actp_[i].Codigo = i;

            for (int i = 0; i < hor_.Count; i++)
                hor_[i].ID = i;
            */

            codHorarios = 0;
            nombre = nom_;
            dni = dni_;
            password = pass_;
            email = email_;
            curso = curso_;
            titulacion = tit_;
            actAcademicas = acta_;
            actPersonales = actp_;
            horarios = hor_;
        }


        /// <summary>
        /// Consturctor de copia
        /// </summary>
        /// <param name="u">User que se quiere copiar</param>
        public User(User u) {
            codHorarios = u.codHorarios;
            dni = u.dni;
            nombre = u.nombre;
            email = u.email;
            password = u.password;
            curso = u.curso;
            actAcademicas = new List<Actividad_a>(u.actAcademicas);
            actPersonales = new List<Actividad_p>(u.actPersonales);
            horarios = new List<Horario>(u.horarios);
            titulacion = u.titulacion;
        }


        /// <summary>
        /// Asigna/Devuelve el nombre del usuario
        /// </summary>
        public string Nombre {
            get { return nombre; }
            set { nombre = value; }
        }


        /// <summary>
        /// Asigna/Devuelve el DNI del usuario
        /// </summary>
        public string DNI {
            get { return dni; }
            set { dni = value; }
        }


        /// <summary>
        /// Asinga/Devuelve el e-Mail del usuario
        /// </summary>
        public string Email {
            get { return email; }
            set { email = value; }
        }


        /// <summary>
        /// Asigna/Devuelve la contraseña del usuario
        /// </summary>
        public string Password {
            get { return password; }
            set { password = value; }
        }


        /// <summary>
        /// Asigna/Devuelve el curso del usuario
        /// </summary>
        public int Curso {
            get { return curso; }
            set { curso = value; }
        }

        /// <summary>
        /// Asigna/Devuelve la titulación del usuario
        /// </summary>
        public string Titulacion {
            set { titulacion = value; }
            get { return titulacion; }
        }

        /// <summary>
        /// Asigna/Devuelve el último código de Horario asignado
        /// </summary>
        public int CodHorarios {
            set { codHorarios = value; }
            get { return codHorarios; }
        }

        /// <summary>
        /// Asigna/Devuelve la lista de Actividad_a a las que está matriculado el usuario
        /// </summary>
        public List<Actividad_a> ActAcademicas {
            set { actAcademicas = value; }
            get { return actAcademicas; }
        }


        /// <summary>
        /// Asigna/Devuelve la lista de Actividad_p que realiza el usuario
        /// </summary>
        public List<Actividad_p> ActPersonales
        {
            set { actPersonales = value; }
            get { return actPersonales; }
        }


        /// <summary>
        /// Añade una Actividad Academica
        /// </summary>
        /// <param name="act">Actividad_a que se desea añadir</param>
        public void AddActAcademica(Actividad_a act) {
            actAcademicas.Add(act);
        }

        /// <summary>
        ///  Borra una actividad académica (si existe). Devuelve valor booleano.
        /// </summary>
        /// <param name="act">Activdad que se desea borrar</param>
        /// <returns>Devuelve TRUE si se ha borrado y FALSE en caso contrario</returns>
        public bool BorraActAcademicaBool(Actividad_a act) {
            return actAcademicas.Remove(act);
        }

        /// <summary>
        ///  Borra una actividad académica (si existe). Lanza excepción.
        /// </summary>
        /// <param name="act">Actividad que se desea borrar</param>
        public void BorraActAcademica(Actividad_a act)
        {
            if(!actAcademicas.Remove(act))
                throw new MissingMemberException("No existe la actividad académica que se desea borrar.");
        }


        /// <summary>
        /// Añade una actividad personal
        /// ¡¡¡NO USAR!!! AÑADIR DESDE PROGRAM
        /// </summary>
        /// <param name="act">Actividad personal que se desa añadir</param>
        public void AddActPersonal(Actividad_p act) {
            actPersonales.Add(act);
        }

        /// <summary>
        ///  Borra una actividad personal (si existe). Devuelve valor booleano.
        /// </summary>
        /// <param name="act">Actividad personal que se desea borrar</param>
        /// <returns>Devuelve TRUE si se ha borrado y FALSE en caso contrario</returns>
        public bool BorraActPersonalBool(Actividad_p act)
        {
            return actPersonales.Remove(act);
        }

        /// <summary>
        /// Borra una actividad personal (si existe). Lanza excepción.
        /// </summary>
        /// <param name="act">Actividad personal que se desea borrar</param>
        public void BorraActPersonal(Actividad_p act)
        {
            if(!actPersonales.Remove(act))
                throw new MissingMemberException("No existe la actividad personal que se desea borrar.");
        }






        /// <summary>
        /// Devolver una actividad, ya sea académica o personal, a partir de su código
        /// </summary>
        public Actividad GetActividad(int cod)
        {
            if (cod >= 0)     // Si el código es un número positivo, es una actividad académica
            {
                foreach (Actividad act in actAcademicas)
                {
                    if (act.Codigo == cod)
                        return act;
                }
            }
            else
            {
                foreach (Actividad act in actPersonales)
                {
                    if (act.Codigo == cod)
                        return act;
                }
            }

            throw new MissingMemberException("No existe ninguna actividad con ese código.");
        }



        /// <summary>
        /// Añade un Horario a la lista de horarios
        /// </summary>
        /// <param name="horario">Hoaraio que se desea añadir</param>
        public void AddHorario(Horario horario) {
            AsignarCodigo(horario);
            horarios.Add(horario);
        }

        /// <summary>
        /// Asigna/Devuelve la lista de horarios del usuario
        /// </summary>
        public List<Horario> Horarios {
            set {
                foreach (Horario h in horarios) {
                    AddHorario(h);
                }
            }
            get { return horarios; }
        }

        /// <summary>
        /// Asigna/Devuelve el último código de la acitivadad personal asignada
        /// </summary>
        public int CodActPers {
            set { codActPers = value; }
            get { return codActPers; }
        }

        /// <summary>
        /// Borrar horario (booleano)
        /// </summary>
        /// <param name="hor">Horario que se desea borrar</param>
        /// <returns>TRUE si borra el horario, FALSE en caso contrario.</returns>
        public bool BorraHorarioBool(Horario hor) {
            return horarios.Remove(hor);
        }


        /// <summary>
        /// Borrar horario (excepción)
        /// </summary>
        /// <param name="hor">Horario que se desea borrar</param>
        public void BorraHorario(Horario hor) {
            if(!horarios.Remove(hor))
                throw new MissingMemberException("No existe el horario que se desea borrar.");
        }



        /// <summary>
        /// Borrar horario a partir de su identificador ID (booleano)
        /// </summary>
        /// <param name="idbuscado">Identificador del Horario que se desea borrar</param>
        /// <returns>TRUE si consigue borrarlo, FALSE en caso contrario</returns>
        public bool BorraHorarioBool (int idbuscado) {
            foreach (Horario hor in horarios) {
                if(hor.ID == idbuscado) 
                    return horarios.Remove(hor);
            }
            return false;
        }


        /// <summary>
        /// Borrar horario a partir de su ID (excepción)
        /// </summary>
        /// <param name="idbuscado">Identificador del Horario que se desea borrar</param>
        public void BorraHorario(int idbuscado)
        {
            if(!BorraHorarioBool(idbuscado))
                throw new MissingMemberException("No existe el horario que se desea borrar.");
        }

        #endregion
    }
}