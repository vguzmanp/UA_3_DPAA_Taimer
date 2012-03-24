﻿// ¡SI SE CAMBIA LA HORA DE INICIO O FIN EN UN TURNO PERSONAL, COMPROBAR SOLAPAMIENTO!

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Taimer {
    public enum dias { L, M, X, J, V, S, D };

    public class Turno {

        #region PARTE PRIVADA

        private int codigo;
        private Hora horaInicio;
        private Hora horaFin;
        private dias diasemana;
        private string ubicacion;
        private Actividad actividad;        // Un turno pertenece a (1,1) actividades

        //public static int proximoId; // guarda el siguiente turno (mantener de momento, hasta que haya un autoincremento)

        //Obtener dia de la semana
        private string ObtDiaString() {
            string dia = "";
            switch (diasemana) {
                case dias.L: dia = "Lunes"; break;
                case dias.M: dia = "Martes"; break;
                case dias.X: dia = "Miércoles"; break;
                case dias.J: dia = "Jueves"; break;
                case dias.V: dia = "Viernes"; break;
                case dias.S: dia = "Sábado"; break;
                case dias.D: dia = "Domingo"; break;
            }
            return dia;
        }

        // Cambiar día de la semana con string
        /*private void CambiarDiaSemana(string s) {
            char d = s.ToUpper()[0];
            switch (d) {
                case 'L': diasemana = dias.L; break;
                case 'M':
                    if (s.Length >= 2) { // de verdad esto comprueba que el día exista?
                        if (s.ToUpper()[1] == 'A')
                            diasemana = dias.M;
                        else if(s.ToString().ToUpper().ToCharArray()[1] == 'I')
                            diasemana = dias.X;
                        else
                            throw new Exception("Día de la semana inexistente.");
                    }
                    else
                        throw new Exception("Día de la semana inexistente.");
                break;
                case 'J': diasemana = dias.J; break;
                case 'V': diasemana = dias.V; break;
                case 'S': diasemana = dias.S; break;
                case 'D': diasemana = dias.D; break;
                default:
                    throw new Exception("Día de la semana inexistente.");
            }
        }*/

        private void NormalizarCadena(ref string s) {
            Regex a = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex e = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex i = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex o = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex u = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            s = a.Replace(s, "a");
            s = e.Replace(s, "e");
            s = i.Replace(s, "i");
            s = o.Replace(s, "o");
            s = u.Replace(s, "u");

            s.ToUpper();
        }

        private void CambiarDiaSemana(string s) {
            NormalizarCadena(ref s);
            if (s.Length == 1) {  //Formato de un carácter
                switch (s) {
                    case "L": diasemana = dias.L; break;
                    case "M": diasemana = dias.M; break;
                    case "X": diasemana = dias.X; break;
                    case "J": diasemana = dias.J; break;
                    case "V": diasemana = dias.V; break;
                    case "S": diasemana = dias.S; break;
                    case "D": diasemana = dias.D; break;
                    default:
                        throw new Exception("Día de la semana inexistente.");
                }
            }
            else { //Día completo
                switch (s) {
                    case "LUNES": diasemana = dias.L; break;
                    case "MARTES": diasemana = dias.M; break;
                    case "MIERCOLES": diasemana = dias.X; break;
                    case "JUEVES": diasemana = dias.J; break;
                    case "VIERNES": diasemana = dias.V; break;
                    case "SABADO": diasemana = dias.S; break;
                    case "DOMINGO": diasemana = dias.D; break;
                    default:
                        throw new Exception("Día de la semana inexistente.");
                }
            }
        }

        #endregion

        #region PARTE PÚBLICA


        // Constructor SIN ACTIVIDAD NI CÓDIGO
        public Turno(Hora horaI_, Hora horaF_, dias dia_, string ubic_) {
            /*codigo = proximoId; //HAY QUE AUTOGENERALO!!!
            proximoId++;*/
            codigo = 0;
            if (horaI_ < horaF_) {
                horaInicio = horaI_;
                horaFin = horaF_;
            }
            else
                throw new Exception("La hora de inicio es la misma o más tarde que la hora de finalización");

            diasemana = dia_;
            ubicacion = ubic_;
            actividad = null;
        }

        // Constructor
        //Crea un turno y lo vincula a una Actividad
        public Turno(Hora horaI_, Hora horaF_, dias dia_, string ubic_, Actividad act_) {

            if (horaI_ < horaF_) {
                horaInicio = horaI_;
                horaFin = horaF_;
            }
            else
                throw new Exception("La hora de inicio es la misma o más tarde que la hora de finalización");

            diasemana = dia_;
            ubicacion = ubic_;
            actividad = act_;

            if (actividad.EsAcademica())
                ((Actividad_a)actividad).AddTurno(this);
            else 
                ((Actividad_p)actividad).AddTurno(this);
        }

        // Constructor
        //Uso exclusivo de los CADs
        //Se le ponen todos los datos
        public Turno(int cod_, Hora horaI_, Hora horaF_, dias dia_, string ubic_, Actividad act_) {
            
            codigo = cod_;

            if (horaI_ < horaF_) {
                horaInicio = horaI_;
                horaFin = horaF_;
            }
            else
                throw new Exception("La hora de inicio es la misma o más tarde que la hora de finalización");

            diasemana = dia_;
            ubicacion = ubic_;
            actividad = act_;
        }


        // Constructor de copia
        public Turno(Turno t) {
            codigo = t.codigo;
            horaInicio = new Hora(t.horaInicio);
            horaFin = new Hora(t.horaFin);
            diasemana = t.diasemana;
            ubicacion = t.ubicacion;
            actividad = t.actividad;
        }


        // Comparar si dos turnos son iguales (exceptuando su código y asignatura).
        // He preferido no usar el operador igualdad, dado que no son 100% iguales.
        public bool TurnoSimilar(Turno t) {
            if (horaInicio == t.horaInicio)
                if (horaFin == t.horaFin)
                    if (diasemana == t.diasemana)
                        if (ubicacion == t.ubicacion) ///es importante la ubicación para considerarlos similares???
                            return true;

            return false;
        }


        // Cambiar/obtener actividad
        public Actividad Actividad {
            set { actividad = value; }
            get { return actividad; }
        }


        // Cambiar/obtener código
        // Lanza excepción si el código ya existe
        //NO RECOMENDADO USAR EL SET (codigo autogenerado)
        public int Codigo {
            set {
                    foreach (Turno existente in actividad.Turnos) {
                        if (existente.Codigo == value)
                            throw new ArgumentException("El código ya existe.");
                    }

                codigo = value;
                }

            get { return codigo; }
        }


        // Cambiar Hora Inicio con dos Integer
        public void HoraI(int hora, int min) {
            HoraInicio = new Hora(hora, min);
        }


        // Cambiar Hora Fin con dos Integer
        public void HoraF(int hora, int min) {
            HoraFin = new Hora(hora, min);
        }

        // Cambiar Hora Inicio con un string
        public void HoraI(string horaI) {
            HoraInicio = new Hora(horaI);
        }

        // Cambiar Hora Fin con un string
        public void HoraF(string horaF) {
            HoraFin = new Hora(horaF);
        }

        // Obtener/Cambiar Hora de inicio
        public Hora HoraInicio {
            set {
                if (value < HoraFin) {
                    if (actividad.EsAcademica())
                        horaInicio = value;
                    else {
                        Turno test = new Turno(this);
                        test.horaInicio = value;

                        if (actividad != null) {
                            foreach (Turno existente in actividad.Turnos) {
                                if (existente.Codigo != this.Codigo) {
                                    test.Superpone(existente);
                                }
                            }
                        }

                        horaInicio = value;
                    }
                }
                else
                    throw new ArgumentOutOfRangeException("La hora de inicio no puede ser posterior a la de fin.");
            }
            get { return horaInicio; }
        }


        // Obtener/Cambiar Hora de Fin
        public Hora HoraFin {
            set { 
                    if(value > HoraInicio) {
                        if (actividad.EsAcademica())
                            horaFin = value;
                        else {
                            Turno test = new Turno(this);
                            test.horaFin = value;

                            if (actividad != null) {
                                foreach (Turno existente in actividad.Turnos) {
                                    if (existente.Codigo != this.Codigo) {
                                        test.Superpone(existente);
                                    }
                                }
                            }

                            horaFin = value;
                        }
                    }
                    else
                        throw new ArgumentOutOfRangeException("La hora de fin no puede ser anterior a la de inicio.");
                }

            get { return horaFin; }
        }

        //Cambiar Hora de inicio y de Fin 
        //Se le pasa dos objetos hora
        public void CambiarHoras(Hora inicio, Hora fin) {
            if (actividad.EsAcademica()) {
                horaInicio = inicio;
                horaFin = fin;
            }
            else {
                Turno test = new Turno(this);
                test.horaInicio = inicio;
                test.horaFin = fin;

                if (actividad != null) {
                    foreach (Turno existente in actividad.Turnos) {
                        if (existente.Codigo != this.Codigo) {
                            test.Superpone(existente);
                        }
                    }
                }

                horaInicio = inicio;
                horaFin = fin;
            }
        }

        //Cambiar Hora de inicio y de Fin
        //Se le pasa las horas como enteros
        public void CambiarHoras(int horaI, int minI, int horaF, int minF) {
            Hora inicio = new Hora(horaI, minI);
            Hora fin = new Hora(horaF, minF);

            CambiarHoras(inicio, fin);
        }

        //Cambiar Hora de inicio y de Fin
        //Se le pasas las horas como strings
        public void CambiarHoras(string inicio, string fin) {
            Hora i = new Hora(inicio);
            Hora f = new Hora(fin);

            CambiarHoras(i, f);
        }


        // Obtener/Cambiar dia de la semana
        public dias Dia{
            get { return diasemana; }
            set { diasemana = value; }
        }

        //Obtener/Cambiar dia de la semna mediante un string
        public string DiaString {
            get { return ObtDiaString(); }
            set { CambiarDiaSemana(value); }
        }


        // Cambiar/Obtener ubicacion
        public string Ubicacion {
            set { ubicacion = value; }
            get { return ubicacion; }
        }

        // Comprobar superposición y lanzar excepción si se superponen
        public void Superpone(Turno t)
        {
            // Si se superponen...
            if (Dia.Equals(t.Dia))
            {
                if ((HoraFin > t.HoraInicio && HoraFin <= t.HoraFin) ||
                    (t.HoraFin > HoraInicio && t.HoraFin <= HoraFin) ||
                    (HoraInicio >= t.HoraInicio && HoraInicio < t.HoraFin) ||
                    (t.HoraInicio >= HoraInicio && t.HoraInicio < HoraFin)) {
                    throw new NotSupportedException("Turnos solapados.");
                }
            }
        }

        // Comprobar superposición de turnos
        // Devuelve TRUE si se superponen los turnos, FALSE si no se solapan.
        public bool SuperponeBool(Turno t)
        {
            bool superpuesto = false;
            try
            {
                Superpone(t);
            }
            catch
            {
                superpuesto = true;
            }

            return superpuesto;
        }


        #endregion
    }
}