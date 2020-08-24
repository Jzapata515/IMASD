<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="EmpleadosUI.aspx.cs" Inherits="Nomina2018.EmpleadosUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
    <script src="/Scripts/jquery-1.4.2.min.js"></script>
    <script src="/Scripts/jquery.alerts.js"></script>
    <script src="/Scripts/jquery-1.9.1.js"></script>
    <script src="/Scripts/jquery-ui-1.10.1.custom.js"></script>
    <link href="/Content/jquery.alerts.css" rel="stylesheet" />
    <link href="/Content/jquery-ui.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
                <h2>CATÁLOGO DE EMPLEADOS</h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dFiltros" class="divfiltros" runat="server">
        <asp:UpdatePanel ID="uPFiltros" runat="server">
        <ContentTemplate>
        <div class="filtrofila">
        <div class="filtrocolumna">
            <asp:Image ID="imgFiltro" src="Images/filtro.png" runat="server" />
        </div>
        <div class="filtrocolumna">
            <span class="label"><asp:Label ID="lblNumeroEmpleadoFiltro" runat="server" Text="Número de Empleado "></asp:Label></span>
            <span class="textbox"><asp:TextBox ID="txtNumeroEmpleadoFiltro" runat="server" Width="100px" MaxLength="10"></asp:TextBox></span>                
        </div> 
        <div class="filtrocolumna">
                <span class="label"><asp:Label ID="lblDepartamentoFiltro" runat="server" Text="Departamento "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlDepartamentoFiltro" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoFiltro_SelectedIndexChanged" Width="150px"></asp:DropDownList></span>   
         </div>
            <div class="filtrocolumna">
                <span class="label"><asp:Label ID="lblPuestoFiltro" runat="server" Text="Puesto "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlPuestoFiltro" runat="server"  AutoPostBack="True" Width="150px"></asp:DropDownList></span>   
         </div>
        <div class="filtrocolumna">
                <span class="label"><asp:Label ID="lblPeriodoPagoFiltro" runat="server" Text="Periodo de Pago "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlPeriodoPagoFiltro" runat="server" Width="150px"></asp:DropDownList></span>
        </div>         
        <div class="filtrocolumna">            
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CausesValidation="False" />
        </div>
       </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br/>
    <div id="drepeater" class="divrepeater" runat="server">
        <asp:ImageButton ID="ibtnNuevo" runat="server" ImageUrl="~/IMAGES/add.png" Height="30px" Width="30px" ToolTip="Nuevo Empleado" OnClick="ibtnNuevo_Click"/>
        <asp:UpdatePanel ID="uPRepeater" runat="server">
        <ContentTemplate>
        <asp:Repeater ID="rEmpleados" runat="server" OnItemCommand="rEmpleados_ItemCommand">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th class="thbutton"></th>
                        <th class="thbutton"></th>
                        <th class="thbutton"></th>
                        <th class="th">Número</th>
                        <th class="th">Nombre</th>
                        <th class="th">Departamento</th>
                        <th class="th">Puesto</th>
                        <th class="th">Periodo Pago</th>            
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="trR">
                    <td align="center">
                        <asp:ImageButton ID="ibtEditar" runat="server" ImageUrl="~/IMAGES/writep.png" CommandName="Editar"
                            CommandArgument='<%# Eval("IdEmpleado") %>' Width="20px" ToolTip="Editar Datos del Empleado" CausesValidation="True" />
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="ibtEliminar" runat="server" ImageUrl="~/IMAGES/deletep.png"
                            OnClientClick=" return confirm('¿Desea elimiar este Empleado?');"
                            CommandName="Eliminar" CommandArgument='<%# Eval("IdEmpleado") %>' Width="20px" ToolTip="Eliminar Empleado" CausesValidation="True" />
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="ibtTabuladores" runat="server" ImageUrl="~/IMAGES/details.png"
                            CommandName="Tabulador" CommandArgument='<%# Eval("IdEmpleado") %>' Width="20px" ToolTip="Tabulador del Empleado" CausesValidation="True" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtIDEmpleado" Text='<%# Eval("IdEmpleado") %>' CssClass="textboxR"
                            Width="50px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtNOMBRECOMPLETO" Text='<%# Eval("NombreCompleto") %>' CssClass="textboxR"
                            Width="200px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtDEPARTAMENTO" Text='<%# Eval("Departamento") %>' CssClass="textboxR"
                            Width="200px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtPUESTO" Text='<%# Eval("Puesto") %>' CssClass="textboxR"
                            Width="100px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtPERIODOPAGO" Text='<%# Eval("PeriodoPago") %>' CssClass="textboxR"
                            Width="100px" ReadOnly="True" Font-Size="X-Small" />
                    </td>                                  
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="trRA">
                    <td align="center">
                        <asp:ImageButton ID="ibtEditar" runat="server" ImageUrl="~/IMAGES/writep.png" CommandName="Editar"
                            CommandArgument='<%# Eval("IdEmpleado") %>' Width="20px" ToolTip="Editar Datos del Empleado" CausesValidation="True" />
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="ibtEliminar" runat="server" ImageUrl="~/IMAGES/deletep.png"
                            OnClientClick=" return confirm('¿Desea elimiar este Empleado?');"
                            CommandName="Eliminar" CommandArgument='<%# Eval("IdEmpleado") %>' Width="20px" ToolTip="Eliminar Empleado" CausesValidation="True" />
                    </td>
                    <td align="center">
                        <asp:ImageButton ID="ibtTabuladores" runat="server" ImageUrl="~/IMAGES/details.png"
                            CommandName="Tabulador" CommandArgument='<%# Eval("IdEmpleado") %>' Width="20px" ToolTip="Tabulador del Empleado" CausesValidation="True" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtIDEmpleadoR" Text='<%# Eval("IdEmpleado") %>' CssClass="textboxRA"
                            Width="50px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtNOMBRECOMPLETOR" Text='<%# Eval("NombreCompleto") %>' CssClass="textboxRA"
                            Width="200px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtDEPARTAMENTOR" Text='<%# Eval("Departamento") %>' CssClass="textboxRA"
                            Width="200px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtPUESTOR" Text='<%# Eval("Puesto") %>' CssClass="textboxRA"
                            Width="100px" ReadOnly="True" Font-Size="X-Small" />
                    </td>
                    <td align="center">
                        <asp:TextBox runat="server" ID="txtPERIODOPAGOR" Text='<%# Eval("PeriodoPago") %>' CssClass="textboxRA"
                            Width="100px" ReadOnly="True" Font-Size="X-Small" />
                    </td>        
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>  
        </ContentTemplate>
        </asp:UpdatePanel>      
    </div>
    <br/>
    <br/>
    <asp:Label ID="lblTarget" runat="server" Text=""></asp:Label>
    <ajaxToolkit:ModalPopupExtender ID="mpePopUp" runat="server" TargetControlID="lblTarget" PopupControlID="ddatos" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    
    <asp:UpdatePanel ID="uPFormularios" runat="server">
    <ContentTemplate>
     
    <div id="ddatos" class="divformulario" runat="server">
        
        <div>
            
            <div class="fila"></div>
            <div class="fila">
                <span class="label">
                <asp:HiddenField ID="hndIdEmpleado" runat="server" />
                <asp:HiddenField ID="hndEstatus" runat="server" />
                <asp:HiddenField ID="hndIdDepartamento" runat="server" />
                <asp:HiddenField ID="hndIdPuesto" runat="server" />
                <asp:HiddenField ID="hndSalarioDiario" runat="server" />
                <asp:Label ID="lblNombres" runat="server" Text="Nombres *"></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtNombres" runat="server"></asp:TextBox></span>
                <span class="error"><asp:RequiredFieldValidator ID="rfvNombres" runat="server" ErrorMessage="Dato Requerido" ControlToValidate="txtNombres" ValidationGroup="Empleado"></asp:RequiredFieldValidator></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="revNombres" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtNombres" ValidationExpression="[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>                
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblApellidoP" runat="server" Text="Apellido Paterno *"></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtApellidoP" runat="server"></asp:TextBox></span>
                <span class="requerido"><asp:RequiredFieldValidator ID="rfvApellidoP" runat="server" ErrorMessage="Dato Requerido" ControlToValidate="txtApellidoP" ValidationGroup="Empleado"></asp:RequiredFieldValidator></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="revApellidoP" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtApellidoP" ValidationExpression="[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblApellidoM" runat="server" Text="Apellido Materno *"></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtApellidoM" runat="server"></asp:TextBox></span>                
                <span class="requerido"><asp:RequiredFieldValidator ID="rfvApellidoM" runat="server" ErrorMessage="Dato Requerido" ControlToValidate="txtApellidoM" ValidationGroup="Empleado"></asp:RequiredFieldValidator></span>                
                <span class="invalido"><asp:RegularExpressionValidator ID="revApellidoM" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtApellidoM" ValidationExpression="[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙ.-]+" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblDireccion" runat="server" Text="Dirección "></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="revDireccion" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtDireccion" ValidationExpression="^[a-zA-Z0-9&amp;.,_ñÑ ]+$" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblTelefono" runat="server" Text="Teléfono "></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtTelefono" runat="server" MaxLength="50"></asp:TextBox></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="revTelefono" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtTelefono" ValidationExpression="^[0-9]*(\.[0-9]+)?$" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblEmail" runat="server" Text="Email "></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtEmail" runat="server" MaxLength="100"></asp:TextBox></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtEmail" ValidationExpression="^[^@]+@[^@]+\.[a-zA-Z]{2,}$" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>
            </div> 
            <div class="fila">
                <span class="label"><asp:Label ID="lblPeriodoPago" runat="server" Text="Periodo de Pago "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlPeriodoPago" runat="server"></asp:DropDownList></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblTipoPago" runat="server" Text="Tipo de Pago "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlTipoPago" runat="server"></asp:DropDownList></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblCllabe" runat="server" Text="Clabe "></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtClabe" runat="server" MaxLength="18"></asp:TextBox></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtClabe" ValidationExpression="^[0-9]*(\.[0-9]+)?$" ValidationGroup="Empleado"></asp:RegularExpressionValidator></span>
            </div>
            <div class="fila">
                <span class="button">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" ValidationGroup="Empleado" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </span>
            </div>
            <div class="fila"></div>
        </div>    
           
    </div>    
    <div id="dTabulador" class="divformulario" runat="server">
    <asp:UpdatePanel ID="uPTabuladores" runat="server">
    <ContentTemplate>
    <div>
           <div class="fila"></div>
           <div class="fila">
                <span class="label"><asp:Label ID="lblNombreEmpleadoL" runat="server" Text="Nombre Empleado"></asp:Label></span>
                <span class="textbox"><asp:Label ID="lblNombreEmpleado" runat="server"></asp:Label></span>
           </div>
           <div class="fila">   
               <span class="label"><asp:Label ID="lblDepartamento" runat="server" Text="Departamento "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlDepartamento" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"></asp:DropDownList></span>   
           </div>
           <div class="fila">
                <span class="label"><asp:Label ID="lblPuesto" runat="server" Text="Puesto "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlPuesto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPuesto_SelectedIndexChanged" ></asp:DropDownList></span>
           </div>
           <div class="fila">
                <span class="label"><asp:Label ID="lblTabulador" runat="server" Text="Tabulador "></asp:Label></span>
                <span class="textbox"><asp:DropDownList ID="ddlTabulador" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTabulador_SelectedIndexChanged" ></asp:DropDownList></span>
           </div>
           <div class="fila">
                <span class="label">
                <asp:HiddenField ID="hdnIdTabulador" runat="server" />
                <asp:HiddenField ID="hdnIdEmpleadoTabulador" runat="server" />
                <asp:Label ID="lblLimiteInferiorL" runat="server" Text="Límite Inferior"></asp:Label></span>
                <span class="textbox"><asp:Label ID="lblLimiteInferior" runat="server" Text="0"></asp:Label></span>
               
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblLimiteSuperiorL" runat="server" Text="Límite Superior"></asp:Label></span>
                <span class="textbox"><asp:Label ID="lblLimiteSuperior" runat="server" Text="0"></asp:Label></span>
            </div>
            <div class="fila">
                <span class="label"><asp:Label ID="lblSalarioEmpleado" runat="server" Text="Salario Empleado"></asp:Label></span>
                <span class="textbox"><asp:TextBox ID="txtSalarioEmpleado" runat="server"></asp:TextBox></span>
                <span class="invalido"><asp:RegularExpressionValidator ID="revSalarioEmpleado" runat="server" ErrorMessage="Dato no válido" ControlToValidate="txtSalarioEmpleado" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="Tabulador"></asp:RegularExpressionValidator></span>
            </div>
            <div class="fila">
                <span class="label"></span>
                <span class="textbox"><asp:Label ID="lblError" runat="server" Text="" ForeColor="#990000"></asp:Label></span>
            </div>
            <div class="fila">
                <span class="button">
                    <asp:Button ID="btnGuardarTabulador" runat="server" Text="Guardar" ValidationGroup="Tabulador" OnClick="btnGuardarTabulador_Click" />
                    <asp:Button ID="btnCancelarTabulador" runat="server" Text="Cancelar" OnClick="btnCancelarTabulador_Click" />
                </span>
            </div>
            <div class="fila"></div>
    </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
    
</asp:Content>
