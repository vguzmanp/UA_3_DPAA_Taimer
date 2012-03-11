﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taimer {
    public class Horario {
        
        //PARTE PRIVADA

        private string nom;
        private int id;
        private ArrayList turnos = new ArrayList();

        //PARTE PUBLICA

        //Constructor
        public Horario(string nom_, int id_) {
            nom = nom_;
            id = id_;
        }

        //Cambiar el nombre
        public void setNom(string nom_) {
            nom = nom_;
        }

        //Cambiar la id
        public void setId(int id_) {
            id = id_;
        }

        //Añadir turnos
        public void AddTurno(Turno turno) {
            foreach (Turno item in turnos)
            {
                if (item.getDia().Equals(turno.getDia()))
                {
                    // si se superponen
                    if((item.getHoraF() > turno.getHoraI() && item.getHoraF() <= turno.getHoraF()) ||
                        (turno.getHoraF() > item.getHoraI() && turno.getHoraF() <= item.getHoraF()) ||
                        (item.getHoraI() >= turno.getHoraI() && item.getHoraI() < turno.getHoraF()) ||
                        (turno.getHoraI() >= item.getHoraI() && turno.getHoraI() < item.getHoraF()))
                        {
                            Exception ex = new Exception("Turnos solapados");
                            throw ex;
                        }
                    
                }
            }
            turnos.Add(turno);
        }

        //Obtener nombre
        public string getNom() {
            return nom;
        }

        //Obtener id
        public int getId() {
            return id;
        }

        //Obtener ArrayList de turnos
        public ArrayList getTurnos() {
            return turnos;
        }

        // Puntuar un horario según el número de días. Puntuará de 0 a 7, añadirá uno por cada día en el que haya turnos.
        public static int puntuarDias(Horario horario)
        {
            int puntuacion = 0;
            bool[] dias = new bool[7];
            foreach (Turno item in horario.getTurnos())
            {
                switch (item.getDia())
                {
                    case 'L': dias[0] = true;
                        break;
                    case 'M': dias[1] = true;
                        break;
                    case 'X': dias[2] = true;
                        break;
                    case 'J': dias[3] = true;
                        break;
                    case 'V': dias[4] = true;
                        break;
                    case 'S': dias[5] = true;
                        break;
                    case 'D': dias[6] = true;
                        break;
                }

            }

            for (int i = 0; i < 7; i++)
            {
                if (dias[i] == true)
                    puntuacion++;
            }

            return puntuacion;
        }

        // Puntuar un horario según el número de horas de hueco. Añadirá uno por cada hora en la que haya huecos entre turnos.
        public static int puntuarHorasHueco(Horario horario)
        {
            int puntuacion = 0;

            foreach (Turno item in horario.getTurnos())
            {
                
            }

            return puntuacion;
        }

        //Generación de un Horario de forma Voraz
        public bool generarHorarioVoraz() {

            Turno t1 = new Turno(new Hora(10, 30), new Hora(12, 30), 'L', "turno1", "L04", 1);
            Turno t2 = new Turno(new Hora(11, 30), new Hora(13, 30), 'L', "turno2", "L04", 1);
            Turno t3 = new Turno(new Hora(12, 30), new Hora(14, 30), 'L', "turno3", "L04", 1);

            Turno t4 = new Turno(new Hora(10, 30), new Hora(12, 30), 'X', "turno4", "L04", 1);
            Turno t5 = new Turno(new Hora(12, 30), new Hora(14, 30), 'X', "turno5", "L04", 1);
            Turno t6 = new Turno(new Hora(14, 30), new Hora(16, 30), 'X', "turno6", "L04", 1);

            Turno t7 = new Turno(new Hora(10, 30), new Hora(14, 30), 'M', "turno7", "L04", 1);
            Turno t8 = new Turno(new Hora(11, 30), new Hora(14, 30), 'M', "turno8", "L04", 1);
            Turno t9 = new Turno(new Hora(12, 30), new Hora(14, 30), 'M', "turno9", "L04", 1);

            Actividad_p actP = new Actividad_p("nombre","descripcion",6,"pepito");
            actP.AddTurno(t1);
            
            Actividad_p actP2 = new Actividad_p("nombre", "descripcion", 6, "pepito");
            actP2.AddTurno(t4);
            actP2.AddTurno(t5);
            actP2.AddTurno(t6);
            
            Actividad_a actA = new Actividad_a("nombre2", "descripcion2", 7, 1);
            actA.AddTurnoTeoria(t3);
            actA.AddTurnoTeoria(t2);

            Actividad_a actA2 = new Actividad_a("nombre2", "descripcion2", 7, 1);
            actA2.AddTurnoTeoria(t7);
            actA2.AddTurnoTeoria(t8);
            actA2.AddTurnoTeoria(t9);

            ArrayList arrayP = new ArrayList();
            ArrayList arrayA = new ArrayList();
            arrayP.Add(actP);
            arrayP.Add(actP2);
            arrayA.Add(actA);
            arrayA.Add(actA2);

            

            foreach (Actividad_p personal in arrayP)
            {
                foreach (Turno item in personal.getTurnos())
                {
                    try 
	                {
                        AddTurno(item);
	                }
	                catch (Exception)
	                {
                        return false;
                    }
                }
                
            }
            bool asignado;
            foreach (Actividad_a asig in arrayA)
            {
                asignado = false;
                foreach (Turno item in asig.getTurnosTeoria())
                {
                    if (asignado)
                        break;
                    try
                    {
                        AddTurno(item);
                        asignado = true;
                    }
                    catch (Exception)
                    {
                        asignado = false;
                    }
                }
                if(!asignado)
                    return false;
            }

            return true;
        }

        public bool generarHorarioBT()
        {

            return true;
        }

    }
}
