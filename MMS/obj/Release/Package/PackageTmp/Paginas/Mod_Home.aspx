<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/PaginaMaestra.Master" CodeBehind="Mod_Home.aspx.vb" Inherits="MMS.Mod_Home" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
                   <!-- Page Heading -->
                <div class="row">
                    <div class="col-lg-12">
                     
                       <h1 class="page-header" id="TituloPrincipal" runat="server">Modificación y Eliminación de Ingresos</h1>
                        
                    </div>
                </div>
                <!-- /.row -->

      <div class="row">
                    <div class="col-lg-12">
                     
                        <ol class="breadcrumb" style="background-color: rgba(119, 115, 115, 0.25);">
                           
                            <li>
                                <i class="fa fa-fw fa-home"></i>  <a href="Home.aspx">Inicio</a>
                            </li>
                            
                        </ol>
                    </div>
                </div>

    
        <div class="panel panel-default">

      
                        <div class="panel-heading" style="background-color: #3c9c40;">
                    Datos 
                        </div>
                                                                    
                   <div class="panel-body">


                           <div class="form-group input-group" >
                                   <span class="input-group-addon">TIPO</span>
                                                 <asp:DropDownList runat ="server" ID="DropDownTIPO" name="DropDownTIPO" class="form-control">
                                                 <asp:ListItem Text="INGRESO" />
                                                  <asp:ListItem Text="EGRESO" />
                                             
                                    </asp:DropDownList>
                                            <span class="input-group-btn">
                                              <asp:Button Text="Buscar" ID="ComboTipo" BackColor="#e2e2e2" class="btn btn-default" runat="server" />
                            </span>
                                        </div>
                       

                           <div class="form-group input-group">
                          <span class="input-group-addon">CATEGORIA</span>
                                       
                                    <asp:DropDownList runat ="server" ID="DropDownCate" name="DropDownCate" class="form-control">
                                             
                                    </asp:DropDownList>
                                              <span class="input-group-btn">
                                              <asp:Button Text="Buscar" ID="ComboCategoria" BackColor="#e2e2e2" class="btn btn-default" runat="server" />
                            </span>
                                     </div>

                       
                           <div class="form-group input-group">
                          <span class="input-group-addon">SUB CATEGORIA</span>
                                       
                                    <asp:DropDownList runat ="server" ID="DropDownSubCategoria" name="DropDownSubCategoria" class="form-control">
                                             
                                    </asp:DropDownList>
                                          
                                     </div>


            <div class="form-group">
                    <div class="form-group input-group">
                                  <span class="input-group-addon">FECHA</span>
             <asp:TextBox type="date" runat="server" class="form-control" id ="DateTimeFecha" name="DateTimeFecha" />
                </div>
                
                  </div>

                       
                           <div class="form-group input-group">
                                            <span class="input-group-addon">OBSERVACIONES</span>
                           <asp:TextBox type="text" runat="server"  placeholder="Observaciones"  style="text-transform:uppercase" class="form-control" id="TextBoxDetalle" />
                           </div> 
                       
                        <div class="form-group input-group">
                          <span class="input-group-addon">FORMA PAGO</span>
                                       
                                    <asp:DropDownList runat ="server" ID="DropDownFormaPago" name="DropDownFormaPago" class="form-control">
                                               <asp:ListItem Text="Efectivo" />
                                                 <asp:ListItem Text="Tarjeta" /> 
                                    </asp:DropDownList>
                                          
                                     </div>


                                  <div class="form-group input-group">
                                  <span class="input-group-addon">PRECIO</span>
             <asp:TextBox type="text" runat="server"  placeholder="Precio"  style="text-transform:uppercase" class="form-control" id="txtPrecio" />
                </div>
                           
            
           <asp:Button class="btn btn-primary btn-sm"  id="SubmitGuardar" runat="server" Text="Guardar" style="background-color:#008287;color:white;" >  </asp:Button>
           <asp:Button class="btn btn-primary btn-sm"  id="btnEliminar" runat="server" Text="Eliminar" style="background-color:red;color:white;" >  </asp:Button>
        
      </div>

        </div>
    
</asp:Content>

