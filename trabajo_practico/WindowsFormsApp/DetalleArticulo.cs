﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class DetalleArticulo : Form
    {
        private Articulo articulo;
        private int indice;
        private List<Articulo> art;
        private List<Imagen> imagenes = new List<Imagen>();   
        private int contadorBtn;

        public DetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void DetalleArticulo_Load_1(object sender, EventArgs e)
        {
            btnAnteriorDA.Enabled = false;
            btnSiguienteDA.Enabled = false;
            if (articulo != null)
            {
                lblNombreDA.Text = articulo.Nombre;
                lblCodigoDA.Text = articulo.Codigo;
                lblPrecioDA.Text = articulo.Precio.ToString();
                lblDescripcionDA.Text = articulo.Descripcion;
                if (articulo.NombreMarca != null)
                {
                    lblMarcaDA.Text = articulo.NombreMarca.Descripcion;
                }
                else
                {
                    lblMarcaDA.Text = "Desconocida";
                }

                if (articulo.TipoCategoria != null)
                {
                    lblCategoriaDA.Text = articulo.TipoCategoria.Descripcion;
                }
                else
                {
                    lblCategoriaDA.Text = "Desconocida";
                }
                //if (articulo.UrlImagen != null && articulo.UrlImagen.Count > 0)
                //{
                //    ptbImagenDA.Load(articulo.UrlImagen[0].Url);
                //}
                //else
                //{
                //    ptbImagenDA.Image = null;
                //}
                if (articulo.UrlImagen != null)
                {
                    
                    imagenes = articulo.UrlImagen;
                    CargarImagenes();
                }

            }
        }

        private void btnVolverDA_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void CargarImagenes()
        {

            indice = 0;
            contadorBtn = 0;
            if (imagenes.Count > 1)
                btnSiguienteDA.Enabled = true;
            if (imagenes.Count == 1)
            {
                btnAnteriorDA.Enabled = false;
                btnSiguienteDA.Enabled = false;
            }

            try
            {
                if (imagenes.Count > 0)
                    ptbImagenDA.Load(imagenes[indice].Url);
            }
            catch (Exception)
            {
                ptbImagenDA.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }

        }

        private void CargarImagenes(bool i)
        {

            try
            {
                if (i == false)
                    indice--;
                else
                    if (contadorBtn == 1)
                {
                    indice = 1;
                }
                else
                {
                    indice++;
                }
                ptbImagenDA.Load(imagenes[indice].Url);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (indice >= imagenes.Count - 1)

                    btnSiguienteDA.Enabled = false;

                else
                    btnSiguienteDA.Enabled = true;


                if (indice > 0)

                    btnAnteriorDA.Enabled = true;

                else

                    btnAnteriorDA.Enabled = false;


            }
        }

        private void btnSiguienteDA_Click(object sender, EventArgs e)
        {
            contadorBtn++;
            CargarImagenes(true);
        }

        private void btnAnteriorDA_Click(object sender, EventArgs e)
        {
            CargarImagenes(false);
        }
    }
}
