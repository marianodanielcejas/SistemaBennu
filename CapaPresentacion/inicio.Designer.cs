﻿namespace CapaPresentacion
{
    partial class inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inicio));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuusuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menumantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.submenucategoria = new FontAwesome.Sharp.IconMenuItem();
            this.submenuproducto = new FontAwesome.Sharp.IconMenuItem();
            this.submenunegocio = new System.Windows.Forms.ToolStripMenuItem();
            this.submenutipoobra = new System.Windows.Forms.ToolStripMenuItem();
            this.submenutamanioobra = new System.Windows.Forms.ToolStripMenuItem();
            this.menucompras = new FontAwesome.Sharp.IconMenuItem();
            this.submenuregistrarcompra = new FontAwesome.Sharp.IconMenuItem();
            this.submenutverdetallecompra = new FontAwesome.Sharp.IconMenuItem();
            this.submenuregistrarcompramateriales = new System.Windows.Forms.ToolStripMenuItem();
            this.submenuverdetallesmatereales = new System.Windows.Forms.ToolStripMenuItem();
            this.menuventas = new FontAwesome.Sharp.IconMenuItem();
            this.submenuregistrarventaObra = new FontAwesome.Sharp.IconMenuItem();
            this.submenuverdetalleVentaObra = new FontAwesome.Sharp.IconMenuItem();
            this.submenuregistrarventa = new System.Windows.Forms.ToolStripMenuItem();
            this.submenudetalleinsumos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuclientes = new FontAwesome.Sharp.IconMenuItem();
            this.menuproveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menureportes = new FontAwesome.Sharp.IconMenuItem();
            this.submenureportecompras = new System.Windows.Forms.ToolStripMenuItem();
            this.submenureporteventasIC = new FontAwesome.Sharp.IconMenuItem();
            this.submenureportecompramateriales = new System.Windows.Forms.ToolStripMenuItem();
            this.submenureporteventasobra = new System.Windows.Forms.ToolStripMenuItem();
            this.menuacercade = new FontAwesome.Sharp.IconMenuItem();
            this.menutitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.lblusuario = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnsalir = new FontAwesome.Sharp.IconButton();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuusuarios,
            this.menumantenedor,
            this.menucompras,
            this.menuventas,
            this.menuclientes,
            this.menuproveedores,
            this.menureportes,
            this.menuacercade});
            this.menu.Location = new System.Drawing.Point(0, 75);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1028, 73);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // menuusuarios
            // 
            this.menuusuarios.AutoSize = false;
            this.menuusuarios.IconChar = FontAwesome.Sharp.IconChar.UsersCog;
            this.menuusuarios.IconColor = System.Drawing.Color.Black;
            this.menuusuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuusuarios.IconSize = 50;
            this.menuusuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuusuarios.Name = "menuusuarios";
            this.menuusuarios.Size = new System.Drawing.Size(80, 69);
            this.menuusuarios.Text = "Usuarios";
            this.menuusuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuusuarios.Click += new System.EventHandler(this.menuusuarios_Click);
            // 
            // menumantenedor
            // 
            this.menumantenedor.AutoSize = false;
            this.menumantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenucategoria,
            this.submenuproducto,
            this.submenunegocio,
            this.submenutipoobra,
            this.submenutamanioobra});
            this.menumantenedor.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.menumantenedor.IconColor = System.Drawing.Color.Black;
            this.menumantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menumantenedor.IconSize = 50;
            this.menumantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menumantenedor.Name = "menumantenedor";
            this.menumantenedor.Size = new System.Drawing.Size(80, 69);
            this.menumantenedor.Text = "Mantenedor";
            this.menumantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenucategoria
            // 
            this.submenucategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenucategoria.IconColor = System.Drawing.Color.Black;
            this.submenucategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenucategoria.Name = "submenucategoria";
            this.submenucategoria.Size = new System.Drawing.Size(161, 22);
            this.submenucategoria.Text = "Categoria";
            this.submenucategoria.Click += new System.EventHandler(this.submenucategoria_Click);
            // 
            // submenuproducto
            // 
            this.submenuproducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuproducto.IconColor = System.Drawing.Color.Black;
            this.submenuproducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuproducto.Name = "submenuproducto";
            this.submenuproducto.Size = new System.Drawing.Size(161, 22);
            this.submenuproducto.Text = "Producto";
            this.submenuproducto.Click += new System.EventHandler(this.submenuproducto_Click);
            // 
            // submenunegocio
            // 
            this.submenunegocio.Name = "submenunegocio";
            this.submenunegocio.Size = new System.Drawing.Size(161, 22);
            this.submenunegocio.Text = "Negocio";
            this.submenunegocio.Click += new System.EventHandler(this.submenunegocio_Click);
            // 
            // submenutipoobra
            // 
            this.submenutipoobra.Name = "submenutipoobra";
            this.submenutipoobra.Size = new System.Drawing.Size(161, 22);
            this.submenutipoobra.Text = "Tipo de Obras";
            this.submenutipoobra.Click += new System.EventHandler(this.submenutipoobra_Click);
            // 
            // submenutamanioobra
            // 
            this.submenutamanioobra.Name = "submenutamanioobra";
            this.submenutamanioobra.Size = new System.Drawing.Size(161, 22);
            this.submenutamanioobra.Text = "Tamaño de Obra";
            this.submenutamanioobra.Click += new System.EventHandler(this.submenutamanioobra_Click);
            // 
            // menucompras
            // 
            this.menucompras.AutoSize = false;
            this.menucompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuregistrarcompra,
            this.submenutverdetallecompra,
            this.submenuregistrarcompramateriales,
            this.submenuverdetallesmatereales});
            this.menucompras.IconChar = FontAwesome.Sharp.IconChar.DollyFlatbed;
            this.menucompras.IconColor = System.Drawing.Color.Black;
            this.menucompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menucompras.IconSize = 50;
            this.menucompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menucompras.Name = "menucompras";
            this.menucompras.Size = new System.Drawing.Size(80, 69);
            this.menucompras.Text = "Compras";
            this.menucompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuregistrarcompra
            // 
            this.submenuregistrarcompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuregistrarcompra.IconColor = System.Drawing.Color.Black;
            this.submenuregistrarcompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuregistrarcompra.Name = "submenuregistrarcompra";
            this.submenuregistrarcompra.Size = new System.Drawing.Size(186, 22);
            this.submenuregistrarcompra.Text = "Registrar Insumos";
            this.submenuregistrarcompra.Click += new System.EventHandler(this.submenuregistrarcompra_Click);
            // 
            // submenutverdetallecompra
            // 
            this.submenutverdetallecompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenutverdetallecompra.IconColor = System.Drawing.Color.Black;
            this.submenutverdetallecompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenutverdetallecompra.Name = "submenutverdetallecompra";
            this.submenutverdetallecompra.Size = new System.Drawing.Size(186, 22);
            this.submenutverdetallecompra.Text = "Ver Detalle Insumos";
            this.submenutverdetallecompra.Click += new System.EventHandler(this.submenutverdetallecompra_Click);
            // 
            // submenuregistrarcompramateriales
            // 
            this.submenuregistrarcompramateriales.Name = "submenuregistrarcompramateriales";
            this.submenuregistrarcompramateriales.Size = new System.Drawing.Size(186, 22);
            this.submenuregistrarcompramateriales.Text = "Registrar Materiales";
            this.submenuregistrarcompramateriales.Click += new System.EventHandler(this.submenuregistrarcompramateriales_Click);
            // 
            // submenuverdetallesmatereales
            // 
            this.submenuverdetallesmatereales.Name = "submenuverdetallesmatereales";
            this.submenuverdetallesmatereales.Size = new System.Drawing.Size(186, 22);
            this.submenuverdetallesmatereales.Text = "Ver Detalle Materiales";
            this.submenuverdetallesmatereales.Click += new System.EventHandler(this.submenuverdetallesmatereales_Click);
            // 
            // menuventas
            // 
            this.menuventas.AutoSize = false;
            this.menuventas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuregistrarventaObra,
            this.submenuverdetalleVentaObra,
            this.submenuregistrarventa,
            this.submenudetalleinsumos});
            this.menuventas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuventas.IconColor = System.Drawing.Color.Black;
            this.menuventas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuventas.IconSize = 50;
            this.menuventas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuventas.Name = "menuventas";
            this.menuventas.Size = new System.Drawing.Size(80, 69);
            this.menuventas.Text = "Ventas ";
            this.menuventas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuregistrarventaObra
            // 
            this.submenuregistrarventaObra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuregistrarventaObra.IconColor = System.Drawing.Color.Black;
            this.submenuregistrarventaObra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuregistrarventaObra.Name = "submenuregistrarventaObra";
            this.submenuregistrarventaObra.Size = new System.Drawing.Size(189, 22);
            this.submenuregistrarventaObra.Text = "Registrar Matereales";
            this.submenuregistrarventaObra.Click += new System.EventHandler(this.submenuregistrarventaobra_Click);
            // 
            // submenuverdetalleVentaObra
            // 
            this.submenuverdetalleVentaObra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuverdetalleVentaObra.IconColor = System.Drawing.Color.Black;
            this.submenuverdetalleVentaObra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuverdetalleVentaObra.Name = "submenuverdetalleVentaObra";
            this.submenuverdetalleVentaObra.Size = new System.Drawing.Size(189, 22);
            this.submenuverdetalleVentaObra.Text = "Ver Detalle Matereales";
            this.submenuverdetalleVentaObra.Click += new System.EventHandler(this.submenuverdetalleventaobra_Click);
            // 
            // submenuregistrarventa
            // 
            this.submenuregistrarventa.Name = "submenuregistrarventa";
            this.submenuregistrarventa.Size = new System.Drawing.Size(189, 22);
            this.submenuregistrarventa.Text = "Registrar Insumos";
            this.submenuregistrarventa.Click += new System.EventHandler(this.submenuregistrarventa_Click);
            // 
            // submenudetalleinsumos
            // 
            this.submenudetalleinsumos.Name = "submenudetalleinsumos";
            this.submenudetalleinsumos.Size = new System.Drawing.Size(189, 22);
            this.submenudetalleinsumos.Text = "Ver Detalle Insumos";
            this.submenudetalleinsumos.Click += new System.EventHandler(this.submenudetalleinsumos_Click);
            // 
            // menuclientes
            // 
            this.menuclientes.AutoSize = false;
            this.menuclientes.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.menuclientes.IconColor = System.Drawing.Color.Black;
            this.menuclientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuclientes.IconSize = 50;
            this.menuclientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuclientes.Name = "menuclientes";
            this.menuclientes.Size = new System.Drawing.Size(80, 69);
            this.menuclientes.Text = "Clientes";
            this.menuclientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuclientes.Click += new System.EventHandler(this.menuclientes_Click);
            // 
            // menuproveedores
            // 
            this.menuproveedores.AutoSize = false;
            this.menuproveedores.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.menuproveedores.IconColor = System.Drawing.Color.Black;
            this.menuproveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuproveedores.IconSize = 50;
            this.menuproveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuproveedores.Name = "menuproveedores";
            this.menuproveedores.Size = new System.Drawing.Size(80, 69);
            this.menuproveedores.Text = "Proveedores";
            this.menuproveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuproveedores.Click += new System.EventHandler(this.menuproveedores_Click);
            // 
            // menureportes
            // 
            this.menureportes.AutoSize = false;
            this.menureportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenureportecompras,
            this.submenureporteventasIC,
            this.submenureportecompramateriales,
            this.submenureporteventasobra});
            this.menureportes.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            this.menureportes.IconColor = System.Drawing.Color.Black;
            this.menureportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menureportes.IconSize = 50;
            this.menureportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menureportes.Name = "menureportes";
            this.menureportes.Size = new System.Drawing.Size(80, 69);
            this.menureportes.Text = "Reportes";
            this.menureportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenureportecompras
            // 
            this.submenureportecompras.Name = "submenureportecompras";
            this.submenureportecompras.Size = new System.Drawing.Size(263, 22);
            this.submenureportecompras.Text = "Reporte Compras IC";
            this.submenureportecompras.Click += new System.EventHandler(this.submenureportecompras_Click);
            // 
            // submenureporteventasIC
            // 
            this.submenureporteventasIC.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenureporteventasIC.IconColor = System.Drawing.Color.Black;
            this.submenureporteventasIC.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenureporteventasIC.Name = "submenureporteventasIC";
            this.submenureporteventasIC.Size = new System.Drawing.Size(263, 22);
            this.submenureporteventasIC.Text = "Reporte Ventas IC";
            this.submenureporteventasIC.Click += new System.EventHandler(this.submenureporteventasIC_Click);
            // 
            // submenureportecompramateriales
            // 
            this.submenureportecompramateriales.Name = "submenureportecompramateriales";
            this.submenureportecompramateriales.Size = new System.Drawing.Size(263, 22);
            this.submenureportecompramateriales.Text = "Reporte Compra Materiales de Obra";
            this.submenureportecompramateriales.Click += new System.EventHandler(this.submenureportecompramateriales_Click);
            // 
            // submenureporteventasobra
            // 
            this.submenureporteventasobra.Name = "submenureporteventasobra";
            this.submenureporteventasobra.Size = new System.Drawing.Size(263, 22);
            this.submenureporteventasobra.Text = "Reporte Ventas Materiales de Obra";
            this.submenureporteventasobra.Click += new System.EventHandler(this.submenureporteventasobra_Click);
            // 
            // menuacercade
            // 
            this.menuacercade.AutoSize = false;
            this.menuacercade.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.menuacercade.IconColor = System.Drawing.Color.Black;
            this.menuacercade.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuacercade.IconSize = 50;
            this.menuacercade.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuacercade.Name = "menuacercade";
            this.menuacercade.Size = new System.Drawing.Size(80, 69);
            this.menuacercade.Text = "Acerca de";
            this.menuacercade.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuacercade.Click += new System.EventHandler(this.menuacercade_Click);
            // 
            // menutitulo
            // 
            this.menutitulo.AutoSize = false;
            this.menutitulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menutitulo.Location = new System.Drawing.Point(0, 0);
            this.menutitulo.Name = "menutitulo";
            this.menutitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menutitulo.Size = new System.Drawing.Size(1028, 75);
            this.menutitulo.TabIndex = 3;
            this.menutitulo.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(781, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sistema Bennu Insumos de Computación y Venta de Obras";
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.contenedor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("contenedor.BackgroundImage")));
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contenedor.Location = new System.Drawing.Point(0, 148);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1028, 340);
            this.contenedor.TabIndex = 5;
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblusuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.Location = new System.Drawing.Point(909, 20);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(61, 15);
            this.lblusuario.TabIndex = 7;
            this.lblusuario.Text = "lblusuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(850, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Usuario:";
            // 
            // btnsalir
            // 
            this.btnsalir.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnsalir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsalir.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.btnsalir.IconColor = System.Drawing.Color.White;
            this.btnsalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnsalir.IconSize = 52;
            this.btnsalir.Location = new System.Drawing.Point(1055, 12);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.btnsalir.Size = new System.Drawing.Size(85, 42);
            this.btnsalir.TabIndex = 8;
            this.btnsalir.UseVisualStyleBackColor = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 488);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menutitulo);
            this.Name = "inicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private FontAwesome.Sharp.IconMenuItem menuusuarios;
        private FontAwesome.Sharp.IconMenuItem menumantenedor;
        private FontAwesome.Sharp.IconMenuItem submenucategoria;
        private FontAwesome.Sharp.IconMenuItem submenuproducto;
        private System.Windows.Forms.ToolStripMenuItem submenunegocio;
        private FontAwesome.Sharp.IconMenuItem menucompras;
        private FontAwesome.Sharp.IconMenuItem submenuregistrarcompra;
        private FontAwesome.Sharp.IconMenuItem submenutverdetallecompra;
        private FontAwesome.Sharp.IconMenuItem menuclientes;
        private FontAwesome.Sharp.IconMenuItem menuproveedores;
        private FontAwesome.Sharp.IconMenuItem menureportes;
        private System.Windows.Forms.ToolStripMenuItem submenureportecompras;
        private System.Windows.Forms.ToolStripMenuItem submenureporteventasobra;
        private FontAwesome.Sharp.IconMenuItem menuacercade;
        private System.Windows.Forms.MenuStrip menutitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnsalir;
        private System.Windows.Forms.ToolStripMenuItem submenutipoobra;
        private System.Windows.Forms.ToolStripMenuItem submenutamanioobra;
        private FontAwesome.Sharp.IconMenuItem submenureporteventasIC;
        private System.Windows.Forms.ToolStripMenuItem submenuregistrarcompramateriales;
        private System.Windows.Forms.ToolStripMenuItem submenuverdetallesmatereales;
        private FontAwesome.Sharp.IconMenuItem menuventas;
        private FontAwesome.Sharp.IconMenuItem submenuregistrarventaObra;
        private FontAwesome.Sharp.IconMenuItem submenuverdetalleVentaObra;
        private System.Windows.Forms.ToolStripMenuItem submenuregistrarventa;
        private System.Windows.Forms.ToolStripMenuItem submenudetalleinsumos;
        private System.Windows.Forms.ToolStripMenuItem submenureportecompramateriales;
    }
}
