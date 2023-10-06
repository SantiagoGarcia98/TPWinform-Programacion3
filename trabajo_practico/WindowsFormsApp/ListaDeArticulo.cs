﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace WindowsFormsApp
{
    public partial class ListadoDeArticulo : Form
    {
        public ListadoDeArticulo()
        {
            InitializeComponent();
        }
        private int indice = 0;
        private List<Articulo> articulos;
        private Articulo seleccion;

        private void ListarArticulos()
        {
            ArticuloNegocio art = new ArticuloNegocio();
            try
            {
                articulos = art.listar();
                dgvArticulos.DataSource = articulos;
                dgvArticulos.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }


        public void CargarImagen(List<Imagen> imagenes)
        {

            try
            {
                ptbImagen.Load(imagenes[indice].Url);

            }
            catch (Exception)
            {
                ptbImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
            finally
            {
                indice++;
                if (indice >= imagenes.Count)
                {
                    btnCambiarImagen.Enabled = false;
                }
            }
        }



        private void btnCambiarImagen_Click(object sender, EventArgs e)
        {

            if (seleccion.UrlImagen != null)
            {
                CargarImagen(seleccion.UrlImagen);
            }
            else
            {
                btnCambiarImagen.Enabled = false;
                ptbImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            indice = 0;
            btnCambiarImagen.Enabled = true;
            if(dgvArticulos.CurrentRow != null)
            seleccion = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            if (seleccion.UrlImagen != null)
            {
                CargarImagen(seleccion.UrlImagen);
            }
            else
            {
                btnCambiarImagen.Enabled = false;
                ptbImagen.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            using (ModificarArticulo ventanaMArticulo = new ModificarArticulo(seleccion))
                ventanaMArticulo.ShowDialog();
            ListarArticulos();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                if (dgvArticulos.CurrentRow != null)
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                else
                {
                    MessageBox.Show("No ha seleccionado ningun articulo");
                    return;
                }
                DialogResult respuesta = MessageBox.Show("Usted quiere eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminarArticulo(seleccionado.Id);
                    ListarArticulos();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListadoDeArticulo_Load(object sender, EventArgs e)
        {
            ListarArticulos();
        }

        private void btnDetalleArticulo_Click(object sender, EventArgs e)
        {
            ArticuloNegocio art = new ArticuloNegocio();
            try
            {
                using (DetalleArticulo ventanaDArticulo = new DetalleArticulo(seleccion))
                    ventanaDArticulo.ShowDialog();
                ListarArticulos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

          

       
  private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;

            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listaFiltrada = articulos.FindAll(x => x.Nombre.ToUpper().Contains( filtro.ToUpper()) || x.Codigo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                MessageBox.Show("El campo texto esta vacio");
                return;
            }

            if (listaFiltrada.Count == 0)
            {
                MessageBox.Show("No hay resultados para la busqueda seleccionada");
                return;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            
        }

      
    }
}