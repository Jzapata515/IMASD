using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nomina2018.BACK.CONEXION;
using Nomina2018.BACK.SERVICES;
using Nomina2018.BACK.BO;
using Nomina2018.BACK.DA;

namespace Nomina2018
{
    public partial class EmpleadosUI : System.Web.UI.Page
    {
        #region ************** Instancias **************

        Cadena cadena = new Cadena { conexion = ConfigurationManager.ConnectionStrings["Nomina2018"].ToString() };

        #endregion

        #region ************** Eventos **************

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.OnCall();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        protected void rEmpleados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                EmpleadoCtrl Empleadoctrl = new EmpleadoCtrl();
                TabuladorCtrl TabuladorCtrl = new TabuladorCtrl();
                switch (e.CommandName)
                {
                    case "Eliminar":
                        RepeaterItem item = e.Item;
                        Empleadoctrl.Delete(new Empleado { IdEmpleado = Convert.ToInt16(e.CommandArgument.ToString()) }, cadena);
                        this.OnCall();
                        break;

                    case "Editar":
                        this.ShowForm();
                        this.HideFormTabulador();
                        RepeaterItem item2 = e.Item;
                        Empleado empleado = Empleadoctrl.LastDataRowToData(Empleadoctrl.Retrieve(new Empleado { IdEmpleado = Convert.ToInt16(e.CommandArgument.ToString()) }, cadena));
                        this.DataToUserInterface(empleado);
                        break;

                    case "Tabulador":
                        this.PrepareNewTabulador();
                        this.ShowFormTabulador();
                        this.HideForm();
                        RepeaterItem item3 = e.Item;
                        empleado = Empleadoctrl.LastDataRowToData(Empleadoctrl.Retrieve(new Empleado { IdEmpleado = Convert.ToInt16(e.CommandArgument.ToString()) }, cadena));
                        Tabulador tabulador = new Tabulador();
                        DataSet ds = TabuladorEmpleadoRetHlp.Action(tabulador, empleado, cadena);
                        if (ds.Tables.Contains("TABULADORESEMPLEADOS"))
                        {
                            if (ds.Tables["TABULADORESEMPLEADOS"].Rows.Count > 0)
                            {
                                DataRow drtabulador = ds.Tables["TABULADORESEMPLEADOS"].Rows[0];
                                if (!drtabulador.IsNull("IdTabulador"))
                                    tabulador.IdTabulador = (Int32)Convert.ChangeType(drtabulador["IdTabulador"], typeof(Int32));
                                tabulador = TabuladorCtrl.LastDataRowToData(TabuladorCtrl.Retrieve(tabulador, cadena));
                                this.PopulateTabuladores(empleado.Puesto);
                                this.TabuladorToUserInterface(tabulador);
                            }
                        }

                        this.TabuladorToUserInterface(empleado);
                        break;

                    case "":
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage("Ocurrió un Error:" + ex.Message, 1);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                EmpleadoCtrl Empleadoctrl = new EmpleadoCtrl();
                Empleado empleado = this.UserInterfaceToData();
                if (empleado.IdEmpleado > 0)
                    Empleadoctrl.Update(empleado, cadena);
                else
                    Empleadoctrl.Insert(empleado, cadena);
                this.OnCall();
            }
            catch (Exception ex)
            {
                this.ShowMessage("Ocurrió un Error:" + ex.Message, 1);
            }
        }

        protected void ibtnNuevo_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowForm();
            this.PrepareNew();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.HideForm();
        }

        protected void btnCancelarTabulador_Click(object sender, EventArgs e)
        {
            this.HideFormTabulador();
        }

        protected void btnGuardarTabulador_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado empleado = this.UserInterfaceToEmpleado();
                Tabulador tabulador = this.UserInterfaceToTabulador();
                TabuladorProHlp.Action(tabulador, empleado, cadena);
                this.OnCall();

            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
            }

        }

        protected void ddlTabulador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TabuladorCtrl TabuladorCtrl = new TabuladorCtrl();
                Tabulador tabulador = TabuladorCtrl.LastDataRowToData(TabuladorCtrl.Retrieve(new Tabulador { IdTabulador = Convert.ToInt32(this.ddlTabulador.SelectedValue) }, cadena));

                this.TabuladorToUserInterface(tabulador);
            }
            catch (Exception ex)
            {
                this.ShowMessage("Ocurrió un Error:" + ex.Message, 1);
            }

        }

        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.PopulateTabuladores(new Puesto { IdPuesto = Convert.ToInt32(this.ddlPuesto.SelectedValue) });
            }
            catch (Exception ex)
            {
                this.ShowMessage("Ocurrió un Error:" + ex.Message, 1);
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Departamento departamento = new Departamento { IdDepartamento = Convert.ToInt32(this.ddlDepartamento.SelectedValue) };
                this.PopulatePuestos(departamento);
            }
            catch (Exception ex)
            {
                this.ShowMessage("Ocurrió un Error:" + ex.Message, 1);
            }
        }

        protected void ddlDepartamentoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(this.ddlDepartamentoFiltro.SelectedValue != "" )
                    this.PopulatePuestosFiltro(new Departamento { IdDepartamento = Convert.ToInt32(this.ddlDepartamentoFiltro.SelectedValue) });
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado empleado = this.UserInterfaceToFiltro();
                this.PopulateEmpleados(empleado);
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        #endregion

        #region ************** Métodos **************

        public void ShowMessage(string mensaje, int tipo)
        {
            switch (tipo)
            {
                case 1:
                    string tipotext = "Alerta";
                    string script = "<script language=javascript>jAlert('" + mensaje + "', '" + tipotext + "');</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script);
                    break;
            }

        }

        public void OnCall()
        {
            this.PopulateEmpleados( new Empleado { Estatus= "A" });
            this.PrepareNew();
            this.PrepareNewTabulador();
            this.PopulateDepartamentos();
            this.PopulateDepartamentosFiltro();
            this.PopulatePeriodosPago();
            this.PopulatePeriodosPagoFiltro();
            this.PopulateTiposPago();
            this.ddatos.Visible = false;
            this.dTabulador.Visible = false;
        }

        public Empleado UserInterfaceToData()
        {
            Empleado empleado = new Empleado();

            if (!string.IsNullOrEmpty(this.hndIdEmpleado.Value))
                empleado.IdEmpleado = Convert.ToInt16(this.hndIdEmpleado.Value);
            else
                empleado.IdEmpleado = 0;

            if (!string.IsNullOrEmpty(this.hndIdDepartamento.Value))
                empleado.SalarioDiario = Convert.ToDecimal(this.hndIdDepartamento.Value);
            else
                empleado.SalarioDiario = 0;

            if (!string.IsNullOrEmpty(this.hndEstatus.Value))
                empleado.Estatus = this.hndEstatus.Value;
            else
                empleado.Estatus = "";

            if (!string.IsNullOrEmpty(this.txtNombres.Text))
                empleado.Nombres = this.txtNombres.Text;
            else
                empleado.Nombres = null;

            if (!string.IsNullOrEmpty(this.txtApellidoP.Text))
                empleado.ApellidoP = this.txtApellidoP.Text;
            else
                empleado.ApellidoP = null;

            if (!string.IsNullOrEmpty(this.txtApellidoM.Text))
                empleado.ApellidoM = this.txtApellidoM.Text;
            else
                empleado.ApellidoM = null;

            if (!string.IsNullOrEmpty(this.txtDireccion.Text))
                empleado.Direccion = this.txtDireccion.Text;
            else
                empleado.Direccion = null;

            if (!string.IsNullOrEmpty(this.txtTelefono.Text))
                empleado.Telefono = this.txtTelefono.Text;
            else
                empleado.Telefono = null;

            if (!string.IsNullOrEmpty(this.txtEmail.Text))
                empleado.Email = this.txtEmail.Text;
            else
                empleado.Email = null;

            if (!string.IsNullOrEmpty(this.txtClabe.Text))
                empleado.Clabe = this.txtClabe.Text;
            else
                empleado.Clabe = null;

            empleado.Puesto = new Puesto();
            empleado.Departamento = new Departamento();
            empleado.PeriodoPago = new PeriodoPago();
            empleado.TipoPago = new TipoPago();

            if (!string.IsNullOrEmpty(this.hndIdPuesto.Value))
                empleado.Puesto.IdPuesto = Convert.ToInt32(this.hndIdPuesto.Value);
            else
                empleado.Puesto.IdPuesto = 0;

            if (!string.IsNullOrEmpty(this.hndIdDepartamento.Value))
                empleado.Departamento.IdDepartamento = Convert.ToInt32(this.hndIdDepartamento.Value);
            else
                empleado.Departamento.IdDepartamento = 0;


            empleado.PeriodoPago.IdPeriodoPago = Convert.ToInt32(this.ddlPeriodoPago.SelectedValue);
            empleado.TipoPago.IdTipoPago = Convert.ToInt32(this.ddlTipoPago.SelectedValue);

            return empleado;
        }

        public void DataToUserInterface(Empleado empleado)
        {
            if (empleado.IdEmpleado > 0)
                this.hndIdEmpleado.Value = empleado.IdEmpleado.ToString();
            else
                this.hndIdEmpleado.Value = "0";

            if (!string.IsNullOrEmpty(empleado.Estatus))
                this.hndEstatus.Value = empleado.Estatus;
            else
                this.hndEstatus.Value = "";

            if (empleado.SalarioDiario > 0)
                this.hndIdDepartamento.Value = empleado.SalarioDiario.ToString();
            else
                this.hndIdDepartamento.Value = "0";

            if (!string.IsNullOrEmpty(empleado.Nombres))
                this.txtNombres.Text = empleado.Nombres;
            else
                this.txtNombres.Text = null;

            if (!string.IsNullOrEmpty(empleado.ApellidoP))
                this.txtApellidoP.Text = empleado.ApellidoP;
            else
                this.txtApellidoP.Text = null;

            if (!string.IsNullOrEmpty(empleado.ApellidoM))
                this.txtApellidoM.Text = empleado.ApellidoM;
            else
                this.txtApellidoM.Text = null;

            if (!string.IsNullOrEmpty(empleado.Direccion))
                this.txtDireccion.Text = empleado.Direccion;
            else
                this.txtDireccion.Text = null;

            if (!string.IsNullOrEmpty(empleado.Telefono))
                this.txtTelefono.Text = empleado.Telefono;
            else
                this.txtTelefono.Text = null;

            if (!string.IsNullOrEmpty(empleado.Email))
                this.txtEmail.Text = empleado.Email;
            else
                this.txtEmail.Text = null;

            if (!string.IsNullOrEmpty(empleado.Clabe))
                this.txtClabe.Text = empleado.Clabe;
            else
                this.txtClabe.Text = null;

            if (empleado.Departamento != null && empleado.Departamento.IdDepartamento > 0)
                this.hndIdDepartamento.Value = Convert.ToString(empleado.Departamento.IdDepartamento);

            if (empleado.Puesto != null && empleado.Puesto.IdPuesto > 0)
                this.hndIdPuesto.Value = Convert.ToString(empleado.Puesto.IdPuesto);

            if (empleado.PeriodoPago != null && empleado.PeriodoPago.IdPeriodoPago > 0)
                this.ddlPeriodoPago.SelectedValue = Convert.ToString(empleado.PeriodoPago.IdPeriodoPago);

            if (empleado.TipoPago != null && empleado.TipoPago.IdTipoPago > 0)
                this.ddlTipoPago.SelectedValue = Convert.ToString(empleado.TipoPago.IdTipoPago);
        }

        public void TabuladorToUserInterface(Tabulador tabulador)
        {
            if (tabulador.IdTabulador > 0)
                this.ddlTabulador.SelectedValue = Convert.ToString(tabulador.IdTabulador);

            if (tabulador.LimiteInferior > 0)
                this.lblLimiteInferior.Text = Convert.ToString(tabulador.LimiteInferior);
            else
                this.lblLimiteInferior.Text = "0";

            if (tabulador.LimiteSuperior > 0)
                this.lblLimiteSuperior.Text = Convert.ToString(tabulador.LimiteSuperior);
            else
                this.lblLimiteSuperior.Text = "0";
        }

        public void TabuladorToUserInterface(Empleado empleado)
        {
            if (empleado.IdEmpleado > 0)
                this.hdnIdEmpleadoTabulador.Value = empleado.IdEmpleado.ToString();
            else
                this.hdnIdEmpleadoTabulador.Value = "0";

            if (!string.IsNullOrEmpty(empleado.NombreCompleto))
                this.lblNombreEmpleado.Text = empleado.NombreCompleto;
            else
                this.lblNombreEmpleado.Text = "";

            if (empleado.SalarioDiario > 0)
                this.txtSalarioEmpleado.Text = empleado.SalarioDiario.ToString();
            else
                this.txtSalarioEmpleado.Text = "0";

            if (empleado.Departamento != null && empleado.Departamento.IdDepartamento > 0)
            {
                this.ddlDepartamento.SelectedValue = Convert.ToString(empleado.Departamento.IdDepartamento);
            }

            if (empleado.Puesto != null && empleado.Puesto.IdPuesto > 0)
            {
                this.PopulatePuestos(empleado.Departamento);
                this.ddlPuesto.SelectedValue = Convert.ToString(empleado.Puesto.IdPuesto);
            }
        }

        public Empleado UserInterfaceToEmpleado()
        {
            Empleado empleado = new Empleado();

            if (this.ddlPuesto.SelectedValue != "" && Convert.ToInt32(this.ddlPuesto.SelectedValue) > 0)
            {
                empleado.Puesto = new Puesto();
                empleado.Puesto.IdPuesto = Convert.ToInt32(this.ddlPuesto.SelectedValue);
            }
            else
                throw new Exception("Debe seleccionar un Puesto");

            if (this.ddlDepartamento.SelectedValue != "" && Convert.ToInt32(this.ddlDepartamento.SelectedValue) > 0)
            {
                empleado.Departamento = new Departamento();
                empleado.Departamento.IdDepartamento = Convert.ToInt32(this.ddlDepartamento.SelectedValue);
            }
            else
                throw new Exception("Debe seleccionar un Departamento");

            if (Convert.ToInt32(this.hdnIdEmpleadoTabulador.Value) > 0)
                empleado.IdEmpleado = Convert.ToInt32(this.hdnIdEmpleadoTabulador.Value);

            if (Convert.ToDecimal(this.txtSalarioEmpleado.Text) > 0)
                empleado.SalarioDiario = Convert.ToDecimal(this.txtSalarioEmpleado.Text);

            decimal limiteinferior = Convert.ToDecimal(this.lblLimiteInferior.Text);
            decimal limitesuperior = Convert.ToDecimal(this.lblLimiteSuperior.Text);

            if (!(empleado.SalarioDiario >= limiteinferior && empleado.SalarioDiario <= limitesuperior))
                throw new Exception("El salario del Empleado debe estar entre los límites del tabulador");

            return empleado;
        }

        public Empleado UserInterfaceToFiltro()
        {
            Empleado empleado = new Empleado();

            if (this.txtNumeroEmpleadoFiltro.Text != "" && Convert.ToInt32(txtNumeroEmpleadoFiltro.Text) > 0)
                empleado.IdEmpleado = Convert.ToInt32(txtNumeroEmpleadoFiltro.Text);

            if (this.ddlPuestoFiltro.SelectedValue != "" && Convert.ToInt32(this.ddlPuestoFiltro.SelectedValue) > 0)
            {
                empleado.Puesto = new Puesto();
                empleado.Puesto.IdPuesto = Convert.ToInt32(this.ddlPuestoFiltro.SelectedValue);
            }
           

            if (this.ddlDepartamentoFiltro.SelectedValue != "" && Convert.ToInt32(this.ddlDepartamentoFiltro.SelectedValue) > 0)
            {
                empleado.Departamento = new Departamento();
                empleado.Departamento.IdDepartamento = Convert.ToInt32(this.ddlDepartamentoFiltro.SelectedValue);
            }

            if (this.ddlPeriodoPagoFiltro.SelectedValue != "" && Convert.ToInt32(this.ddlPeriodoPagoFiltro.SelectedValue) > 0)
            {
                empleado.PeriodoPago = new PeriodoPago();
                empleado.PeriodoPago.IdPeriodoPago = Convert.ToInt32(this.ddlPeriodoPagoFiltro.SelectedValue);
            }

            empleado.Estatus = "A";

            return empleado;
        }

        public Tabulador UserInterfaceToTabulador()
        {
            Tabulador tabulador = new Tabulador();

            if (this.ddlTabulador.SelectedValue != "" && Convert.ToInt32(this.ddlTabulador.SelectedValue) > 0)
                tabulador.IdTabulador = Convert.ToInt32(this.ddlTabulador.SelectedValue);
            else
                throw new Exception("Debe seleccionar un Tabulador");

            return tabulador;
        }

        public void PopulateEmpleados(Empleado empleado)
        {
            try
            {
                EmpleadoCtrl Empleadoctrl = new EmpleadoCtrl();
                DataSet ds = Empleadoctrl.Retrieve(empleado, cadena);
                this.rEmpleados.DataSource = null;
                this.rEmpleados.DataBind();
                this.rEmpleados.DataSource = ds;
                this.rEmpleados.DataBind();
                if (ds.Tables.Contains("EMPLEADOS"))
                {
                    if (ds.Tables["EMPLEADOS"].Rows.Count == 0)
                        this.ShowMessage("No se encontraron Empleados", 1);
                }
                                       
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulateDepartamentos()
        {
            try
            {
                DepartamentoCtrl DepartamentoCtrl = new DepartamentoCtrl();
                DataSet ds = DepartamentoCtrl.Retrieve(new Departamento { Estatus = "A" }, cadena);
                this.ddlDepartamento.Items.Clear();
                this.ddlDepartamento.DataSource = ds;
                this.ddlDepartamento.DataValueField = "IdDepartamento";
                this.ddlDepartamento.DataTextField = "Nombre";
                this.ddlDepartamento.DataBind();
                this.ddlDepartamento.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulateDepartamentosFiltro()
        {
            try
            {
                DepartamentoCtrl DepartamentoCtrl = new DepartamentoCtrl();
                DataSet ds = DepartamentoCtrl.Retrieve(new Departamento { Estatus = "A" }, cadena);
                this.ddlDepartamentoFiltro.Items.Clear();
                this.ddlDepartamentoFiltro.DataSource = ds;
                this.ddlDepartamentoFiltro.DataValueField = "IdDepartamento";
                this.ddlDepartamentoFiltro.DataTextField = "Nombre";
                this.ddlDepartamentoFiltro.DataBind();
                this.ddlDepartamentoFiltro.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulatePuestos(Departamento departamento)
        {
            try
            {
                PuestoCtrl PuestoCtrl = new PuestoCtrl();
                DataSet ds = PuestoCtrl.Retrieve(new Puesto { IdDepartamento = departamento.IdDepartamento, Estatus = "A" }, cadena);
                this.ddlPuesto.Items.Clear();
                this.ddlPuesto.DataSource = ds;
                this.ddlPuesto.DataValueField = "IdPuesto";
                this.ddlPuesto.DataTextField = "Nombre";
                this.ddlPuesto.DataBind();
                this.ddlPuesto.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulatePuestosFiltro(Departamento departamento)
        {
            try
            {
                PuestoCtrl PuestoCtrl = new PuestoCtrl();
                DataSet ds = PuestoCtrl.Retrieve(new Puesto { IdDepartamento = departamento.IdDepartamento, Estatus = "A" }, cadena);
                this.ddlPuestoFiltro.Items.Clear();
                this.ddlPuestoFiltro.DataSource = ds;
                this.ddlPuestoFiltro.DataValueField = "IdPuesto";
                this.ddlPuestoFiltro.DataTextField = "Nombre";
                this.ddlPuestoFiltro.DataBind();
                this.ddlPuestoFiltro.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulatePeriodosPago()
        {
            try
            {
                PeriodoPagoCtrl PeriodoPagoCtrl = new PeriodoPagoCtrl();
                DataSet ds = PeriodoPagoCtrl.Retrieve(new PeriodoPago { Estatus = "A" }, cadena);
                this.ddlPeriodoPago.Items.Clear();
                this.ddlPeriodoPago.DataSource = ds;
                this.ddlPeriodoPago.DataValueField = "IdPeriodoPago";
                this.ddlPeriodoPago.DataTextField = "Nombre";
                this.ddlPeriodoPago.DataBind();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulatePeriodosPagoFiltro()
        {
            try
            {
                PeriodoPagoCtrl PeriodoPagoCtrl = new PeriodoPagoCtrl();
                DataSet ds = PeriodoPagoCtrl.Retrieve(new PeriodoPago { Estatus = "A" }, cadena);
                this.ddlPeriodoPagoFiltro.Items.Clear();
                this.ddlPeriodoPagoFiltro.DataSource = ds;
                this.ddlPeriodoPagoFiltro.DataValueField = "IdPeriodoPago";
                this.ddlPeriodoPagoFiltro.DataTextField = "Nombre";
                this.ddlPeriodoPagoFiltro.DataBind();
                this.ddlPeriodoPagoFiltro.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulateTiposPago()
        {
            try
            {
                TipoPagoCtrl TipoPagoCtrl = new TipoPagoCtrl();
                DataSet ds = TipoPagoCtrl.Retrieve(new TipoPago { Estatus = "A" }, cadena);
                this.ddlTipoPago.Items.Clear();
                this.ddlTipoPago.DataSource = ds;
                this.ddlTipoPago.DataValueField = "IdTipoPago";
                this.ddlTipoPago.DataTextField = "Nombre";
                this.ddlTipoPago.DataBind();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PopulateTabuladores(Puesto puesto)
        {
            try
            {
                TabuladorCtrl TabuladorCtrl = new TabuladorCtrl();
                DataSet ds = TabuladorCtrl.Retrieve(new Tabulador { IdPuesto = puesto.IdPuesto, Estatus = "A" }, cadena);
                this.ddlTabulador.Items.Clear();
                this.ddlTabulador.DataSource = ds;
                this.ddlTabulador.DataValueField = "IdTabulador";
                this.ddlTabulador.DataTextField = "Nombre";
                this.ddlTabulador.DataBind();
                this.ddlTabulador.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, 1);
            }
        }

        public void PrepareNew()
        {
            this.hndIdEmpleado.Value = string.Empty;
            this.hndEstatus.Value = string.Empty;
            this.hndIdDepartamento.Value = string.Empty;
            this.hndSalarioDiario.Value = string.Empty;
            this.hndIdPuesto.Value = string.Empty;
            this.txtNombres.Text = string.Empty;
            this.txtApellidoP.Text = string.Empty;
            this.txtApellidoM.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtClabe.Text = string.Empty;
        }

        public void PrepareNewTabulador()
        {
            this.hdnIdEmpleadoTabulador.Value = string.Empty;
            this.hndIdDepartamento.Value = string.Empty;
            this.hndSalarioDiario.Value = string.Empty;
            this.hndIdPuesto.Value = string.Empty;
            this.lblError.Text = string.Empty;
            this.txtSalarioEmpleado.Text = string.Empty;
            this.ddlPuesto.Items.Clear();
            this.ddlTabulador.Items.Clear();
            this.ddlDepartamento.SelectedIndex = 0;
            this.lblLimiteInferior.Text = "0";
            this.lblLimiteSuperior.Text = "0";
        }

        public void ShowForm()
        {
            this.ddatos.Visible = true;
            this.mpePopUp.Show();
        }

        public void ShowFormTabulador()
        {
            this.dTabulador.Visible = true;
        }

        public void HideForm()
        {
            this.ddatos.Visible = false;
        }

        public void HideFormTabulador()
        {
            this.dTabulador.Visible = false;
        }

        #endregion
       
    }
}