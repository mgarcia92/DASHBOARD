﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace B2B_DASHBOARD.ServiceSap {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    // CODEGEN: The optional WSDL extension element 'Policy' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ZSD_DASHBOARD_METHOD01", Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_DASHBOARD_METHOD01 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ZSD_SERVI_METHODOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ZSD_DASHBOARD_METHOD01() {
            this.Url = global::B2B_DASHBOARD.Properties.Settings.Default.B2B_DASHBOARD_ServiceSap_ZSD_DASHBOARD_METHOD01;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ZSD_SERVI_METHODCompletedEventHandler ZSD_SERVI_METHODCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sap-com:document:sap:rfc:functions:ZSD_DASHBOARD_METHOD01:ZSD_SERVI_METHODReq" +
            "uest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ZSD_SERVI_METHODResponse", Namespace="urn:sap-com:document:sap:rfc:functions")]
        public ZSD_SERVI_METHODResponse ZSD_SERVI_METHOD([System.Xml.Serialization.XmlElementAttribute("ZSD_SERVI_METHOD", Namespace="urn:sap-com:document:sap:rfc:functions")] ZSD_SERVI_METHOD ZSD_SERVI_METHOD1) {
            object[] results = this.Invoke("ZSD_SERVI_METHOD", new object[] {
                        ZSD_SERVI_METHOD1});
            return ((ZSD_SERVI_METHODResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ZSD_SERVI_METHODAsync(ZSD_SERVI_METHOD ZSD_SERVI_METHOD1) {
            this.ZSD_SERVI_METHODAsync(ZSD_SERVI_METHOD1, null);
        }
        
        /// <remarks/>
        public void ZSD_SERVI_METHODAsync(ZSD_SERVI_METHOD ZSD_SERVI_METHOD1, object userState) {
            if ((this.ZSD_SERVI_METHODOperationCompleted == null)) {
                this.ZSD_SERVI_METHODOperationCompleted = new System.Threading.SendOrPostCallback(this.OnZSD_SERVI_METHODOperationCompleted);
            }
            this.InvokeAsync("ZSD_SERVI_METHOD", new object[] {
                        ZSD_SERVI_METHOD1}, this.ZSD_SERVI_METHODOperationCompleted, userState);
        }
        
        private void OnZSD_SERVI_METHODOperationCompleted(object arg) {
            if ((this.ZSD_SERVI_METHODCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ZSD_SERVI_METHODCompleted(this, new ZSD_SERVI_METHODCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_SERVI_METHOD {
        
        private string cUSTOMERField;
        
        private RSIS_S_RANGE[] dATEField;
        
        private ZSD_DOCUMENTS_DASHBOARD[] dATOS_DOCUMENTOSField;
        
        private ZSD_POM_DASHBOARD[] dATOS_POMField;
        
        private string oRDERField;
        
        private RSIS_S_RANGE[] oRGVField;
        
        private string rEQUESTField;
        
        private string rOUTEField;
        
        private string sYSTEMField;
        
        private RSIS_S_RANGE[] vAR001Field;
        
        private string vAR002Field;
        
        private string vAR003Field;
        
        private string vAR004Field;
        
        private string vAR005Field;
        
        private RSIS_S_RANGE[] wERKSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CUSTOMER {
            get {
                return this.cUSTOMERField;
            }
            set {
                this.cUSTOMERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public RSIS_S_RANGE[] DATE {
            get {
                return this.dATEField;
            }
            set {
                this.dATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_DOCUMENTS_DASHBOARD[] DATOS_DOCUMENTOS {
            get {
                return this.dATOS_DOCUMENTOSField;
            }
            set {
                this.dATOS_DOCUMENTOSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_POM_DASHBOARD[] DATOS_POM {
            get {
                return this.dATOS_POMField;
            }
            set {
                this.dATOS_POMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ORDER {
            get {
                return this.oRDERField;
            }
            set {
                this.oRDERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public RSIS_S_RANGE[] ORGV {
            get {
                return this.oRGVField;
            }
            set {
                this.oRGVField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string REQUEST {
            get {
                return this.rEQUESTField;
            }
            set {
                this.rEQUESTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ROUTE {
            get {
                return this.rOUTEField;
            }
            set {
                this.rOUTEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SYSTEM {
            get {
                return this.sYSTEMField;
            }
            set {
                this.sYSTEMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public RSIS_S_RANGE[] VAR001 {
            get {
                return this.vAR001Field;
            }
            set {
                this.vAR001Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VAR002 {
            get {
                return this.vAR002Field;
            }
            set {
                this.vAR002Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VAR003 {
            get {
                return this.vAR003Field;
            }
            set {
                this.vAR003Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VAR004 {
            get {
                return this.vAR004Field;
            }
            set {
                this.vAR004Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VAR005 {
            get {
                return this.vAR005Field;
            }
            set {
                this.vAR005Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public RSIS_S_RANGE[] WERKS {
            get {
                return this.wERKSField;
            }
            set {
                this.wERKSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class RSIS_S_RANGE {
        
        private string sIGNField;
        
        private string oPTIONField;
        
        private string lOWField;
        
        private string hIGHField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIGN {
            get {
                return this.sIGNField;
            }
            set {
                this.sIGNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPTION {
            get {
                return this.oPTIONField;
            }
            set {
                this.oPTIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOW {
            get {
                return this.lOWField;
            }
            set {
                this.lOWField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string HIGH {
            get {
                return this.hIGHField;
            }
            set {
                this.hIGHField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_POM_DASHBOARD {
        
        private string vRSIOField;
        
        private string vKORGField;
        
        private string pVRTNRField;
        
        private string pKUNAGField;
        
        private string nAME1Field;
        
        private string mATNRField;
        
        private decimal zPLAN_SOPField;
        
        private decimal zPESO_PEDIField;
        
        private decimal zPESO_ENTRField;
        
        private decimal zLOGROField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VRSIO {
            get {
                return this.vRSIOField;
            }
            set {
                this.vRSIOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VKORG {
            get {
                return this.vKORGField;
            }
            set {
                this.vKORGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PVRTNR {
            get {
                return this.pVRTNRField;
            }
            set {
                this.pVRTNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PKUNAG {
            get {
                return this.pKUNAGField;
            }
            set {
                this.pKUNAGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME1 {
            get {
                return this.nAME1Field;
            }
            set {
                this.nAME1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATNR {
            get {
                return this.mATNRField;
            }
            set {
                this.mATNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZPLAN_SOP {
            get {
                return this.zPLAN_SOPField;
            }
            set {
                this.zPLAN_SOPField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZPESO_PEDI {
            get {
                return this.zPESO_PEDIField;
            }
            set {
                this.zPESO_PEDIField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZPESO_ENTR {
            get {
                return this.zPESO_ENTRField;
            }
            set {
                this.zPESO_ENTRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZLOGRO {
            get {
                return this.zLOGROField;
            }
            set {
                this.zLOGROField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_DOCUMENTS_DASHBOARD {
        
        private string iD_DOCField;
        
        private string dOC_TYPEField;
        
        private string pURCHASE_ORDERField;
        
        private string cUSTOMERField;
        
        private string dATEField;
        
        private decimal nET_AMOUNTField;
        
        private string pOSNRField;
        
        private string mATERIALField;
        
        private string dESCRIPCIONField;
        
        private string mEINSField;
        
        private string eSTADO_ENTREGAField;
        
        private string eSTADO_FACTURAField;
        
        private decimal cANTIDADField;
        
        private string fACTURAField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ID_DOC {
            get {
                return this.iD_DOCField;
            }
            set {
                this.iD_DOCField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DOC_TYPE {
            get {
                return this.dOC_TYPEField;
            }
            set {
                this.dOC_TYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PURCHASE_ORDER {
            get {
                return this.pURCHASE_ORDERField;
            }
            set {
                this.pURCHASE_ORDERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CUSTOMER {
            get {
                return this.cUSTOMERField;
            }
            set {
                this.cUSTOMERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DATE {
            get {
                return this.dATEField;
            }
            set {
                this.dATEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal NET_AMOUNT {
            get {
                return this.nET_AMOUNTField;
            }
            set {
                this.nET_AMOUNTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POSNR {
            get {
                return this.pOSNRField;
            }
            set {
                this.pOSNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATERIAL {
            get {
                return this.mATERIALField;
            }
            set {
                this.mATERIALField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DESCRIPCION {
            get {
                return this.dESCRIPCIONField;
            }
            set {
                this.dESCRIPCIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MEINS {
            get {
                return this.mEINSField;
            }
            set {
                this.mEINSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ESTADO_ENTREGA {
            get {
                return this.eSTADO_ENTREGAField;
            }
            set {
                this.eSTADO_ENTREGAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ESTADO_FACTURA {
            get {
                return this.eSTADO_FACTURAField;
            }
            set {
                this.eSTADO_FACTURAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CANTIDAD {
            get {
                return this.cANTIDADField;
            }
            set {
                this.cANTIDADField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FACTURA {
            get {
                return this.fACTURAField;
            }
            set {
                this.fACTURAField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_SERVI_METHODResponse {
        
        private ZSD_DOCUMENTS_DASHBOARD[] dATOS_DOCUMENTOSField;
        
        private ZSD_POM_DASHBOARD[] dATOS_POMField;
        
        private string eDIRECCIONField;
        
        private string eLATITUDEField;
        
        private string eLONGITUDEField;
        
        private string eLZONEField;
        
        private string eLZONE_TField;
        
        private string eNOMBREField;
        
        private string eREGIOField;
        
        private string eREGIO_TField;
        
        private string eRIFField;
        
        private string eRROR_MESSAGEField;
        
        private string eRR_MAPSField;
        
        private string fLAG_01Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_DOCUMENTS_DASHBOARD[] DATOS_DOCUMENTOS {
            get {
                return this.dATOS_DOCUMENTOSField;
            }
            set {
                this.dATOS_DOCUMENTOSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_POM_DASHBOARD[] DATOS_POM {
            get {
                return this.dATOS_POMField;
            }
            set {
                this.dATOS_POMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EDIRECCION {
            get {
                return this.eDIRECCIONField;
            }
            set {
                this.eDIRECCIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ELATITUDE {
            get {
                return this.eLATITUDEField;
            }
            set {
                this.eLATITUDEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ELONGITUDE {
            get {
                return this.eLONGITUDEField;
            }
            set {
                this.eLONGITUDEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ELZONE {
            get {
                return this.eLZONEField;
            }
            set {
                this.eLZONEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ELZONE_T {
            get {
                return this.eLZONE_TField;
            }
            set {
                this.eLZONE_TField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ENOMBRE {
            get {
                return this.eNOMBREField;
            }
            set {
                this.eNOMBREField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EREGIO {
            get {
                return this.eREGIOField;
            }
            set {
                this.eREGIOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EREGIO_T {
            get {
                return this.eREGIO_TField;
            }
            set {
                this.eREGIO_TField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ERIF {
            get {
                return this.eRIFField;
            }
            set {
                this.eRIFField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ERROR_MESSAGE {
            get {
                return this.eRROR_MESSAGEField;
            }
            set {
                this.eRROR_MESSAGEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ERR_MAPS {
            get {
                return this.eRR_MAPSField;
            }
            set {
                this.eRR_MAPSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FLAG_01 {
            get {
                return this.fLAG_01Field;
            }
            set {
                this.fLAG_01Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ZSD_SERVI_METHODCompletedEventHandler(object sender, ZSD_SERVI_METHODCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ZSD_SERVI_METHODCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ZSD_SERVI_METHODCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ZSD_SERVI_METHODResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZSD_SERVI_METHODResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591